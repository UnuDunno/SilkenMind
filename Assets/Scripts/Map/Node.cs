using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    [Header("Referências UI")]
    public Image background;
    public Image icon;
    public Image state;

    [Header("Dados")]
    public NodeData data;
    public List<Node> nextNodes = new List<Node>();

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();

        if (button == null) return;

        button.onClick.AddListener(Clicked);
    }

    public void Configure(NodeData nodeData)
    {
        data = nodeData;

        if (background != null) background.color = data.color;
        if (icon != null) icon.sprite = data.icon;
    }

    private void Clicked()
    {
        Debug.Log("Clicked on node: " + data.name);
    }
}
