using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Teia Protetora", menuName ="Cards/Teia Protetora")]
public class TeiaProtetora : BaseCard
{
    public int defense = 8;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        player.AddBlock(defense);
    }
}
