using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Configuração da Teia")]
    public int layers = 5;
    public int nodesPerLayer = 8;
    public float initialRadius = 850f;
    public float radiusSpacing = 185f;

    [Header("Prefabs")]
    public GameObject nodePrefab;
    public GameObject edgePrefab;

    [Header("Referências")]
    public RectTransform content;
    public Transform nodesRoot;
    public Transform connectionsRoot;
    public List<NodeData> nodeTypes;
    public NodeData bossNodeData;

    private List<List<Node>> map = new List<List<Node>>();

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        ClearMap();
        map.Clear();

        AdjustContent();

        for (int j = 0; j < layers; j++)
        {
            List<Node> layer = new List<Node>();

            float radius = initialRadius - j * radiusSpacing;
            int quantity = nodesPerLayer;

            for (int i = 0; i < quantity; i++)
            {
                float angle = (360f / quantity) * i;
                float rad = angle * Mathf.Deg2Rad;

                Vector3 pos = new Vector3(Mathf.Cos(rad) * radius, Mathf.Sin(rad) * radius, 0);

                GameObject newNode = Instantiate(nodePrefab, nodesRoot);
                newNode.GetComponent<RectTransform>().anchoredPosition = pos;

                Node node = newNode.GetComponent<Node>();
                node.Configure(ChooseRandomNodeData());

                layer.Add(node);
            }

            map.Add(layer);
        }

        GameObject bossObj = Instantiate(nodePrefab, nodesRoot);
        bossObj.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        Node boss = bossObj.GetComponent<Node>();
        if (bossNodeData != null)
        {
            boss.Configure(bossNodeData);
        }

        for (int j = 0; j < map.Count; j++)
        {
            List<Node> currentLayer = map[j];
            List<Node> nextLayer = (j < map.Count - 1) ? map[j + 1] : new List<Node>() { boss };

            for (int i = 0; i < currentLayer.Count; i++)
            {
                Node current = currentLayer[i];
                Node neighbor = currentLayer[(i + 1) % currentLayer.Count];

                CreateEdge(current.transform as RectTransform, neighbor.transform as RectTransform);
            }

            foreach (Node current in currentLayer)
            {
                Node destiny = nextLayer[Random.Range(0, nextLayer.Count)];
                current.nextNodes.Add(destiny);
                CreateEdge(current.transform as RectTransform, destiny.transform as RectTransform);
            }
        }
    }

    private void CreateEdge(RectTransform a, RectTransform b)
    {
        GameObject line = Instantiate(edgePrefab, connectionsRoot);
        RectTransform rt = line.GetComponent<RectTransform>();

        Vector2 dir = b.anchoredPosition - a.anchoredPosition;
        float dist = dir.magnitude;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        rt.anchoredPosition = (a.anchoredPosition + b.anchoredPosition) / 2f;
        rt.sizeDelta = new Vector2(dist, rt.sizeDelta.y);
        rt.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private NodeData ChooseRandomNodeData()
    {
        if (nodeTypes == null || nodeTypes.Count == 0)
        {
            return null;
        }

        int total = 0;
        foreach (var t in nodeTypes)
        {
            total += t.weight;
        }

        int r = Random.Range(0, total);
        int sum = 0;

        foreach (var t in nodeTypes)
        {
            sum += t.weight;
            if (r < sum)
            {
                return t;
            }
        }

        return nodeTypes[0];
    }

    private void ClearMap()
    {
        for (int i = nodesRoot.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(nodesRoot.GetChild(i).gameObject);
        }
        for (int i = connectionsRoot.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(connectionsRoot.GetChild(i).gameObject);
        }
    }

    private void AdjustContent()
    {
        float maxRadius = initialRadius + 50f;
        float diameter = maxRadius * 2f;

        content.sizeDelta = new Vector2(diameter, diameter);

        content.anchoredPosition = Vector2.zero;
        content.localScale = Vector3.one;
    }
}
