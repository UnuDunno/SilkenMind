using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Sussuros da Escuridão", menuName ="Cards/Sussuros da Escuridão")]
public class SussurosDaEscuridao : BaseCard
{
    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        if (PlayerStats.Instance == null) return;

        PlayerStats.Instance.Heal(PlayerStats.Instance.maxHealth);
    }
}
