using UnityEngine;

[CreateAssetMenu(fileName = "Picada Venenosa", menuName = "SilkenMind/Cards/Picada Venenosa")]
public class PicadaVenenosa : BaseCard
{
    public int damage = 12;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        enemy.TakeDamage(damage);
    }
}