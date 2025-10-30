using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Investida Sombria", menuName ="Cards/Investida Sombria")]
public class InvestidaSombria : BaseCard
{
    public int damage = 12;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        enemy.TakeDamage(damage);
    }
}
