using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Reflexos Aracnídeos", menuName ="Cards/Reflexos Aracnídeos")]
public class ReflexosAracnideos : BaseCard
{
    public int defense = 4;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        player.AddBlock(defense);
    }
}
