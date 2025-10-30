using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Espiar nas Sombras", menuName ="Cards/Espiar nas Sombras")]
public class EspiarNasSombras : BaseCard
{
    public int draw = 3;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        CombatManager combatManager = CombatManager.Instance;

        if (combatManager == null) return;

        combatManager.StartCoroutine(combatManager.DrawCards(draw));
    }
}
