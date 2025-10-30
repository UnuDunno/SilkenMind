using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Olhos de Caçador", menuName ="Cards/Olhos de Caçador")]
public class OlhosDeCacador : BaseCard
{
    public int draw = 2;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        CombatManager combatManager = CombatManager.Instance;

        if (combatManager == null) return;

        combatManager.StartCoroutine(combatManager.DrawCards(draw));
    }
}
