using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CombatNodeHandler : MonoBehaviour
{
    public GameObject combatPanel;

    private EnemyData currentEnemy;
    private int currentGoldReward;
    private List<BaseCard> currentCardPool;

    public UnityEvent onVictory;
    public UnityEvent onDefeat;

    public void Initiate(Node node)
    {
        Debug.Log("⚔️ CombatNodeHandler: Iniciando nó de combate!");

        CombatNodeData combatNodeData = node.data as CombatNodeData;

        if (combatNodeData == null)
        {
            Debug.LogError($"Os dados do nó '{node.name}' não são do tipo CombatNodeData");

            onVictory.Invoke();

            return;
        }

        currentEnemy = combatNodeData.enemiesToSpawn[Random.Range(0, combatNodeData.enemiesToSpawn.Count)];
        currentGoldReward = combatNodeData.goldReward;
        currentCardPool = combatNodeData.cardRewardPool;

        if (currentEnemy == null)
        {
            Debug.LogError($"CombatNodeData '{combatNodeData.name}' não tem um inimigo (EnemyData) configurado");

            onVictory.Invoke();

            return;
        }

        if (PlayerDeckManager.Instance == null || CombatManager.Instance == null || PlayerStats.Instance == null)
        {
            Debug.LogError("Sistemas (DeckManager, CombatManager ou PlayerStats) não encontrados");

            return;
        }

        List<BaseCard> playerDeck = PlayerDeckManager.Instance.GetDeck();

        combatPanel.SetActive(true);

        CombatManager.Instance.StartCombat(playerDeck, currentEnemy, this);
    }

    public void HandleBattleVictory()
    {
        Debug.Log("NodeHandler: Vitória recebida. Dando recompensa");

        PlayerWallet.Instance.AddGold(currentGoldReward);

        int randIndex;
        BaseCard rewardCard;
        if (currentCardPool != null && currentCardPool.Count > 0)
        {
            randIndex = Random.Range(0, currentCardPool.Count);

            rewardCard = currentCardPool[randIndex];

            PlayerDeckManager.Instance.AddCardToDeck(rewardCard);
        }

        combatPanel.SetActive(false);

        onVictory.Invoke();
    }

    public void HandleBattleDefeat()
    {
        Debug.Log("NodeHandler: Derrota recebida. Processando Game Over");

        combatPanel.SetActive(false);

        if (onDefeat.GetPersistentEventCount() > 0)
        {
            onDefeat.Invoke();
        }
        else
        {
            Debug.Log("Nenhum evento de derrota. Resetando o jogo");

            if(PlayerStats.Instance != null)
            {
                PlayerStats.Instance.ResetStats();
            }

            Scene currentScene = SceneManager.GetActiveScene();

            SceneManager.LoadScene(currentScene.name);
        }
    }
}
