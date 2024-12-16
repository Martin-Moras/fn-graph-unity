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


public class NetContentManager : I_Manager
{
	public string id;
	public List<NodeTypeList> nodeTypeLists = new();
	#region Singleton
	
	public static NetContentManager inst { get; private set;}
	void SingletonizeThis()
	{
		if (inst != null && inst != this) Destroy(this);
		else inst = this;
	}
	#endregion
	
	public override void Initiallize()
	{
		SingletonizeThis();
	}
	/// <summary>
	/// creates a new node. 
	/// </summary>
	/// <param name="nodePath"></param>
	/// <returns></returns>
	public DataNode NewNode(uint nodeId, List<uint> connectedNodeIds, string nodePath = "")
	{
		//if path = "" change it to a random number
		if (nodePath == "")
			nodePath = UnityEngine.Random.Range(0, int.MaxValue).ToString(); 
		//create a new node
		var newNode = new DataNode(nodePath, nodeId, connectedNodeIds);
		SetConnectedDataNodesBasedOnConnectedNodeId(newNode);
		return newNode;
	}
	public void SetConnectedDataNodesBasedOnConnectedNodeId(DataNode dataNode)
	{
		foreach (var currDataNode in GetAllNodes()) {
			//find and connect all nodes to dataNode
			foreach (var connectedNodeId in currDataNode.connectedNodeIds) {
				if (dataNode.nodeId == connectedNodeId) {
					if (!currDataNode.connectedNodes.Exists(x => x == dataNode)) {
						currDataNode.connectedNodes.Add(dataNode);
					}
				}
			}
			//connect all nodes to dataNode
			foreach (var connectedNodeId in dataNode.connectedNodeIds) {
				if (currDataNode.nodeId == connectedNodeId) {
					if (!dataNode.connectedNodes.Exists(x => x == currDataNode)) {
						dataNode.connectedNodes.Add(currDataNode);
					}
				}
			}
		}
	}
	public void ConnectNewNode(DataNode node, out DataNode newNode)
	{
		//instantiate a new node
		newNode = new("", VariableManager.inst.GenerateId(), null);
		ConnectNodes(node, newNode);
	}
	public void ConnectNodes(DataNode fromNode, DataNode toNode, bool updateTypeLists = true)
	{
		fromNode.connectedNodes.Add(toNode);
		fromNode.connectedNodeIds.Add(toNode.nodeId);
		if (updateTypeLists) UpdateTypeLists();
	}
	public Connection NewConnection(DataNode outNode)
	{
		var newConnetion = Instantiate(VariableManager.inst.ConnectionPrefab).GetComponent<Connection>();
		newConnetion.outNode = outNode;
		return newConnetion;
	}
	public void DisconnectNodes(DataNode inputNode, DataNode outputNode)
	{
		inputNode.connectedNodes.Remove(outputNode);
		inputNode.connectedNodeIds.Remove(outputNode.nodeId);

		UpdateTypeLists();
	}
	public DataNode GetNode(string nodePath, bool createIfDoesntExist, bool tryAddToNodesTypeLists)
	{
		DataNode newSelectedNode = GetAllNodes().ToList().Find(x=>x.nodePath == nodePath);
			if (newSelectedNode == null && createIfDoesntExist)
				newSelectedNode = NewNode(VariableManager.inst.GenerateId(), null, nodePath);
			if (tryAddToNodesTypeLists) TryAddToNodesTypeLists(new[]{newSelectedNode});
		return newSelectedNode;
	}
	
	public DataNode[] GetAllNodes()
	{
		return GameObject.FindObjectsOfType<DataNode>();
	}
	#region Delete
	public void DeleteNode(DataNode node){
		//Remove all in going connections
		foreach (var refNode in GetAllNodes().ToList().FindAll(x => x.connectedNodeIds.Exists(x => x == node.nodeId))) {
			DisconnectNodes(refNode, node);
		}
		//Remove all out going connections
		foreach (var connectedNode in node.connectedNodes) {
			DisconnectNodes(node, connectedNode);
		}
	}
	#endregion
	#region TypeList
	public void UpdateTypeLists(){
		TryAddToNodesTypeLists(GetAllNodes(), nodeTypeLists.ToArray());
	}
	public List<NodeTypeList> TryAddToNodesTypeLists(DataNode[] nodes, NodeTypeList[] typeLists = null){
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

				bool AreRequirementsMet(DataNode node, TypeListRequirement[] requirements){
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
	public void SelectNodes(DataNode[] nodesToSelect){
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
	public void ConnectSelectedNodes(DataNode nodeToConnectTo, bool reverseConnect){
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