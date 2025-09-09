using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    public GameObject combatPanel;
    public TextMeshProUGUI playerHPText;
    public TextMeshProUGUI playerFearText;
    public TextMeshProUGUI playerDefenseText;
    public TextMeshProUGUI enemyHPText;
    public Button endOfTurnButton;

    public int initialHand = 5;

    public int playerMaxHP = 20;
    private int playerHP;
    public int playerMaxFear = 5;
    public int playerFear;
    public int playerDefense = 0;

    public int enemyMaxHP = 15;
    private int enemyHP;
    public int enemyDamage = 2;

    private CardManager cardManager;
    private PlayerMapProgress progress;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        combatPanel.SetActive(false);

        if (endOfTurnButton != null)
        {
            endOfTurnButton.onClick.AddListener(EndPlayerTurn);
        }
    }

    private void Start()
    {
        if (cardManager == null)
        {
            cardManager = FindObjectOfType<CardManager>();
        }

        if (progress == null)
        {
            progress = FindObjectOfType<PlayerMapProgress>();
        }

        playerHP = playerMaxHP;
    }

    public void StartCombat()
    {
        combatPanel.SetActive(true);
        if (cardManager != null)
        {
            cardManager.BuildPlayingDeck();
            cardManager.BuyCards(initialHand);
        }

        playerHP = (playerHP > playerMaxHP) ? playerMaxHP : playerHP;
        enemyHP = enemyMaxHP;
        playerFear = playerMaxFear;
        playerDefense = 0;

        UpdateUI();
    }

    public void DealDamageToEnemy(int damage)
    {
        enemyHP -= damage;
        if (enemyHP < 0)
        {
            enemyHP = 0;
        }

        UpdateUI();

        if (enemyHP <= 0)
        {
            Victory();
        }
    }

    public void ApplyDefenseOnPlayer(int defense)
    {
        playerDefense += defense;
        UpdateUI();
    }

    public void HealPlayer(int heal, bool update = true)
    {
        playerHP += heal;

        if (playerHP > playerMaxHP) playerHP = playerMaxHP;

        if (update) UpdateUI();
    }

    public void BuyCards(int drawQuantity)
    {
        if (cardManager == null)
        {
            Debug.LogWarning("CardManager não encontrado");
            return;
        }

        cardManager.BuyCards(drawQuantity);
    }

    public bool ApplyFear(int value)
    {
        if (value > playerFear) return false;

        playerFear -= value;

        return true;
    }

    public void EndPlayerTurn()
    {
        if (playerDefense < enemyDamage)
        {
            playerHP -= enemyDamage - playerDefense;
        }

        if (playerHP < 0)
        {
            playerHP = 0;
        }

        playerDefense = 0;
        playerFear = playerMaxFear;

        List<CardInstance> hand = cardManager.hand;
        if (hand.Count > 0)
        {
            List<int> toRemove = new List<int>();
            for (int i = hand.Count - 1; i >= 0; i--)
            {
                if (hand[i] == null) toRemove.Add(i);
            }

            foreach (int i in toRemove)
            {
                hand.RemoveAt(i);
            }
        }

        if (hand.Count < initialHand)
        {
            BuyCards(initialHand - hand.Count);
        }

        UpdateUI();

        if (playerHP <= 0)
        {
            Defeat();
        }

    }

    private void Victory()
    {
        Debug.Log("Vitória!");
        combatPanel.SetActive(false);
        cardManager.DiscardHand();

        if (progress != null)
        {
            progress.EndCurrentEvent();
        }
    }

    private void Defeat()
    {
        Debug.Log("Jogador derrotado...");
        combatPanel.SetActive(false);
        cardManager.DiscardHand();

        if (progress != null)
        {
            progress.EndCurrentEvent();
        }
    }

    private void UpdateUI()
    {
        if (playerHPText != null)
        {
            playerHPText.text = $"HP: {playerHP}/{playerMaxHP}";
        }

        if (playerDefenseText != null)
        {
            playerDefenseText.text = $"Defesa: {playerDefense}";
        }

        if (playerFearText != null)
        {
            playerFearText.text = $"Medo: {playerFear}/{playerMaxFear}";
        }

        if (enemyHPText != null)
        {
            enemyHPText.text = $"Inimigo: {enemyHP}/{enemyMaxHP}";
        }
    }
}
