using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class NetManager : MonoBehaviour
{
    public string id;
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
        //foreach (var node in ){}
        var newNode = Instantiate(VariableManager.Instance.Node);
    }
    void NewChildNode(){
        foreach (var node in getAllNodes()){
            //if node is in "nodeSelectionRadius"
            if (((Vector2)node.transform.position - InputManager.Instance.mousePosWorld).magnitude > VariableManager.Instance.nodeSelectionRadius) continue;

            connectNewNode(node, out Node newNode, out Connection newConnection);
            newNode.nodeName = "child";
            newConnection.connectionName = "identifier";
        }
    }
    private void NodeSelected(){
        foreach (var node in getAllNodes()){
            //if node is in "nodeSelectionRadius"
            if (((Vector2)node.transform.position - InputManager.Instance.mousePosWorld).magnitude > VariableManager.Instance.nodeSelectionRadius) continue;
            
            //if node has an identifier called "selected" remove it else add it
            var selectedIdentifierConnections = GetIdentifierConnections(node, new string[]{"selected"});

            if (selectedIdentifierConnections.Count == 0){
                connectNewNode(node, out Node newNode, out Connection newConnection);
                newNode.nodeName = "selected";
                newConnection.connectionName = "identifier";
            }
            else {
                foreach (var selectedIdentifierConnection in selectedIdentifierConnections)
                {
                    selectedIdentifierConnection.outputNode.DeleteNode();
                    selectedIdentifierConnection.DeleteConnection();
                }
            }
        }
    }
    private void Load(){
        var clas = JsonManager.LoadClassFromJson<JsonNet>(Application.dataPath);
        var newNodes = new List<Node>();
        //Instantiate nodes
        foreach (var node in clas.nodes){
            var newNodeObj = Instantiate(VariableManager.Instance.Node);
            var newNode = newNodeObj.GetComponent<Node>();
            newNode.NodeConstructor(node.id, node.name, new List<Connection>());
            newNodes.Add(newNode);
        }
        //Instantiate connections
        var newConnections = new List<Connection>();
        foreach (var connection in clas.connections){
            var newconnectionObj = Instantiate(VariableManager.Instance.Connection);
            var newConnection = newconnectionObj.GetComponent<Connection>();
            
            var inputNode = newNodes.Find(x=>x.nodeId == connection.inputNodeId);
            var outputNode = newNodes.Find(x=>x.nodeId == connection.outputNodeId);
            
            newConnection.connectionConstructor(connection.id, connection.name, inputNode, outputNode);
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
                var nodeSprite = node.GetComponent<SpriteRenderer>();
                var jsonNode = new JsonNode
                {
                    id = node.nodeId,
                    name = node.nodeName,
                    colorR = nodeSprite.color.r,
                    colorG = nodeSprite.color.g,
                    colorB = nodeSprite.color.b,
                    colorA = nodeSprite.color.a, 
                    JsonConnectionIds = node.connections.Select(item => item.connectionId).ToArray(),
                };
                net.nodes[i] = jsonNode;
            }
            //Set connections
            var allConnections = getAllConnections();
            net.connections = new JsonConnection[allConnections.Length];
            for (int i = 0; i < net.connections.Length; i++)
            {
                var connection = allConnections[i];
                var connectionSprite = connection.GetComponent<LineRenderer>();
                var jsonConnection = new JsonConnection
                {
                    id = connection.connectionId,
                    name = connection.connectionName,
                    colorR = 255,
                    colorG = 0,
                    colorB = 0,
                    colorA = 0,
                    inputNodeId = connection.inputNode.nodeId,
                    outputNodeId = connection.outputNode.nodeId
                };
                //Collor
                net.connections[i] = jsonConnection;
            }
            return net;
        }
    }
    private void DragNode(){

    }
    private void DropNode(){

    }
    public void connectNewNode(Node node, out Node newNode, out Connection newConnection){
        //instantiate a new node
        var newNodeObj = Instantiate(VariableManager.Instance.Node, node.transform.position, quaternion.identity);
        //collor node red
        newNode = newNodeObj.GetComponent<Node>();
        newConnection = connectNodes(node, newNode);
    }
    public Connection connectNodes(Node inputNode = null, Node outputNode = null, string connectionName = "", string connectionId = ""){
            var newConnection = Instantiate(VariableManager.Instance.Connection).GetComponent<Connection>();
            newConnection.connectionConstructor(id, connectionName, inputNode, outputNode);
            if (inputNode != null) inputNode.connections.Add(newConnection);
            if (outputNode != null) outputNode.connections.Add(newConnection);
            return newConnection;
    }
    public List<Connection> GetIdentifierConnections(Node node, string[] identifiers){
        var identifierConnections = new List<Connection>();
        //loop through all identifiers
        foreach (var identifier in identifiers){
            //loop through all identifier connections
            foreach (var identifierConnection in node.connections.FindAll(x=>x.connectionName=="identifier")) {
                    //if the output node of the identifier connection matches the identifier, add it to "identifierConnection" 
                    if (identifierConnection.outputNode.nodeName==identifier) identifierConnections.Add(identifierConnection);
                }
        }
        return identifierConnections;
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
        InputManager.newChildNode += NewChildNode;
    }
    void OnDisable(){
        InputManager.newNode -= newNode;
        InputManager.nodeSelected -= NodeSelected;
        InputManager.load -= Load;
        InputManager.save -= Save;
        InputManager.newChildNode -= NewChildNode;

    }
}
