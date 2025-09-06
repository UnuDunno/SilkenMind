using UnityEngine;
using System.Collections.Generic;

public class PlayerMapProgress : MonoBehaviour
{
    public Color avaiableColor = Color.green;
    public Color selectedColor = Color.yellow;

    public MapGenerator mapGenerator;

    private Node currentNode;
    private bool ongoingEvent = false;

    public CombatNodeHandler combatNodeHandler;
    public EventNodeHandler eventNodeHandler;
    public ShopNodeHandler shopNodeHandler;
    public RestNodeHandler restNodeHandler;


    public void Initialize()
    {
        if (mapGenerator.Map.Count <= 0 || mapGenerator.Map[0].Count <= 0) return;
    
        List<Node> externalLayer = mapGenerator.Map[0];

        currentNode = externalLayer[Random.Range(0, externalLayer.Count)];
        
        UpdateAvaiability(first:true);
    }

    public void SelectNode(Node chosen)
    {
        if (chosen == null) return;

        if (ongoingEvent) return;

        currentNode.SetPlayerActive(false);
        currentNode = chosen;

        ongoingEvent = true;
        UpdateAvaiability();

        switch (chosen.data.type)
        {
            case NodeData.NodeType.Combat:
            case NodeData.NodeType.Elite:
            case NodeData.NodeType.Boss:
                combatNodeHandler.Initiate(chosen);
                break;
            case NodeData.NodeType.Event:
                eventNodeHandler.Initiate(chosen);
                break;
            case NodeData.NodeType.Shop:
                shopNodeHandler.Initiate(chosen);
                break;
            case NodeData.NodeType.Rest:
                restNodeHandler.Initiate(chosen);
                break;
        }
    }

    public void EndCurrentEvent()
    {
        if (currentNode == null) return;

        currentNode.SetAsCompleted();

        ongoingEvent = false;

        UpdateAvaiability();

        Debug.Log($"Evento concluído em nó");
    }

    private void UpdateAvaiability(bool first = false)
    {
        foreach (List<Node> layer in mapGenerator.Map)
        {
            foreach (Node node in layer)
            {
                if (node == null) continue;

                node.ResetColor();
                node.SetAlpha(0.3f);
                node.SetInteractable(false);
                node.SetPlayerActive(false);
            }
        }

        if (mapGenerator.bossNode != null)
        {
            mapGenerator.bossNode.ResetColor();
            mapGenerator.bossNode.SetAlpha(0.3f);
            mapGenerator.bossNode.SetInteractable(false);
            mapGenerator.bossNode.SetPlayerActive(false);
        }

        if (currentNode != null)
        {
            currentNode.SetColor(selectedColor);
            currentNode.SetAlpha(1f);
            currentNode.SetInteractable(true);
            currentNode.SetPlayerActive(true);
        }


        if (!ongoingEvent && currentNode != null && !first)
        {
            foreach (Node next in currentNode.nextNodes)
            {
                if (!next.completed)
                {
                    next.nextNodes.Add(next);
                    next.SetColor(avaiableColor);
                    next.SetAlpha(1f);
                    next.SetInteractable(true);
                }
            }
        }
    }
}
