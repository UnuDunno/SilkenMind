using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Banquete Macabro", menuName ="Cards/Banquete Macabro")]
public class BanqueteMacabro : BaseCard
{
    public int heal = 6;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        if (PlayerStats.Instance == null) return;

        PlayerStats.Instance.Heal(heal);
    }
}
