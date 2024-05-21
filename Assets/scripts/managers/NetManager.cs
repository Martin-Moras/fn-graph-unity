using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class NetManager : MonoBehaviour
{
	public string id;
	public List<ConnectionTypeList> connectionTypeLists;
	public List<NodeTypeList> nodeTypeLists;
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
	#region Delete
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
	# endregion
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

	#region Json
	public void Load()
    {
        var clas = JsonManager.LoadClassFromJson<JsonNet>(Application.dataPath);
        var newNodes = new List<Node>();
        InstantiateNodes(clas, newNodes);
        InstantiateConnections(clas, newNodes);
		AddNodeTypeLists();
		AddConnectionTypeLists();

        UpdateTypeLists();

		void InstantiateNodes(JsonNet clas, List<Node> newNodes)
        {
            foreach (var node in clas.nodes)
            {
                var newNodeObj = Instantiate(VariableManager.Instance.Node);
                var newNode = newNodeObj.GetComponent<Node>();
                newNode.NodeConstructor(node.id, node.nodeName);
                newNodes.Add(newNode);
            }
        }
        void InstantiateConnections(JsonNet clas, List<Node> newNodes)
        {
            foreach (var connection in clas.connections)
            {
                var newconnectionObj = Instantiate(VariableManager.Instance.Connection);
                var newConnection = newconnectionObj.GetComponent<Connection>();

                var inputNode = newNodes.Find(x => x.nodeId == connection.inputNodeId);
                var outputNode = newNodes.Find(x => x.nodeId == connection.outputNodeId);

                newConnection.connectionConstructor(connection.id, connection.connectionName, inputNode, outputNode);
            }
        }
		void AddConnectionTypeLists(){
			foreach (var connectionTypeList in clas.connectionTypeLists)
			{
				var existingTypeList = connectionTypeLists.Find(x=>x.listName == connectionTypeList.name);
				if (existingTypeList == null) connectionTypeLists.Add(JsonTypeListToTypeList(connectionTypeList));
				else existingTypeList = JsonTypeListToTypeList(connectionTypeList);
			}

			ConnectionTypeList JsonTypeListToTypeList(JsonConnectionTypeList connectionTypeList){
				Color color;
				color.r = connectionTypeList.color_r;
				color.g = connectionTypeList.color_g;
				color.b = connectionTypeList.color_b;
				color.a = connectionTypeList.color_a;
				
				return new ConnectionTypeList(
						connectionTypeList.name,
						connectionTypeList.id,
						null,
						color,
						connectionTypeList.connectionName,
						connectionTypeList.inNodeName,
						connectionTypeList.inNodeId,
						connectionTypeList.outNodeName,
						connectionTypeList.outNodeId
				);
			}
		}
		void AddNodeTypeLists(){
			foreach (var nodeTypeList in clas.nodeTypeLists)
			{
				var existingTypeList = nodeTypeLists.Find(x=>x.listName == nodeTypeList.name);
				if (existingTypeList == null) nodeTypeLists.Add(JsonTypeListToTypeList(nodeTypeList));
				else existingTypeList = JsonTypeListToTypeList(nodeTypeList);
			}

			NodeTypeList JsonTypeListToTypeList(JsonNodeTypeList nodeTypeList){
				Color color;
				color.r = nodeTypeList.color_r;
				color.g = nodeTypeList.color_g;
				color.b = nodeTypeList.color_b;
				color.a = nodeTypeList.color_a;
				
				return new NodeTypeList(
						nodeTypeList.name,
						nodeTypeList.id,
						null,
						color,
						nodeTypeList.nodeName,
						nodeTypeList.inGoingConnectionName,
						nodeTypeList.inGoingConnectionId,
						nodeTypeList.outGoingConnectionName,
						nodeTypeList.outGoingConnectionId
				);
			}
		}
    }
    public void Save(){
		var d = JsonManager.SaveClassAsJson(CreateJsonNet(), Application.dataPath);
		
		JsonNet CreateJsonNet()
        {
            var net = new JsonNet();
            net.name = "name";
            
			AddJsonNodes();
            AddJsonConnections();
			AddJsonNodeTypeLists();
			AddJsonConncetionTypeLists();
            return net;

            void AddJsonNodes()
            {
                //Set nodes
                var allNodes = GetAllNodes();
                net.nodes = new JsonNode[allNodes.Length];
                for (int i = 0; i < net.nodes.Length; i++)
                {
                    var node = allNodes[i];
                    var jsonNode = new JsonNode
                    {
                        id = node.nodeId,
                        nodeName = node.nodeName,
                    };
                    net.nodes[i] = jsonNode;
                }
            }
            void AddJsonConnections()
            {
                //Set connections
                var allConnections = this.GetConnections();
                net.connections = new JsonConnection[allConnections.Count];
                for (int i = 0; i < net.connections.Length; i++)
                {
                    var connection = allConnections[i];
                    var connectionSprite = connection.GetComponent<LineRenderer>();
                    net.connections[i] = new JsonConnection
                    {
                        id = connection.connectionId,
                        connectionName = connection.connectionName,
                        inputNodeId = connection.inNode.nodeId,
                        outputNodeId = connection.outNode.nodeId
                    };
                }
            }
			void AddJsonConncetionTypeLists(){
				net.connectionTypeLists = new JsonConnectionTypeList[connectionTypeLists.Count];
                for (int i = 0; i < net.connectionTypeLists.Length; i++)
                {
                    var connectionTypeList = connectionTypeLists[i];
                    
                    net.connectionTypeLists[i] = new JsonConnectionTypeList{
						name = connectionTypeList.listName,
						id = connectionTypeList.listId,
						connectionIds = connectionTypeList.connections.Select(x=>x.connectionId).ToArray(),
						color_r = connectionTypeList.color.r,
						color_g = connectionTypeList.color.g,
						color_b = connectionTypeList.color.b,
						color_a = connectionTypeList.color.a,
						connectionName = connectionTypeList.connectionName,
						inNodeName = connectionTypeList.inNodeName,
						inNodeId = connectionTypeList.inNodeId,
						outNodeName = connectionTypeList.outNodeName,
						outNodeId = connectionTypeList.outNodeId,
					};
                }
			}
			void AddJsonNodeTypeLists(){
				net.nodeTypeLists = new JsonNodeTypeList[nodeTypeLists.Count];
                for (int i = 0; i < net.nodeTypeLists.Length; i++)
                {
                    var nodeTypeList = nodeTypeLists[i];
                    
                    net.nodeTypeLists[i] = new JsonNodeTypeList{
						name = nodeTypeList.listName,
						id = nodeTypeList.listId,
						nodeIds = nodeTypeList.nodes.Select(x=>x.nodeId).ToArray(),
						color_r = nodeTypeList.color.r,
						color_g = nodeTypeList.color.g,
						color_b = nodeTypeList.color.b,
						color_a = nodeTypeList.color.a,
						nodeName = nodeTypeList.nodeName,
						inGoingConnectionName = nodeTypeList.inGoingConnectionName,
						inGoingConnectionId = nodeTypeList.inGoingConnectionId,
						outGoingConnectionName = nodeTypeList.outGoingConnectionName,
						outGoingConnectionId = nodeTypeList.outGoingConnectionId,
					};
                }
			}
		}
    }
	#endregion
	#region TypeList
	public void UpdateTypeLists(){
		TryAddToConnectionTypeList(GetConnections().ToArray(), connectionTypeLists.ToArray());
		TryAddToNodeTypeList(GetAllNodes(), nodeTypeLists.ToArray());
	}
	public List<ConnectionTypeList> TryAddToConnectionTypeList(Connection[] connections, ConnectionTypeList[] typeLists = null){
		ConnectionTypeList[] listsToCheck;
		if (typeLists == null) listsToCheck = connectionTypeLists.ToArray();
		else listsToCheck = typeLists.ToArray();
		var outTypeLists = new List<ConnectionTypeList>();

		foreach (var typeList in listsToCheck)
		{
			foreach (var connection in connections)
			{
				if (typeList.connectionName != connection.connectionName) continue;
				if (typeList.inNodeName != connection.inNode.nodeName) continue;
				if (typeList.inNodeId != connection.inNode.nodeId) continue;
				if (typeList.outNodeName != connection.outNode.nodeName) continue;
				if (typeList.outNodeId != connection.outNode.nodeId) continue;
				//if requirements are met
				typeList.connections.Add(connection);
			}
		}
		return outTypeLists;
	}
	public List<NodeTypeList> TryAddToNodeTypeList(Node[] nodes, NodeTypeList[] typeLists = null){
		NodeTypeList[] listsToCheck;
		if (typeLists == null) listsToCheck = nodeTypeLists.ToArray();
		else listsToCheck = typeLists.ToArray();
		var outTypeLists = new List<NodeTypeList>();

		foreach (var typeList in listsToCheck)
		{
			foreach (var node in nodes)
			{
				if (typeList.nodeName != node.nodeName) continue;
				if (GetConnections(node).Exists(x=>x.connectionName == typeList.inGoingConnectionName)) continue;
				if (GetConnections(node).Exists(x=>x.connectionId == typeList.inGoingConnectionId)) continue;
				if (GetConnections(outNode: node).Exists(x=>x.connectionName == typeList.outGoingConnectionName)) continue;
				if (GetConnections(outNode: node).Exists(x=>x.connectionId == typeList.inGoingConnectionId)) continue;
				//if requirements are met
				typeList.nodes.Add(node);
			}
		}
		return outTypeLists;
	}
	public List<Connection> AddIdentifiers(Node node, string[] identifiers, bool createIfDoesntExist) {
		var newIdConnections = new List<Connection>();

		foreach (var idNode in GetIdentifierNodes(identifiers, createIfDoesntExist))
		{
			newIdConnections.Add(ConnectNodes(node, idNode, "identifier"));
			// TODO : Implement Identifiers into the new TypeList system.
			//		  Make "AddIdentifiers" and "GetIdentifierNodes" usable for all typeLists.
			//		  have fun!!!
			TryAddToConnectionTypeList(newIdConnections.ToArray(), new ConnectionTypeList[]{connectionTypeLists.Find(x=>x.listName == )});
		}
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
	
	#endregion
}

