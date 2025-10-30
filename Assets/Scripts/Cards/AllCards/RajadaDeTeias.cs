using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Rajada de Teias", menuName ="Cards/Rajada de Teias")]
public class RajadaDeTeias : BaseCard
{
    public int damage = 10;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        enemy.TakeDamage(damage);
    }
}
