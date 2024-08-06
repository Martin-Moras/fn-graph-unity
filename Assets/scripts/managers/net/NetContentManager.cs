using System;
using MathNet.Numerics.Interpolation;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.AI;
using Unity.VisualScripting;


public class NetContentManager : MonoBehaviour
{
	public string id;
	public List<NodeTypeList> nodeTypeLists = new();
	#region Singleton
	
	public static NetContentManager Instance { get; private set;}
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
	
	public Node NewNode(string path = ""){
		var newNode = Instantiate(VariableManager.Instance.NodePefab).GetComponent<Node>();
		
		if (path == "" || GetAllNodes().ToList().Exists(x=>x.nodePath == path))
			path += UnityEngine.Random.Range(0, 100000) + ".node"; 
		newNode.NodeConstructor(path, new List<Node>(), new List<Connection>());
		return newNode;
	}
	
	
	private void DragNode(){

	}
	private void DropNode(){
/*
create/delete nodes/connections
type lists + behaviour
(dis)connect nodes
behaviour
	nodes repell each other
	connections pull and push
	connections limit the angle a connected node can be at
	node summary
*/
	}
	public void ConnectNewNode(Node node, out Node newNode){
		//instantiate a new node
		var newNodeObj = Instantiate(VariableManager.Instance.NodePefab, node.transform.position, quaternion.identity);
		newNode = newNodeObj.GetComponent<Node>();
		ConnectNodes(node, newNode);
	}
	public void ConnectNodes(Node inputNode, Node outputNode, bool updateTypeLists = true){
		inputNode.connectedNodes.Add(outputNode);
		inputNode.connections.Add(NewConnection(outputNode));
		if (updateTypeLists) UpdateTypeLists();
	}
	public Connection NewConnection(Node outNode){
		var newConnetion = Instantiate(VariableManager.Instance.ConnectionPrefab).GetComponent<Connection>();
		newConnetion.outNode = outNode;
		return newConnetion;
	}
	public void DisconnectNodes(Node inputNode, Node outputNode){
		inputNode.connectedNodes.Remove(outputNode);
		var connection = inputNode.connections.Find(x=>x.outNode == outputNode);
		inputNode.connections.Remove(connection);
		Destroy(connection.gameObject);

		UpdateTypeLists();
	}
	public Node GetNode(string nodePath, bool createIfDoesntExist, bool tryAddToNodesTypeLists){
		Node newSelectedNode = GetAllNodes().ToList().Find(x=>x.nodePath == nodePath);
			if (newSelectedNode == null && createIfDoesntExist)
				newSelectedNode = NewNode(nodePath);
			if (tryAddToNodesTypeLists) TryAddToNodesTypeLists(new[]{newSelectedNode});
		return newSelectedNode;
	}
	private void ManageConnections(){
		foreach (var node in GetAllNodes().ToList().Select(x=>x)){
			for (int i = 0; i < node.connectedNodes.Count; i++){
				 
				node.connections[i].line.SetPositions(new[]{node.transform.position, node.connectedNodes[i].transform.position});
			}
		}
	}
	#region Delete
	/*public void DeleteConnection(Connection connection){

		foreach (var typeList in connectionTypeLists)
		{
			typeList.connections.Remove(connection);
		}
		Destroy(connection.gameObject);
	}*/
	public void DeleteNode(Node node){
		//Remove all in going connections
		foreach (var refNode in GetAllNodes())
		{
			if (!refNode.connectedNodes.Contains(node)) continue;
			refNode.connectedNodes.Remove(node);
			refNode.connections.RemoveAll(x=>x.outNode == node);
		}

		foreach (var connectedNode in node.connectedNodes)
		{
			DisconnectNodes(node, connectedNode);
		}
		//Manage typeLists
		foreach (var typeList in nodeTypeLists)
		{
			typeList.nodes.Remove(node);
		}
		UpdateTypeLists();
		//Destroy node
		Destroy(node.gameObject);
	}
	# endregion
	public Node[] GetAllNodes(){
		return GameObject.FindObjectsOfType<Node>();
	}

	
	#region TypeList
	public void UpdateTypeLists(){
		TryAddToNodesTypeLists(GetAllNodes(), nodeTypeLists.ToArray());
	}
	public List<NodeTypeList> TryAddToNodesTypeLists(Node[] nodes, NodeTypeList[] typeLists = null){
		NodeTypeList[] listsToCheck;
		if (typeLists == null) listsToCheck = nodeTypeLists.ToArray();
		else listsToCheck = typeLists;
		var outTypeLists = new List<NodeTypeList>();

		foreach (var typeList in listsToCheck)
		{
			foreach (var node in nodes)
			{
				if (!AreRequirementsMet(node, typeList.requirements)){
					typeList.nodes.Remove(node);
					continue;
				} 
				if (!typeList.nodes.Contains(node)) typeList.nodes.Add(node);

				bool AreRequirementsMet(Node node, TypeListRequirement[] requirements){
					if (requirements == null) return true;
					foreach (var requirement in requirements)
					{
						if (!PathMatches(node.nodePath, requirement.path)) return false;
						if (node.connectedNodes.Count < requirement.connectedNodeRequirements.Length) return false;
						foreach (var connectedNode in node.connectedNodes)
						{
							if (!AreRequirementsMet(connectedNode, requirement.connectedNodeRequirements)) return false;
						}
					}
					//if requirements are met
					return true;
				}

			}
		}
		return outTypeLists;

		bool PathMatches(string path, string filter){
			var regex = new Regex(filter, RegexOptions.None);
			//if (string.IsNullOrEmpty(filter)) return false;
            return regex.IsMatch(path);
		}
	}
	public void SelectNodes(Node[] nodesToSelect){
		foreach (var node in nodesToSelect){
			if (node == null) continue;
			//find the "selected" type list
			var selectedTypeList = nodeTypeLists.Find(x=>x.listPath == "selected.tlist");
			//Create selected.tlist if it doesn't exist
			if (selectedTypeList == null){
				selectedTypeList = new(
					"selected.tlist", 
					null, 
					Color.clear, 
					new TypeListRequirement[] {
						new TypeListRequirement(
							"",
							new TypeListRequirement[]{
								new TypeListRequirement(
									"identifiers/selected.node",
									null
								)
							}
						)
					});
				nodeTypeLists.Add(selectedTypeList);
			}
			//if node has an identifier called "selected" remove it else add it
			var selectedNode = GetNode("identifiers/selected.node", true, true);
			if (!node.connectedNodes.Contains(selectedNode)){
				ConnectNodes(node, selectedNode);
				TryAddToNodesTypeLists(new[]{node});
			}
			else{
				DisconnectNodes(node, selectedNode);
				TryAddToNodesTypeLists(new[]{node});
			}
		}
	}
	public void ConnectSelectedNodes(Node nodeToConnectTo, bool reverseConnect){
		var selectedTypeList = nodeTypeLists.Find(x=>x.listPath == "selected.tlist");
		if (selectedTypeList == null || selectedTypeList.nodes.Count == 0) return;
		foreach (var selectedNode in selectedTypeList.nodes)
		{
			if (reverseConnect) ConnectNodes(nodeToConnectTo, selectedNode, false);
			else ConnectNodes(selectedNode, nodeToConnectTo, false);
		}
		UpdateTypeLists();
	}
	public void ManageConnectionForcesTList(){
		var connForceTypeList = nodeTypeLists.Find(x=>x.listPath == "connection-force.tlist");
			//Create selected.tlist if it doesn't exist
			if (connForceTypeList == null){
				connForceTypeList = new(
					"connection-force.tlist", 
					null, 
					Color.clear, 
					new TypeListRequirement[] {
						new TypeListRequirement(
							"",
							new TypeListRequirement[]{
								new TypeListRequirement(
									"",
									new TypeListRequirement[]{
										new TypeListRequirement(
											"identifier/connection-force.node",
											null
										)
									}
								)
							}
						)
					});
				nodeTypeLists.Add(connForceTypeList);
			}
	}
	#endregion
}


