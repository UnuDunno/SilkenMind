using UnityEngine;

[CreateAssetMenu(fileName = "NovaCarta", menuName = "Cartas/BaseCard")]
public class BaseCard : ScriptableObject 
{
    public string cardName;
    public string description;
    public Sprite image;
    public int fearValue;

    public virtual void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        Debug.Log($"Executando efeito de {cardName}");
    }
}
