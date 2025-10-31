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
                PlayerWallet.Instance.AddGold(rewardAmount);
                break;
            case RewardType.MaxHealth:
                PlayerStats.Instance.IncreaseMaxHealth(rewardAmount);
                break;
            case RewardType.Fear:
                PlayerStats.Instance.IncreaseMaxFear(rewardAmount);
                break;
        }
    }
}
