using UnityEngine;

[CreateAssetMenu(fileName = "NodeData", menuName = "Map/Node Data")]
public class NodeData : ScriptableObject
{
    public enum NodeType
    {
        Combat,
        Event,
        Shop,
        Rest,
        Elite,
        Boss
    }


    public string nodeName;
    public NodeType type;
    public Sprite icon;
    public int weight = 1;
    public Color color = Color.white;

}
