using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class NetManager : MonoBehaviour
{
    public string id ;
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
    }

    // Update is called once per frame
    void Update()
    {

    }
    void newNode(){
        var newNode =Instantiate(VariableManager.Instance.Node);
    }
    private void NodeSelected(){
    
        foreach (var node in getAllNodes()){
            //if node is in "nodeSelectionRadius"
            if (((Vector2)node.transform.position - InputManager.Instance.mousePosWorld).magnitude > VariableManager.Instance.nodeSelectionRadius) continue;

            //connectNewNode(node);
            node.identifiers.Add("selected");
            connectNewNode(node);
        }
    }
    private void Load(){
        var clas = JsonManager.LoadClassFromJson<JsonNet>(Application.dataPath);
        var newNodes = new List<Node>();
        //Instantiate nodes
        foreach (var node in clas.nodes){
            var newNodeObj = Instantiate(VariableManager.Instance.Node);
            var newNode = newNodeObj.GetComponent<Node>();
            newNode.NodeConstructor(node.id, node.identifiers.ToList(), new List<Connection>());
            newNodes.Add(newNode);
        }
        //Instantiate connections
        var newConnections = new List<Connection>();
        foreach (var connection in clas.connections){
            var newconnectionObj = Instantiate(VariableManager.Instance.Connection);
            var newConnection = newconnectionObj.GetComponent<Connection>();
            var asd = newNodes.Find(x=>x.id == connection.inputNodeId);
            var das = newNodes.Find(x=>x.id == connection.outputNodeId);
            newConnection.connectionConstructor(connection.id, connection.identifiers.ToList(), asd, das);
            newConnections.Add(newConnection);
        }
        //connection to "node.connections)
        foreach (var node in newNodes){
            node.connections.AddRange(newConnections.FindAll(x=>x.inputNode == node));
            node.connections.AddRange(newConnections.FindAll(x=>x.outputNode == node));
        }
    }
    
    private void Save(){
        var d = JsonManager.SaveClassAsJson(CreateJsonNet(), Application.dataPath);
        JsonNet CreateJsonNet(){
        var net = new JsonNet();
        net.name = "name";
        //Set nodes
        var allNodes = getAllNodes();
        net.nodes = new JsonNode[allNodes.Length];
        for (int i = 0; i < net.nodes.Length; i++)
        {
            var node = allNodes[i];
            var jsonNode = new JsonNode();
            jsonNode.id = node.id;
            //Collor
            var nodeSprite = node.GetComponent<SpriteRenderer>();
            jsonNode.colorR = nodeSprite.color.r;
            jsonNode.colorG = nodeSprite.color.g;
            jsonNode.colorB = nodeSprite.color.b;
            jsonNode.colorA = nodeSprite.color.a; 

            jsonNode.identifiers = node.identifiers.ToArray();
            jsonNode.JsonConnectionIds = node.connections.Select(item => item.id).ToArray();;

            net.nodes[i] = jsonNode;
        }
        //Set connections
        var allConnections = getAllConnections();
        net.connections = new JsonConnection[allConnections.Length];
        for (int i = 0; i < net.connections.Length; i++)
        {
            var Connection = allConnections[i];
            var jsonConnection = new JsonConnection();
            jsonConnection.id = Connection.id;
            //Collor
            var connectionSprite = Connection.GetComponent<LineRenderer>();
            jsonConnection.colorR = 255;
            jsonConnection.colorG = 0;
            jsonConnection.colorB = 0;
            jsonConnection.colorA = 0; 
        

            jsonConnection.identifiers = Connection.identifiers.ToArray();
            jsonConnection.inputNodeId = Connection.inputNode.id;
            jsonConnection.outputNodeId = Connection.outputNode.id;

            net.connections[i] = jsonConnection;
        }
        
        return net;
    }
    }
    
    
    public void connectNewNode(Node node){
        //instantiate a new node
            var newNode = Instantiate(VariableManager.Instance.Node, node.transform.position, quaternion.identity);
            //collor node red
            newNode.GetComponent<SpriteRenderer>().color = Color.red;
            
            connectNodes(node, newNode.GetComponent<Node>());
    }
    public Connection connectNodes(Node inputNode = null, Node outputNode = null){
            var newConnection = Instantiate(VariableManager.Instance.Connection).GetComponent<Connection>();
            newConnection.connectNodes(inputNode, outputNode);
            return newConnection;
    }
    public Node[] getAllNodes(){
        return GameObject.FindObjectsOfType<Node>();
    }
    public Connection[] getAllConnections(){
        return GameObject.FindObjectsOfType<Connection>();
    }
    void OnEnable(){
        InputManager.newNode += newNode;
        InputManager.nodeSelected += NodeSelected;
        InputManager.load += Load;
        InputManager.save += Save;
    }
    void OnDisable(){
        InputManager.newNode -= newNode;
        InputManager.nodeSelected -= NodeSelected;
        InputManager.load -= Load;
        InputManager.save -= Save;
    }
}
