using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }

    public enum CombatState { Setup, PlayerTurn, EnemyTurn, Victory, Defeat }
    private CombatState currentState;

    private CombatNodeHandler currentNodeHandler;
    private CombatEntity player;
    private EnemyData enemyData;
    private CombatEntity enemy;

    private List<BaseCard> playerMasterDeck;
    private List<BaseCard> drawPile = new List<BaseCard>();
    private List<BaseCard> hand = new List<BaseCard>();
    private List<BaseCard> discardPile = new List<BaseCard>();

    public GameObject cardPrefab;

    private GameObject cardHandContainer;
    private Button endTurnButton;
    private GameObject victoryScreen;
    private GameObject defeatScreen;
    private Button victoryContinueButton;
    private Button defeatContinueButton;

    private EnemyHealthUI enemyHealthUI;

    private BlockUI playerBlockUI;
    private BlockUI enemyBlockUI;

    private Image enemySpriteRenderer;

    public int initialHandSize = 5;

    private List<GameObject> handCardGameObjects = new List<GameObject>();

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StartCombat(List<BaseCard> deck, EnemyData enemySO, CombatNodeHandler handler)
    {
        StopAllCoroutines();

        Debug.Log("Iniciando combate");

        currentNodeHandler = handler;
        playerMasterDeck = deck;
        enemyData = enemySO;

        CombatPanelUIReferences uiReferences = handler.combatPanel.GetComponent<CombatPanelUIReferences>();

        if (uiReferences == null)
        {
            Debug.Log("O CombatPanel está sem o script para as referencias de ui");
            return;
        }

        cardHandContainer = uiReferences.cardHandContainer;
        endTurnButton = uiReferences.endTurnButton;
        victoryScreen = uiReferences.victoryScreen;
        defeatScreen = uiReferences.defeatScreen;
        victoryContinueButton = uiReferences.victoryContinueButton;
        defeatContinueButton = uiReferences.defeatContinueButton;

        enemyHealthUI = uiReferences.enemyHealthUI;

        playerBlockUI = uiReferences.playerBlockUI;
        enemyBlockUI = uiReferences.enemyBlockUI;

        enemySpriteRenderer = uiReferences.enemySpriteRenderer;

        endTurnButton.onClick.RemoveAllListeners();
        endTurnButton.onClick.AddListener(OnEndTurnButton);

        victoryContinueButton.onClick.RemoveAllListeners();
        victoryContinueButton.onClick.AddListener(OnVictoryContinue);

        defeatContinueButton.onClick.RemoveAllListeners();
        defeatContinueButton.onClick.AddListener(OnDefeatContinue);

        ChangeState(CombatState.Setup);
    }

    private void ChangeState(CombatState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case CombatState.Setup:
                StartCoroutine(SetupBattle());
                break;
            case CombatState.PlayerTurn:
                StartCoroutine(PlayerTurn());
                break;
            case CombatState.EnemyTurn:
                StartCoroutine(EnemyTurn());
                break;
            case CombatState.Victory:
                ProcessVictory();
                break;
            case CombatState.Defeat:
                ProcessDefeat();
                break;
        }
    }

    private IEnumerator SetupBattle()
    {
        Debug.Log("Configurando batalha");

        victoryScreen.SetActive(false);
        defeatScreen.SetActive(false);
        endTurnButton.interactable = false;

        player = new CombatEntity(PlayerStats.Instance.currentHealth);
        enemy = new CombatEntity(enemyData.maxHealth);

        if (enemyHealthUI != null)
        {
            enemy.onHealthChanged.RemoveAllListeners();
            enemy.onHealthChanged.AddListener(enemyHealthUI.UpdateHealth);

            enemyHealthUI.UpdateHealth(enemy.currentHealth, enemy.maxHealth);
        }

        if (playerBlockUI != null)
        {
            player.onBlockChanged.RemoveAllListeners();
            player.onBlockChanged.AddListener(playerBlockUI.UpdateBlock);

            playerBlockUI.UpdateBlock(player.currentBlock);
        }

        if (enemyBlockUI != null)
        {
            enemy.onBlockChanged.RemoveAllListeners();
            enemy.onBlockChanged.AddListener(enemyBlockUI.UpdateBlock);

            enemyBlockUI.UpdateBlock(enemy.currentBlock);
        }
        
        if(enemySpriteRenderer != null)
        {
            if (enemyData.enemySprite != null)
            {
                enemySpriteRenderer.sprite = enemyData.enemySprite;
                enemySpriteRenderer.gameObject.SetActive(true);
            }
            else
            {
                enemySpriteRenderer.gameObject.SetActive(false);
            }
        }

        BuildDrawPile();

        yield return null;

        ChangeState(CombatState.PlayerTurn);
    }

    private IEnumerator PlayerTurn()
    {
        Debug.Log("Início do turno do jogador");

        player.ResetBlock();
        PlayerStats.Instance.SetFear(PlayerStats.Instance.maxFear);

        int cardsToDraw = initialHandSize - hand.Count;

        if (cardsToDraw > 0)
        {
            Debug.Log($"Mão atual: {hand.Count}. Comprando {cardsToDraw} cartas.");

            yield return DrawCards(cardsToDraw);
        }
        else
        {
            Debug.Log($"Mão cheia ({hand.Count}). Nenhuma carta comprada.");
        }

        endTurnButton.interactable = true;
    }

    private IEnumerator EnemyTurn()
    {
        endTurnButton.interactable = false;

        enemy.ResetBlock();

        Debug.Log("Turno inimigo");

        yield return new WaitForSeconds(1.0f);

        int damage = enemyData.simpleAttackDamage;

        Debug.Log($"Inimigo ataca com {damage} de dano");

        damage = player.TakeDamage(damage);
        PlayerStats.Instance.TakeDamage(damage);

        if (player.currentHealth <= 0) ChangeState(CombatState.Defeat);
        else ChangeState(CombatState.PlayerTurn);
    }

    private void ProcessVictory()
    {
        Debug.Log("VITÓRIA");

        endTurnButton.interactable = false;

        ClearHandAndPiles();

        victoryScreen.SetActive(true);
    }

    private void ProcessDefeat()
    {
        Debug.Log("DERROTA");

        endTurnButton.interactable = false;

        ClearHandAndPiles();

        defeatScreen.SetActive(true);
    }

    public void TryPlayCard(CardInstance cardInstance)
    {
        if (currentState != CombatState.PlayerTurn) return;

        BaseCard cardData = cardInstance.data;

        if (PlayerStats.Instance.TrySpendFear(cardData.fearValue))
        {
            Debug.Log($"Carta jogada: {cardData.cardName}");

            cardData.ExecuteEffect(player, enemy);

            hand.Remove(cardData);
            discardPile.Add(cardData);

            handCardGameObjects.Remove(cardInstance.gameObject);
            Destroy(cardInstance.gameObject);

            if (enemy.currentHealth <= 0) ChangeState(CombatState.Victory);
        }
        else
        {
            Debug.Log("Energia insuficiente");
        }
    }

    private void OnEndTurnButton()
    {
        if (currentState == CombatState.PlayerTurn) ChangeState(CombatState.EnemyTurn);
    }

    private void OnVictoryContinue()
    {
        currentNodeHandler.HandleBattleVictory();
    }

    private void OnDefeatContinue()
    {
        currentNodeHandler.HandleBattleDefeat();

        if (enemySpriteRenderer != null) enemySpriteRenderer.gameObject.SetActive(false);
    }

    public IEnumerator DrawCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (drawPile.Count == 0)
            {
                ReshuffleDiscardPile();

                if (drawPile.Count == 0)
                {
                    Debug.Log("Sem mais cartas para comprar");

                    yield break;
                }
            }

            BaseCard cardData = drawPile[0];
            drawPile.RemoveAt(0);

            hand.Add(cardData);

            GameObject cardGO = Instantiate(cardPrefab, cardHandContainer.transform);

            handCardGameObjects.Add(cardGO);

            cardGO.GetComponent<CardInstance>().Initialize(cardData);

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void BuildDrawPile()
    {
        ClearHandAndPiles();

        drawPile = new List<BaseCard>(playerMasterDeck);

        BaseCard temp;
        int randomIndex;
        for (int i = 0; i < drawPile.Count; i++)
        {
            temp = drawPile[i];
            randomIndex = Random.Range(i, drawPile.Count);

            drawPile[i] = drawPile[randomIndex];
            drawPile[randomIndex] = temp;
        }
    }

    private void ReshuffleDiscardPile()
    {
        Debug.Log("Embaralhando descarte");

        drawPile.AddRange(discardPile);
        discardPile.Clear();

        BaseCard temp;
        int randomIndex;
        for (int i = 0; i < drawPile.Count; i++)
        {
            temp = drawPile[i];
            randomIndex = Random.Range(i, drawPile.Count);

            drawPile[i] = drawPile[randomIndex];
            drawPile[randomIndex] = temp;
        }
    }
    
    private void ClearHandAndPiles()
    {
        drawPile.Clear();
        discardPile.Clear();
        hand.Clear();

        foreach (GameObject cardGO in handCardGameObjects)
        {
            Destroy(cardGO);
        }

        handCardGameObjects.Clear();
    }
}
