using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Casulo Protetor", menuName ="Cards/Casulo Protetor")]
public class CasuloProtetor : BaseCard
{
    public int defense = 12;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        player.AddBlock(defense);
    }
}
