using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Teia da Vida", menuName ="Cards/Teia da Vida")]
public class TeiaDaVida : BaseCard
{
    public int heal = 3;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        if (PlayerStats.Instance == null) return;

        PlayerStats.Instance.Heal(heal);
    }
}
