using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Mordida Venenosa", menuName ="Cards/Mordida Venenosa")]
public class MordidaVenenosa : BaseCard
{
    public int damage = 9;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        enemy.TakeDamage(damage);
    }
}
