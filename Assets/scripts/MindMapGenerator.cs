
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MindMapGenerator : MonoBehaviour
{
    [System.Serializable]
    public class Node
    {
        public string name;
        public Vector2 position;
        public List<Node> subNodes = new List<Node>();
    }

    public Node rootNode; // Root node
    public GameObject nodePrefab; // Prefab for nodes
    public LineRenderer linePrefab; // Prefab for lines
    public Transform canvasParent; // Parent object for UI elements

    private Dictionary<GameObject, Node> nodeMap = new Dictionary<GameObject, Node>();

    void Start()
    {
        if (rootNode != null)
        {
            CreateNode(rootNode, null);
        }
    }

    void CreateNode(Node nodeData, Transform parent)
    {
        // Create visual node
        GameObject node = Instantiate(nodePrefab, canvasParent);
        node.transform.localPosition = nodeData.position;
        node.name = nodeData.name;

        // Set the node text
        Text text = node.GetComponentInChildren<Text>();
        if (text != null)
        {
            text.text = nodeData.name;
        }

        // Save the mapping of GameObject to Node
        nodeMap[node] = nodeData;

        // Add button interactions
        Button addButton = node.transform.Find("AddButton").GetComponent<Button>();
        Button deleteButton = node.transform.Find("DeleteButton").GetComponent<Button>();

        addButton.onClick.AddListener(() => AddSubNode(node));
        deleteButton.onClick.AddListener(() => DeleteNode(node));

        // Draw line to parent if it exists
        if (parent != null)
        {
            LineRenderer line = Instantiate(linePrefab, canvasParent);
            line.SetPosition(0, parent.position);
            line.SetPosition(1, node.transform.position);
        }

        // Recursively create sub-nodes
        foreach (var subNode in nodeData.subNodes)
        {
            CreateNode(subNode, node.transform);
        }
    }

    void AddSubNode(GameObject parentNode)
    {
        Node parentNodeData = nodeMap[parentNode];

        // Create new node data
        Node newNode = new Node
        {
            name = "New Node",
            position = parentNodeData.position + new Vector2(100, -50) // Offset
        };

        parentNodeData.subNodes.Add(newNode);

        // Update the UI
        CreateNode(newNode, parentNode.transform);
    }

    void DeleteNode(GameObject node)
    {
        if (nodeMap.ContainsKey(node))
        {
            Node nodeData = nodeMap[node];

            // Find the parent node
            foreach (var kvp in nodeMap)
            {
                if (kvp.Value.subNodes.Contains(nodeData))
                {
                    kvp.Value.subNodes.Remove(nodeData);
                    break;
                }
            }

            // Remove the node
            Destroy(node);
            nodeMap.Remove(node);
        }
    }
}
