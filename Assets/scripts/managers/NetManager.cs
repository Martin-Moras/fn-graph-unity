using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class NetManager : MonoBehaviour
{
	public string id;
	public List<Connection> identifierConnections;
	public List<Node> identifierNodes;
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
	void Update()
	{

	}
	public Node NewNode(string name = ""){
		var newNode = Instantiate(VariableManager.Instance.Node).GetComponent<Node>();
		newNode.nodeName = name;
		return newNode;
	}
	public void NewChildNode(){
		foreach (var node in GetAllNodes()){
			//if node is in "nodeSelectionRadius"
			if (((Vector2)node.transform.position - InputManager.Instance.mousePosWorld).magnitude > VariableManager.Instance.nodeSelectionRadius) continue;

			ConnectNewNode(node, out Node newNode, out Connection newConnection);
			newNode.nodeName = "child";
			newConnection.connectionName = "identifier";
		}
	}
	public void NodeSelected(){
		foreach (var node in GetAllNodes()){
			//if node is in "nodeSelectionRadius"
			if (((Vector2)node.transform.position - InputManager.Instance.mousePosWorld).magnitude > VariableManager.Instance.nodeSelectionRadius) continue;
			
			//if node has an identifier called "selected" remove it else add it
			var selectedIdentifierNodes = GetIdentifierNodes(new string[]{"selected"}, false);
			Node selectedIdentifierNode;
			if (selectedIdentifierNodes.Count == 0) selectedIdentifierNode = null;
			else selectedIdentifierNode = selectedIdentifierNodes[0];

			if (selectedIdentifierNode == null){
				AddIdentifiers(node, new string[]{"selected"}, true);
				return;
			}
			var selectConnections = GetConnections(node, selectedIdentifierNode);
			Connection selectConnection;
			if (selectConnections.Count == 0) selectConnection = null;
			else selectConnection = selectConnections[0];
			if (selectConnection == null){
				AddIdentifiers(node, new string[]{"selected"}, true);
			}
			else {
				DeleteConnection(selectConnection);
			}
		}
	}
	public List<Connection> AddIdentifiers(Node node, string[] identifiers, bool createIfDoesntExist) {
		var newIdConnections = new List<Connection>();

		foreach (var idNode in GetIdentifierNodes(identifiers, createIfDoesntExist))
		{
			newIdConnections.Add(ConnectNodes(node, idNode, "identifier"));
		}
		identifierConnections.AddRange(newIdConnections);
		return newIdConnections;
	}
	public List<Node> GetIdentifierNodes(string[] identifiers, bool createIfDoesntExist){
		var matchingIdentifierConnections = new List<Node>();
		//loop through all identifiers
		foreach (var identifier in identifiers){
			//get all Identifier connections where "outNode = identifier"
			var identifierNode = identifierNodes.FindAll(x=>x.nodeName == identifier);
			Node newIdNode;
			if (identifierNode.Count == 0 && createIfDoesntExist) {
				newIdNode = NewNode(identifier);
				identifierNode.Add(newIdNode); 
				identifierNodes.Add(newIdNode);
				//add to global, 
			} 
			matchingIdentifierConnections.AddRange(identifierNode);
		}
		return matchingIdentifierConnections;
	}
	public void Load(){
		var clas = JsonManager.LoadClassFromJson<JsonNet>(Application.dataPath);
		var newNodes = new List<Node>();
		//Instantiate nodes
		foreach (var node in clas.nodes){
			var newNodeObj = Instantiate(VariableManager.Instance.Node);
			var newNode = newNodeObj.GetComponent<Node>();
			newNode.NodeConstructor(node.id, node.nodeName);
			newNodes.Add(newNode);
		}
		//Instantiate connections
		var newConnections = new List<Connection>();
		foreach (var connection in clas.connections){
			var newconnectionObj = Instantiate(VariableManager.Instance.Connection);
			var newConnection = newconnectionObj.GetComponent<Connection>();
			
			var inputNode = newNodes.Find(x=>x.nodeId == connection.inputNodeId);
			var outputNode = newNodes.Find(x=>x.nodeId == connection.outputNodeId);
			
			newConnection.connectionConstructor(connection.id, connection.connectionName, inputNode, outputNode);
			newConnections.Add(newConnection);
			//add connction to "identifierConnections"
			if (newConnection.connectionName == "identifier") identifierConnections.Add(newConnection);
		}
		//add newNodes to identifierNodes
		foreach(var node in newNodes){
			if (!identifierConnections.Exists(x=>x.outNode == node)) continue;
			identifierNodes.Add(node);
		}
	}
	public void Save(){
		var d = JsonManager.SaveClassAsJson(CreateJsonNet(), Application.dataPath);
		
		JsonNet CreateJsonNet(){
			var net = new JsonNet();
			net.name = "name";
			//Set nodes
			var allNodes = GetAllNodes();
			net.nodes = new JsonNode[allNodes.Length];
			for (int i = 0; i < net.nodes.Length; i++)
			{
				var node = allNodes[i];
				var nodeSprite = node.GetComponent<SpriteRenderer>();
				var jsonNode = new JsonNode
				{
					id = node.nodeId,
					nodeName = node.nodeName,
					colorR = nodeSprite.color.r,
					colorG = nodeSprite.color.g,
					colorB = nodeSprite.color.b,
					colorA = nodeSprite.color.a, 
				};
				net.nodes[i] = jsonNode;
			}
			//Set connections
			var allConnections = GetConnections();
			net.connections = new JsonConnection[allConnections.Count];
			for (int i = 0; i < net.connections.Length; i++)
			{
				var connection = allConnections[i];
				var connectionSprite = connection.GetComponent<LineRenderer>();
				var jsonConnection = new JsonConnection
				{
					id = connection.connectionId,
					connectionName = connection.connectionName,
					colorR = 255,
					colorG = 0,
					colorB = 0,
					colorA = 0,
					inputNodeId = connection.inNode.nodeId,
					outputNodeId = connection.outNode.nodeId
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
	public void ConnectNewNode(Node node, out Node newNode, out Connection newConnection){
		//instantiate a new node
		var newNodeObj = Instantiate(VariableManager.Instance.Node, node.transform.position, quaternion.identity);
		//collor node red
		newNode = newNodeObj.GetComponent<Node>();
		newConnection = ConnectNodes(node, newNode);
	}
	public Connection ConnectNodes(Node inputNode = null, Node outputNode = null, string connectionName = "", string connectionId = ""){
			var newConnection = Instantiate(VariableManager.Instance.Connection).GetComponent<Connection>();
			newConnection.connectionConstructor(id, connectionName, inputNode, outputNode);
			return newConnection;
	}
	public void DeleteConnection(Connection connection){
		identifierConnections.Remove(connection);
		Destroy(connection.gameObject);
	}
	public void DeleteNode(Node node, bool inConnection, bool outConnection){
		foreach (var connection in GetConnections())
		{
			if (connection.inNode == node){
				if (inConnection) Destroy(connection.gameObject);
				else connection.inNode = NewNode("empty");
			}
			if (connection.outNode == node){
				if (outConnection) Destroy(connection.gameObject);
				else connection.outNode = NewNode("empty");
			}
		}
		identifierNodes.Remove(node);
		Destroy(node.gameObject);
	}
	public Node[] GetAllNodes(){
		return GameObject.FindObjectsOfType<Node>();
	}
	public List<Connection> GetConnections(Node inNode = null, Node outNode = null){
		//this funktion returns all connections that have "inNode" as input node and "outNode" as output node. If both are null this funktion returnes all connections
		var allConnections = GameObject.FindObjectsOfType<Connection>().ToList();
		var matchingConnections = new List<Connection>();

		if (inNode == null && outNode == null) return allConnections;
		foreach (var connection in allConnections)
		{
			if (inNode != null && connection.inNode != inNode) continue; 
			if (outNode != null && connection.outNode != outNode) continue;

			matchingConnections.Add(connection);
		}
		return matchingConnections;
	}
}

