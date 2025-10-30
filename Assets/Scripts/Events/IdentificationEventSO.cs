using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Evento Identificação", menuName ="Eventos/Identificação")]
public class IdentificationEventSO : BaseEventSO
{
    public string questionText;

    public string option1Text;
    public bool option1IsCorrect;

    public string option2Text;
    public bool option2IsCorrect;

    public string correctOutcomeText;
    public RewardType correctReward;
    public int correctRewardAmount;

    public string incorrectOutcomeText;

    public override void SetupPanel(EventPanelUI panel)
    {
        panel.DisplayIdentificationEvent(this);
    }

    public void ApplyOutcome(bool wasCorrect)
    {
        if (wasCorrect)
        {
            Debug.Log("Correto! Concedendo recompensa");

            switch (correctReward)
            {
                case RewardType.Gold:
                    PlayerWallet.Instance.AddGold(correctRewardAmount);
                    break;
                case RewardType.MaxHealth:
                    break;
                case RewardType.Fear:
                    break;
            }
        }
        else
        {
            Debug.Log("Incorreto. Apenas aprendizado");
        }
    }
}
