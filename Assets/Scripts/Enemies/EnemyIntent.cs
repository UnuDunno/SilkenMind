using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IntentType
{
    Attack,
    Block,
    Special
}

[CreateAssetMenu(fileName ="Intent", menuName ="Intenção do Inimigo")]
public class EnemyIntent : ScriptableObject
{
    public IntentType intentType;
    public string intentName = "Ataque";
    public Sprite intentIcon;

    public int attackDamage = 0;
    public int blockAmount = 0;
    public string specialAbilityDescription = "";
}
