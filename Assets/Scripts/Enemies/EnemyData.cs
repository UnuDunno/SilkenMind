using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Inimigo", menuName ="Novo Inimigo")]
public class EnemyData : ScriptableObject
{
    public string enemyName = "Aranha";
    public int maxHealth = 50;

    public int simpleAttackDamage = 8;
}
