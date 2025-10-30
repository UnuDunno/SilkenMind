using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Investida", menuName ="Cards/Investida")]
public class Investida : BaseCard
{
    public int damage = 8;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        enemy.TakeDamage(damage);
    }
}
