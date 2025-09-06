using UnityEngine;

public enum CardType { Attack, Defense, Heal, Draw }

[CreateAssetMenu(fileName = "NovaCarta", menuName = "Cartas/BaseCard")]
public class BaseCard : ScriptableObject {
    public string cardName;
    public string description;
    public Sprite image;
    public CardType type;
    public int fearValue;
    public int value;

    public virtual void ExecuteEffect()
    {
        Debug.Log($"[DEBUG] {cardName} usada. Tipo: {type}, Valor: {fearValue}");

        CombatManager combatManager = GameObject.FindObjectOfType<CombatManager>();

        if (combatManager == null)
        {
            Debug.LogWarning("CombatManager não encontrado. Efeito não aplicado.");
            return;
        }

        switch (type)
        {
            case CardType.Attack:
                combatManager.DealDamageToEnemy(value);
                break;
            case CardType.Defense:
                combatManager.ApplyDefenseOnPlayer(value);
                break;
            case CardType.Heal:
                combatManager.HealPlayer(value);
                break;
            case CardType.Draw:
                combatManager.BuyCards(value);
                break;
            default:
                Debug.LogWarning("Tipo de carta não reconhecido.");
                break;
        }
    }
}
