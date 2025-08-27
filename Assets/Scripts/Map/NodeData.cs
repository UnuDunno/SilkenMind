using UnityEngine;

[CreateAssetMenu(fileName = "NodeData", menuName = "Map/Node Data")]
public class NodeData : ScriptableObject
{
    [Header("Configuração Visual")]
    public string nodeName;
    public Sprite icon;
    public Color color = Color.white;

    [Header("Geração Procedural")]
    [Range(1, 100)] public int weight = 10;
}
