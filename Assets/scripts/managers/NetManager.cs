using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NetManager : MonoBehaviour
{
    public List<Node> allNodes;

    #region Singleton
    
    public static NetManager Instance { get; private set;}
    void SingletonizeThis()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    #endregion
    
    void Awake()
    {
        SingletonizeThis();
        allNodes = getAllNodes().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void newNode(){
        Instantiate(VariableManager.Instance.Node);
    }
    private void nodeSelected(){
        foreach (var node in allNodes){
            if (((Vector2)node.transform.position - InputManager.Instance.mousePosWorld).magnitude > VariableManager.Instance.nodeSelectionRadius) continue;
            var newNode = Instantiate(VariableManager.Instance.Node, node.transform.position, quaternion.identity);
            newNode.GetComponent<SpriteRenderer>().color = Color.red;
            var newConnection = Instantiate(VariableManager.Instance.Connection).GetComponent<Connection>();
            newConnection.connectNodes(node, newNode.GetComponent<Node>());
        }
    }
    private Node[] getAllNodes(){
        return GameObject.FindObjectsOfType<Node>();
    }
    void OnEnable(){
        InputManager.newNode += newNode;
        InputManager.nodeSelected += nodeSelected;
    }
    void OnDisable(){
        InputManager.newNode -= newNode;
        InputManager.nodeSelected -= nodeSelected;
    }
}
