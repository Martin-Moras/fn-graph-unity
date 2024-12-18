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
	public List<DataNode> thisFrame_newDataNodes = new();
	public List<DataNode> thisFrame_deletedDataNodes = new();
	public List<DataNode> thisFrame_changedNodes = new();
	#region Singleton
	
	public static NetContentManager inst { get; private set;}
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
		FilterChangedNodes();
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
		thisFrame_newDataNodes.Add(newNode);
		SetConnectedDataNodesBasedOnConnectedNodeId(newNode);
		return newNode;
	}
	public void SetConnectedDataNodesBasedOnConnectedNodeId(DataNode dataNode)
	{
		//loop through all nodes that connect to "dataNode"
		foreach (var ingoingConnectedNode in GetAllNodes().FindAll(x=>x.connectedNodeIds.Contains(dataNode.nodeId))) {
			//connect node which has the id of "dataNode" in its "connectedNodeIds"
			HandleNodeConnection(ingoingConnectedNode, dataNode);
		}
		//connect dataNode to all nodes
		foreach (var connectedNodeId in dataNode.connectedNodeIds) {
			var outGoingConnectedNode = GetAllNodes().Find(x=>x.nodeId == connectedNodeId);
			if (outGoingConnectedNode == null)
				continue;
			HandleNodeConnection(dataNode, outGoingConnectedNode);
		}
	}
	public DataNode ConnectNewNode(DataNode node)
	{
		//instantiate a new node
		var newNode = new DataNode("", VariableManager.inst.GenerateId(), null);
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
		switch (connectType) {
			case ConnectType.Connect:
				if (!fromNode.connectedNodes.Exists(x=>x == toNode))
					fromNode.connectedNodes.Add(toNode);
				if (!fromNode.connectedNodes.Exists(x=>x.nodeId == toNode.nodeId))
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
		//delete all nodes from "changedNodes" which also ocurred in "newDataNodes" or "deletedDataNodes"
		foreach (var node in nodesToRemove)	{
			thisFrame_changedNodes.Remove(node);
		}
	}
	public DataNode GetNode(string nodePath, bool createIfDoesntExist)
	{
		DataNode newSelectedNode = GetAllNodes().ToList().Find(x=>x.nodePath == nodePath);
			if (newSelectedNode == null && createIfDoesntExist)
				newSelectedNode = NewNode(VariableManager.inst.GenerateId(), null, nodePath);
		return newSelectedNode;
	}
	
	public List<DataNode> GetAllNodes()
	{
		return SpecialNodeManager.inst.allNodes_sp.connectedNodes;
	}
	public void DeleteNode(DataNode node)
	{
		thisFrame_deletedDataNodes.Add(node);
		//Remove all in going connections
		foreach (var refNode in GetAllNodes().ToList().FindAll(x => x.connectedNodeIds.Exists(x => x == node.nodeId))) {
			HandleNodeConnection(refNode, node, ConnectType.Disconnect);
		}
		//Remove all out going connections
		foreach (var connectedNode in node.connectedNodes) {
			HandleNodeConnection(node, connectedNode, ConnectType.Disconnect);
		}
	}
	public void ConnectSelectedNodes(DataNode nodeToConnectTo, ConnectType connectType, bool reverseConnect){
		var selectedTypeList = SpecialNodeManager.inst.selected_sp;
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