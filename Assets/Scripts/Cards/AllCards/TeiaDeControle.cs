using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Teia de Controle", menuName ="Cards/Teia de Controle")]
public class TeiaDeControle : BaseCard
{
    public int defense = 999999;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        player.AddBlock(defense);
    }
}
