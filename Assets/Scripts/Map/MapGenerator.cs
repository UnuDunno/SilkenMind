using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    public int layers = 5;
    public int nodesPerLayer = 8;
    public float initialRadius = 620f;
    public float radiusIncrement = 150f;

    public GameObject nodePrefab;
    public GameObject edgePrefab;
    public GameObject bossPrefab;

    public RectTransform content;
    public RectTransform nodesRoot;
    public RectTransform connectionsRoot;

    public List<NodeData> nodeTypes;
    public NodeData bossNodeData;

    public UnityEvent OnMapCreated;

    public bool Created { get; private set; } = false;
    private readonly List<List<Node>> map = new List<List<Node>>();
    public List<List<Node>> Map => map;

    [HideInInspector] public Node bossNode;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        Created = false;

        ClearMap();
        Map.Clear();
        AdjustContent();

        if (connectionsRoot != null)
        {
            connectionsRoot.SetAsFirstSibling();
        }
        if (nodesRoot != null)
        {
            nodesRoot.SetAsLastSibling();
        }

        // Criação dos nós do mapa
        float radius, step, angle;
        for (int layer = 0; layer < layers; layer++)
        {
            radius = initialRadius - layer * radiusIncrement;
            step = 360f / nodesPerLayer;

            List<Node> currentLayer = new List<Node>();

            for (int i = 0; i < nodesPerLayer; i++)
            {
                angle = step * i * Mathf.Deg2Rad;
                Vector2 pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

                GameObject newNode = Instantiate(nodePrefab, nodesRoot);
                newNode.GetComponent<RectTransform>().anchoredPosition = pos;

                Node node = newNode.GetComponent<Node>();

                var nodeData = ChooseNodeDataByWeight();
                if (nodeData != null)
                {
                    node.Configure(nodeData);
                }

                node.name = $"{(nodeData != null ? nodeData.nodeName : "Nó")}";

                currentLayer.Add(node);
            }

            Map.Add(currentLayer);
        }

        if (bossPrefab != null)
        {
            GameObject boss = Instantiate(bossPrefab, nodesRoot);
            boss.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            bossNode = boss.GetComponent<Node>();
            if (bossNodeData != null)
            {
                bossNode.Configure(bossNodeData);
            }
        }
        else
        {
            bossNode = null;
        }

        // Criação das linhas do mapa
        for (int layer = 0; layer < Map.Count; layer++)
        {
            List<Node> currentLayer = Map[layer];

            for (int node = 0; node < currentLayer.Count; node++)
            {
                Node a = currentLayer[node];
                Node b = currentLayer[(node + 1) % currentLayer.Count];

                CreateEdge(a.transform as RectTransform, b.transform as RectTransform);
            }

            if (layer < Map.Count - 1)
            {
                List<Node> nextLayer = Map[layer + 1];

                for (int node = 0; node < currentLayer.Count; node++)
                {
                    Node current = currentLayer[node];
                    Node n1 = nextLayer[node % nextLayer.Count];
                    Node n2 = nextLayer[(node + 1) % nextLayer.Count];

                    if (!current.nextNodes.Contains(n1))
                    {
                        current.nextNodes.Add(n1);

                    }
                    if (!current.nextNodes.Contains(n2))
                    {
                        current.nextNodes.Add(n2);
                    }

                    CreateEdge(current.transform as RectTransform, n1.transform as RectTransform);
                    CreateEdge(current.transform as RectTransform, n2.transform as RectTransform);
                }
            }
            else
            {
                if (bossNode != null)
                {
                    foreach (Node n in currentLayer)
                    {
                        if (!n.nextNodes.Contains(bossNode))
                        {
                            n.nextNodes.Add(bossNode);
                        }

                        CreateEdge(n.transform as RectTransform, bossNode.transform as RectTransform);
                    }
                }
            }
        }

        Created = true;

        OnMapCreated?.Invoke();
    }

    private NodeData ChooseNodeDataByWeight()
    {
        if (nodeTypes == null || nodeTypes.Count == 0) return null;

        int sum = 0;
        foreach (NodeData nd in nodeTypes)
        {
            if (nd != null && nd.weight > 0 && nd.type != NodeData.NodeType.Boss)
            {
                sum += nd.weight;
            }
        }

        if (sum <= 0) return nodeTypes[0];

        int rand = Random.Range(0, sum);
        sum = 0;
        foreach (NodeData nd in nodeTypes)
        {
            if (nd == null || nd.weight <= 0 || nd.type == NodeData.NodeType.Boss)
            {
                continue;
            }

            sum += nd.weight;

            if (rand < sum) return nd;
        }

        return nodeTypes[0];
    }

    private void CreateEdge(RectTransform a, RectTransform b)
    {
        if (edgePrefab == null || connectionsRoot == null) return;

        GameObject line = Instantiate(edgePrefab, connectionsRoot);
        RectTransform rt = line.GetComponent<RectTransform>();
        Image img = line.GetComponent<Image>();
        if (img != null)
        {
            img.raycastTarget = false;
        }

        Vector2 dir = b.anchoredPosition - a.anchoredPosition;
        float dist = dir.magnitude;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        rt.anchoredPosition = (a.anchoredPosition + b.anchoredPosition) * 0.5f;
        rt.sizeDelta = new Vector2(dist, rt.sizeDelta.y);
        rt.localRotation = Quaternion.Euler(0, 0, angle);
        rt.SetAsFirstSibling();
    }

    private void ClearMap()
    {
        if (nodesRoot != null)
        {
            for (int i = nodesRoot.childCount - 1; i >= 0; i--)
            {
                Destroy(nodesRoot.GetChild(i).gameObject);
            }
        }

        if (connectionsRoot != null)
        {
            for (int i = connectionsRoot.childCount - 1; i >= 0; i--)
            {
                Destroy(connectionsRoot.GetChild(i).gameObject);
            }
        }

        bossNode = null;
    }

    private void AdjustContent()
    {
        if (content == null) return;

        float maxRadius = initialRadius + Mathf.Max(0, layers - 1) * radiusIncrement + 80f;
        float diameter = maxRadius * 2f;

        content.sizeDelta = new Vector2(diameter, diameter);
        content.anchoredPosition = Vector2.zero;
        content.localScale = Vector3.one;
    }
}
