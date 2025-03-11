using System;
using MathNet.Numerics.Interpolation;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.AI;


public class NetContentManager : I_Manager
{
	public string id;
	public List<DataNode> thisFrame_newDataNodes = new();
	public List<DataNode> thisFrame_deletedDataNodes = new();
	public List<DataNode> thisFrame_changedNodes = new();
	
	public override void Initialize()
	{
	}

	public override void ManagerUpdate()
	{
		FilterChangedNodes();
	}
	/// <summary>
	/// creates a new node. 
	/// </summary>
	/// <param name="nodePath"></param>
	/// <returns></returns>
	public DataNode NewNode(string nodePath = "", uint? nodeId = null, List<uint> connectedNodeIds = null)
	{
		//if path = "" change it to a "unnamed"
		if (nodePath == "")
			nodePath = "unnamed"; 
		//if nodeId = null generate one
		if (nodeId == null)
			nodeId = GameManager.inst.variableManager.GenerateId();
		//if nodeId allready exists return null
		else if (GetNodeById((uint)nodeId) != null) {
			
			return null;
		}
		//create a new node
		var newNode = new DataNode(nodePath, (uint)nodeId, connectedNodeIds);
		thisFrame_newDataNodes.Add(newNode);
		HandleNodeConnection(GameManager.inst.specialNodeManager.allNodes_sp, newNode);
		if (connectedNodeIds != null && connectedNodeIds.Count > 0)
			SetConnectedDataNodesBasedOnConnectedNodeId(newNode);
		return newNode;
	}
	public void SetConnectedDataNodesBasedOnConnectedNodeId(DataNode dataNode)
	{
		//loop through all nodes that connect to "dataNode"
		var allNodes = GetAllNodes();
		if (allNodes == null) {
			Debug.LogWarning("SetConnectedDataNodesBasedOnConnectedNodeId: allNodes are null");
			return;
		}
		var inGoingConnectedNodes = new List<DataNode>();
		foreach (var connectedId in dataNode.connectedNodeIds) {
			var connectedNode = allNodes.Find(x=>x.nodeId == connectedId);
			if (connectedNode == null)
				continue;
			inGoingConnectedNodes.Add(connectedNode);
		}
		foreach (var inGoingConnectedNode in inGoingConnectedNodes) {
			//connect node which has the id of "dataNode" in its "connectedNodeIds"
			HandleNodeConnection(inGoingConnectedNode, dataNode);
		}
		//connect dataNode to all nodes
		foreach (var connectedNodeId in dataNode.connectedNodeIds) {
			var outGoingConnectedNode = allNodes.Find(x=>x.nodeId == connectedNodeId);
			if (outGoingConnectedNode == null)
				continue;
			HandleNodeConnection(dataNode, outGoingConnectedNode);
		}
	}
	public DataNode ConnectNewNode(DataNode node)
	{
		//instantiate a new node
		var newNode = NewNode("Unnamed");
		HandleNodeConnection(node, newNode);
		return newNode;
	}
	/// <summary>
	/// By default connects "fromNode" to "toNode".
	/// If there is allready a connection between the two nodes it doesn't add another one
	/// </summary>
	/// <param name="fromNode"></param>
	/// <param name="toNode"></param>
	/// <param name="connectType"></param>
	public void HandleNodeConnection(DataNode fromNode, DataNode toNode, ConnectType connectType = ConnectType.Connect)
	{
		if (fromNode == null || toNode == null)
			return;
		switch (connectType) {
			case ConnectType.Connect:
				if (!fromNode.connectedNodes.Exists(x=>x == toNode))
					fromNode.connectedNodes.Add(toNode);
				if (!fromNode.connectedNodeIds.Exists(x=>x == toNode.nodeId))
					fromNode.connectedNodeIds.Add(toNode.nodeId);
				thisFrame_changedNodes.Add(fromNode);
				thisFrame_changedNodes.Add(toNode);
				break;
			case ConnectType.Disconnect:
				fromNode.connectedNodes.Remove(toNode);
				fromNode.connectedNodeIds.Remove(toNode.nodeId);
				thisFrame_changedNodes.Add(fromNode);
				thisFrame_changedNodes.Add(toNode);
			break;
			case ConnectType.Toggle:
				if (fromNode.connectedNodes.Exists(x=>x == toNode))
					HandleNodeConnection(fromNode, toNode, ConnectType.Disconnect);
				else
					HandleNodeConnection(fromNode, toNode, ConnectType.Connect);
				break;
		}
	}
	private void FilterChangedNodes()
	{
		//find all nodes that are in "changedNodes" and in ("newDataNodes" or "deletedDataNodes")
		var nodesToRemove = thisFrame_changedNodes
			.FindAll(changedNode => 
				thisFrame_newDataNodes.Contains(changedNode) ||
				thisFrame_deletedDataNodes.Contains(changedNode));
		//remove all nodes from "thisFrame_changedNodes" which also ocurred in "newDataNodes" or "deletedDataNodes"
		foreach (var node in nodesToRemove)	{
			thisFrame_changedNodes.Remove(node);
		}
	}
	public DataNode GetNodeById(uint nodeId)
	{
		var allNodes = GetAllNodes();
		if (allNodes == null)
			return null;
		var newSelectedNode = allNodes.Find(x=>x.nodeId == nodeId);
		return newSelectedNode;
	}
	public List<DataNode> GetNodeByName(string nodePath)
	{
		var allNodes = GetAllNodes();
		if (allNodes == null)
			return null;
		var newSelectedNode = allNodes.FindAll(x=>x.nodePath == nodePath);
		return newSelectedNode;
	}
	public List<DataNode> GetAllNodes()
	{
		if (GameManager.inst.specialNodeManager.allNodes_sp == null)
			return null;
		return GameManager.inst.specialNodeManager.allNodes_sp.connectedNodes;
	}
	public void DeleteNode(DataNode node)
	{
		thisFrame_deletedDataNodes.Add(node);
		//Remove all in going connections
		var allNodes = GetAllNodes();
		if (allNodes == null) {
			Debug.LogWarning("Delete Node: allNodes are null");
			return;
		}
		foreach (var refNode in allNodes.ToList().FindAll(x => x.connectedNodeIds.Exists(x => x == node.nodeId))) {
			HandleNodeConnection(refNode, node, ConnectType.Disconnect);
		}
		//Remove all out going connections
		foreach (var connectedNode in node.connectedNodes) {
			HandleNodeConnection(node, connectedNode, ConnectType.Disconnect);
		}
	}
	public void ConnectSelectedNodes(DataNode nodeToConnectTo, ConnectType connectType, bool reverseConnect){
		var selectedTypeList = GameManager.inst.specialNodeManager.selected_sp;
		if (selectedTypeList == null || 
			selectedTypeList.connectedNodes == null) 
			return;
		foreach (var selectedNode in selectedTypeList.connectedNodes)
		{
			if (reverseConnect) 
				HandleNodeConnection(nodeToConnectTo, selectedNode, connectType);
			else 
				HandleNodeConnection(selectedNode, nodeToConnectTo, connectType);
		}
	}
	public void RenameNode(DataNode node, string path)
	{
		node.SetPath(path);
		thisFrame_changedNodes.Add(node);
	}
}
public enum ConnectType {
	Connect,
	Disconnect,
	Toggle,
}