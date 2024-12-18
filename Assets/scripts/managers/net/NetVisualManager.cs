using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetVisualManager : I_Manager
{
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
	
	void Update()
	{
		ManageSprite();
		ManageConnectionLine();
	}
	private void ManageSprite(){
		foreach (var node in NetContentManager.inst.GetAllNodes()){
			var renderer = node.GetComponent<SpriteRenderer>();
			var selectedTList = NetContentManager.inst.nodeTypeLists.Find(x => x.listPath == "selected.tlist");
			if (selectedTList != null && selectedTList.nodes.Contains(node)) 
				renderer.color = Color.white;
			else renderer.color = Color.red; 
		}
	}
	public Connection NewConnection(DataNode outNode)
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

	public override void ManagerUpdate()
	{
		throw new System.NotImplementedException();
	}
}