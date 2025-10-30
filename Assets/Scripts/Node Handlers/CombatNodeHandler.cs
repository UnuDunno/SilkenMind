using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CombatNodeHandler : MonoBehaviour
{
    public GameObject combatPanel;

    public EnemyData enemyToSpawn;

    public int goldReward = 50;
    public List<BaseCard> cardRewardPool;

    public UnityEvent onVictory;
    public UnityEvent onDefeat;

    public void Initiate(Node node)
    {
        Debug.Log("⚔️ CombatNodeHandler: Iniciando nó de combate!");

        if (enemyToSpawn == null)
        {
            Debug.LogError("Nenhum Inimigo configurado neste nó");
            return;
        }
        if (PlayerDeckManager.Instance == null || CombatManager.Instance == null || PlayerStats.Instance == null)
        {
            Debug.LogError("Sistemas não encontrados");
            return;
        }

        List<BaseCard> playerDeck = PlayerDeckManager.Instance.GetDeck();

        combatPanel.SetActive(true);

        CombatManager.Instance.StartCombat(playerDeck, enemyToSpawn, this);
    }

    public void HandleBattleVictory()
    {
        Debug.Log("NodeHandler: Vitória recebida. Dando recompensa");

        PlayerWallet.Instance.AddGold(goldReward);

        int randIndex;
        BaseCard rewardCard;
        if (cardRewardPool != null && cardRewardPool.Count > 0)
        {
            randIndex = Random.Range(0, cardRewardPool.Count);

            rewardCard = cardRewardPool[randIndex];

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
