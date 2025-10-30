using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Bloqueio", menuName ="Cards/Bloqueio")]
public class Bloqueio : BaseCard
{
    public int defense = 5;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        player.AddBlock(defense);
    }
}
