using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ND_Evento", menuName ="Map/Nó de Evento")]
public class EventNodeData : NodeData
{
    public List<BaseEventSO> nodeEventPool;
}
