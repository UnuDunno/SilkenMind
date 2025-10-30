using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Evento Curiosidade", menuName ="Eventos/Curiosidade")]
public class CuriosityEventSO : BaseEventSO
{
    public RewardType reward;
    public int rewardAmount;

    public override void SetupPanel(EventPanelUI panel)
    {
        panel.DisplayCuriosityEvent(this);
    }

    public void GrantReward()
    {
        switch(reward)
        {
            case RewardType.Gold:
                Debug.Log($"Recompensa: +{rewardAmount} Ouro");
                break;
            case RewardType.MaxHealth:
                Debug.Log($"Recompensa: +{rewardAmount} Vida Máxima");
                break;
            case RewardType.Fear:
                Debug.Log($"Recompensa: +{rewardAmount} Recuso Medo");
                break;
        }
    }
}
