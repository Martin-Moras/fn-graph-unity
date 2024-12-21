using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
	public override void Initialize()
	{
	}
	
	public override void ManagerUpdate()
	{
		VisualizeNewNodes();
		DeVidualizeNodes();
		ChangeVisualNodes();
		ManageSprite();
		ManageConnectionLine();
	}
	private void FixedUpdate() {
		ManageConnectionForces();
	}
	private void VisualizeNewNodes()
	{
		VisualizeNodes(GameManager.inst.netContentManager.thisFrame_newDataNodes);
	}
	private void DeVidualizeNodes()
	{
		foreach (var dataNode in GameManager.inst.netContentManager.thisFrame_deletedDataNodes) {
			var visualNode = GetVisualNodeByDataNode(dataNode);
			if (visualNode == null)
				continue;
			DeleteVisualNode(visualNode);
		}
	}
	private void ChangeVisualNodes()
	{
		foreach (var dataNode in GameManager.inst.netContentManager.thisFrame_changedNodes) {
			var visualNode = GetVisualNodeByDataNode(dataNode);
			var newConnectedDataNodes = dataNode.connectedNodes.Except(visualNode.connections.Select(x => x.outNode.dataNode));
			//find all connections in "visualNode.connections" which aren't contained in "dataNode.connectedNodes"
			var deletedConnections = visualNode.connections.FindAll(connection=> !dataNode.connectedNodes.Contains(connection.outNode.dataNode));

			foreach (var newConnectedDataNode in newConnectedDataNodes) {
				HandleNodeConnection(visualNode, GetVisualNodeByDataNode(newConnectedDataNode));
			}
			foreach (var deletedConnection in deletedConnections) {
				HandleNodeConnection(visualNode, deletedConnection.outNode, ConnectType.Disconnect);
			}
			visualNode.SetText(dataNode.nodePath);
		}
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
				i++;
			}
			return newVisualNodes;
		}
		void ConnectNewVisualNodes(List<VisualNode> visualNodeToConnect)
		{
			foreach (var newNode in visualNodeToConnect) {
				ConnectOtherNodesTo(newNode);
				ConnectToOtherNodes(newNode);
			}
		}
	}
	private VisualNode GetVisualNodeByDataNode(DataNode dataNode)
	{
		var visualNode = allVisualNodes.Find(x=>x.dataNode == dataNode);
		return visualNode;
	}
	private VisualNode DataNodeToVisualNode(DataNode dataNode, Vector2 pos, bool connectToOther, bool connectOtersToThis)
	{
		var newVisualNode = Object.Instantiate(GameManager.inst.variableManager.NodePrefab, (Vector3)pos, Quaternion.identity).GetComponent<VisualNode>();
		newVisualNode.NodeConstructor(dataNode, null);
		if (connectToOther)
			ConnectToOtherNodes(newVisualNode);
		if (connectOtersToThis)
			ConnectOtherNodesTo(newVisualNode);
		thisFrame_newVisualNodes.Add(newVisualNode);
		allVisualNodes.Add(newVisualNode);
		return newVisualNode;
	}
	private void DeleteVisualNode(VisualNode node)
	{
		DisconnectOtherNodesFrom(node);
		foreach (var connectedConnection in node.connections) {
			DeleteConection(connectedConnection);
		}
		thisFrame_DeletedVisualNodes.Add(node);
		Object.Destroy(node);
	}
	private List<VisualNode> ConnectToOtherNodes(VisualNode node)
	{
		//Find all VisualNodes which node connects to
		var outGoingConnectionVisualNodes = allVisualNodes.FindAll(visualNode=>
			node.dataNode.connectedNodes.Contains(visualNode.dataNode));

		foreach (var outGoingConnectionVisualNode in outGoingConnectionVisualNodes) {
			HandleNodeConnection(node, outGoingConnectionVisualNode);
		}
		return outGoingConnectionVisualNodes;
	}
	private void DisconnectOtherNodesFrom(VisualNode node)
	{
		foreach (var visualNode in allVisualNodes) {
			HandleNodeConnection(visualNode, node, ConnectType.Disconnect);
		}
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
			// var selectedTList = GameManager.inst.netContentManager.nodeTypeLists.Find(x => x.listPath == "selected.tlist");
			// if (selectedTList != null && selectedTList.nodes.Contains(node)) 
			// 	renderer.color = Color.white;
			// else renderer.color = Color.red; 
		}
	}
	private void ManageConnectionLine()
	{
		foreach (var node in allVisualNodes){
			foreach (var connection in node.connections) {
				connection.line.SetPositions(new Vector3[]{node.transform.position, connection.outNode.transform.position});
			}
		}
	}
	public Connection HandleNodeConnection(VisualNode fromNode, VisualNode toNode, ConnectType connectType = ConnectType.Connect)
	{
		switch (connectType) {
			case ConnectType.Connect:
				Connection newConnection = null;
				if (!fromNode.connections.Exists(x=>x.outNode == toNode)) {
					newConnection = CreateConnection(toNode);
					fromNode.connections.Add(newConnection);
				}
				return newConnection;
			case ConnectType.Disconnect:
				var connection = fromNode.connections.Find(x=>x.outNode == toNode);
				if (connection == null)
					return null;
				DeleteConection(connection);
				fromNode.connections.Remove(connection);
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
	private Connection CreateConnection(VisualNode outNode)
	{
		var newConnetion = Object.Instantiate(GameManager.inst.variableManager.ConnectionPrefab).GetComponent<Connection>();
		newConnetion.Constructor(outNode);
		allConnections.Add(newConnetion);
		thisFrame_newConnections.Add(newConnetion);
		return newConnetion;
	}
	private void DeleteConection(Connection connection)
	{
		thisFrame_DeletedConnections.Add(connection);
		Object.Destroy(connection);
	}
	/// <summary>
	/// applyes a spring force to each connection between nodes
	/// </summary>
	private void ManageConnectionForces(){
		foreach (var node in allVisualNodes)
		{
			foreach (var connectedConnection in node.connections)
			{
				var relativeNodePos = connectedConnection.outNode.transform.position - node.transform.position;
				var nodeDistance = relativeNodePos.magnitude;
				//the difference between node distance and how far they should be apart
				var offsetFromSpringRestLenght = nodeDistance - springRestLenght;
				//get rigitbodys from node and connectedNode
				var connectedNodeRb = connectedConnection.outNode.GetComponent<Rigidbody2D>();
				var nodeRb = node.GetComponent<Rigidbody2D>();
				//get the velocity relative to the direction of the other node
				var nodeSpringVel = Vector2.Dot(nodeRb.linearVelocity, relativeNodePos.normalized);
				var connectedNodeSpringVel = Vector2.Dot(connectedNodeRb.linearVelocity, relativeNodePos.normalized);
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