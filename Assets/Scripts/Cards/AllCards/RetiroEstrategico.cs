using UnityEngine;

[CreateAssetMenu(fileName = "Retiro Estratégico", menuName = "SilkenMind/Cards/Retiro Estratégico")]
public class RetiroEstrategico : BaseCard
{
    public int block = 7;
    public int draw = 1;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        player.AddBlock(block);
        
        if (CombatManager.Instance != null)
        {
            CombatManager.Instance.StartCoroutine(CombatManager.Instance.DrawCards(draw));
        }
    }
}