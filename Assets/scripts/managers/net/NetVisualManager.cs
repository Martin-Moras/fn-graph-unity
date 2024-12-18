using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NetVisualManager : I_Manager
{
	public List<VisualNode> allVisualNodes = new();
	public List<Connection> allConnections = new();
	public List<VisualNode> thisFrame_newVisualNodes = new();
	public List<VisualNode> thisFrame_DeletedVisualNodes = new();
	public List<Connection> thisFrame_newConnections = new();
	public List<Connection> thisFrame_DeletedConnections = new();
	public float springStiffness;
	//how long the spring should be
	public float springRestLenght;
	public float springDamping;
	#region Singleton
	public static NetVisualManager inst { get; private set;}
	public override void SingletonizeThis()
	{
		if (inst != null && inst != this) Destroy(this);
		else inst = this;
	}
	#endregion
	public override void Initialize()
	{
	}
	
	public override void ManagerUpdate()
	{
		VisualizeNewNodes();
		ManageSprite();
		ManageConnectionLine();
	}
	private void VisualizeNewNodes()
	{
		VisualizeNodes(NetContentManager.inst.thisFrame_newDataNodes);
	}	

	private List<VisualNode> VisualizeNodes(List<DataNode> nodesToVisualize)
	{
		var newVisualNodes = CreateVisualNodes(nodesToVisualize);
		ConnectNewVisualNodes(newVisualNodes);

		return newVisualNodes;

		List<VisualNode> CreateVisualNodes(List<DataNode> nodesToVisualize) 
		{
			float i = 0;
			var newVisualNodes = new List<VisualNode>();
			
			foreach(var node in nodesToVisualize) {
				var newVisualNode = DataNodeToVisualNode(node, Vector2.one * i / 3, false, false);

				newVisualNodes.Add(newVisualNode);
				thisFrame_newVisualNodes.Add(newVisualNode);
				allVisualNodes.Add(newVisualNode);
				i++;
			}
			return newVisualNodes;
		}
		void ConnectNewVisualNodes(List<VisualNode> visualNodeToConnect)
		{
			foreach (var newNode in newVisualNodes) {
				ConnectOtherNodesTo(newNode);
				ConnectToOtherNodes(newNode);
			}
		}
	}
	private VisualNode DataNodeToVisualNode(DataNode dataNode, Vector2 pos, bool connectToOther, bool connectOtersToThis)
	{
		var visNode = Instantiate(VariableManager.inst.NodePrefab, (Vector3)pos, Quaternion.identity).GetComponent<VisualNode>();
		visNode.NodeConstructor(dataNode, null);
		if (connectToOther)
			ConnectToOtherNodes(visNode);
		if (connectOtersToThis)
			ConnectOtherNodesTo(visNode);
		return visNode;
	}
	private List<VisualNode> ConnectToOtherNodes(VisualNode node)
	{
		//Find all VisualNodes which node connects to
		var outGoingConnectionVisualNodes = allVisualNodes.FindAll(x=>
			node.connections.Exists(nodeConnection=>
			nodeConnection.outNode.dataNode == x.dataNode));

		foreach (var outGoingConnectionVisualNode in outGoingConnectionVisualNodes) {
			HandleNodeConnection(node, outGoingConnectionVisualNode);
		}
		return outGoingConnectionVisualNodes;
	}
	private List<VisualNode> ConnectOtherNodesTo(VisualNode node)
	{
		//find all nodes that connect to node
		var inGoingConnectionNodes = allVisualNodes.FindAll(x => x.dataNode.connectedNodes.Contains(node.dataNode));

		foreach (var inGoingConnectionNode in inGoingConnectionNodes) {
			HandleNodeConnection(inGoingConnectionNode, node);
		}
		return inGoingConnectionNodes;
	}
	private void ManageSprite(){
		foreach (var node in allVisualNodes) {
			node.sprite.color = Color.red;
			// var renderer = node.GetComponent<SpriteRenderer>();
			// var selectedTList = NetContentManager.inst.nodeTypeLists.Find(x => x.listPath == "selected.tlist");
			// if (selectedTList != null && selectedTList.nodes.Contains(node)) 
			// 	renderer.color = Color.white;
			// else renderer.color = Color.red; 
		}
	}
	public Connection NewConnection(VisualNode outNode)
	{
		var newConnetion = Instantiate(VariableManager.inst.ConnectionPrefab).GetComponent<Connection>();
		newConnetion.outNode = outNode;
		return newConnetion;
	}
	private void ManageConnectionLine(){
		foreach (var node in NetContentManager.inst.GetAllNodes()){
			foreach (var connection in node.connections)
			{
				connection.line.SetPositions(new Vector3[]{node.transform.position, connection.outNode.transform.position});
			}
		}
	}
	private void ManageConnections(){
		foreach (var node in GetAllNodes().ToList()) {
			for (int i = 0; i < node.connectedNodes.Count; i++) {
				node.connections[i].line.SetPositions(new[]{node.transform.position, node.connectedNodes[i].transform.position});
			}
		}
	}
	public Connection HandleNodeConnection(VisualNode fromNode, VisualNode toNode, ConnectType connectType = ConnectType.Connect)
	{
		switch (connectType) {
			case ConnectType.Connect:
				Connection newConnection = null;
				if (!fromNode.connections.Exists(x=>x.outNode == toNode)) {
					newConnection = NewConnection(toNode);
					fromNode.connections.Add(newConnection);
					thisFrame_newConnections.Add(newConnection);
				}
				return newConnection;
			case ConnectType.Disconnect:
				var connection = fromNode.connections.Find(x=>x.outNode == toNode);
				fromNode.connections.Remove(connection);
				thisFrame_DeletedConnections.Add(connection);
				return connection;
			case ConnectType.Toggle:
				if (fromNode.connections.Exists(x=>x == toNode))
					return HandleNodeConnection(fromNode, toNode, ConnectType.Disconnect);
				else
					return HandleNodeConnection(fromNode, toNode, ConnectType.Connect);
			default:
				return null;
		}
	}
	
	/// <summary>
	/// applyes a spring force to each connection between nodes
	/// </summary>
		private void ManageConnectionForces(){
		foreach (var node in NetContentManager.inst.GetAllNodes())
		{
			foreach (var connectedNode in node.connectedNodes)
			{
				var relativeNodePos = connectedNode.transform.position - node.transform.position;
				var nodeDistance = relativeNodePos.magnitude;
				//the difference between node distance and how far they should be apart
				var offsetFromSpringRestLenght = nodeDistance - springRestLenght;
				//get rigitbodys from node and connectedNode
				var connectedNodeRb = connectedNode.GetComponent<Rigidbody2D>();
				var nodeRb = node.GetComponent<Rigidbody2D>();
				//get the velocity relative to the direction of the other node
				var nodeSpringVel = Vector2.Dot(nodeRb.velocity, relativeNodePos.normalized);
				var connectedNodeSpringVel = Vector2.Dot(connectedNodeRb.velocity, relativeNodePos.normalized);
				//springVel shows how fast the spring is expanding/retracting
				var springVel = nodeSpringVel - connectedNodeSpringVel;
				//apply the damping coeficent to springVel
				var dampingForce = springVel * springDamping;
				
				//calcualte the force that should be applied both nodes
				var springForce = offsetFromSpringRestLenght * springStiffness - dampingForce;
				//apply spring force
				nodeRb.AddForce(relativeNodePos.normalized * springForce);
				connectedNodeRb.AddForce(relativeNodePos.normalized * -springForce);
			}
		}
	}
}