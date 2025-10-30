using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Exoesqueleto Endurecido", menuName ="Cards/Exoesqueleto Endurecido")]
public class ExoesqueletoEndurecido : BaseCard
{
    public int defense = 6;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        player.AddBlock(defense);
    }
}
