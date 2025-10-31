using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Evento Escolha", menuName ="Eventos/Escolha")]
public class ChoiceEventSO : BaseEventSO
{
    public string choice1Text;
    public PunishmentType choice1Punishment;
    public int choice1Value;
    public string choice1OutcomeText;

    public string choice2Text;
    public PunishmentType choice2Punishment;
    public int choice2Value;
    public string choice2OutcomeText;

    public override void SetupPanel(EventPanelUI panel)
    {
        panel.DisplayChoiceEvent(this);
    }

    public void ApplyPunishment(int choiceIndex)
    {
        PunishmentType punishment = (choiceIndex == 0) ? choice1Punishment : choice2Punishment;
        int value = (choiceIndex == 0) ? choice1Value : choice2Value;

        switch(punishment)
        {
            case PunishmentType.TakeDamage:
                PlayerStats.Instance.TakeDamage(value);
                break;
            case PunishmentType.LoseCard:
                if (PlayerDeckManager.Instance != null)
                {
                    PlayerDeckManager.Instance.RemoveRandomCard();
                }
                else
                {
                    Debug.LogError("PlayerDeckManager não encontrado. Não foi possível remover a carta");
                }
                break;
            case PunishmentType.None:
                Debug.Log("Saiu ileso");
                break;
        }
    }
}
