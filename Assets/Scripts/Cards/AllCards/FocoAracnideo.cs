using UnityEngine;

[CreateAssetMenu(fileName = "Foco Aracnídeo", menuName = "SilkenMind/Cards/Foco Aracnídeo")]
public class FocoAracnideo : BaseCard
{
    public int fearGain = 2;

    public override void ExecuteEffect(CombatEntity player, CombatEntity enemy)
    {
        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.AddFear(fearGain);
        }
    }
}