using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ND_Combate", menuName ="Map/Nó de Combate")]
public class CombatNodeData : NodeData
{
    public List<EnemyData> enemiesToSpawn;

    public int goldReward = 50;

    public List<BaseCard> cardRewardPool;
}
