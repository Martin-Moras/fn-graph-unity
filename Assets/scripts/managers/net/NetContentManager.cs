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
	public List<DataNode> newDataNodes = new();
	public List<DataNode> deletedDataNodes = new();
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
		newDataNodes.Add(newNode);
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
		HandleNodeConnection(node, newNode);
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
				break;
			case ConnectType.Disconnect:
				fromNode.connectedNodes.Remove(toNode);
				fromNode.connectedNodeIds.Remove(toNode.nodeId);
			break;
			case ConnectType.Toggle:
				if (fromNode.connectedNodes.Exists(x=>x == toNode))
					HandleNodeConnection(fromNode, toNode, ConnectType.Disconnect);
				else
					HandleNodeConnection(fromNode, toNode, ConnectType.Disconnect);
				break;
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
		//Remove all in going connections
		foreach (var refNode in GetAllNodes().ToList().FindAll(x => x.connectedNodeIds.Exists(x => x == node.nodeId))) {
			HandleNodeConnection(refNode, node, ConnectType.Disconnect);
		}
		//Remove all out going connections
		foreach (var connectedNode in node.connectedNodes) {
			HandleNodeConnection(node, connectedNode, ConnectType.Disconnect);
		}
		deletedDataNodes.Add(node);
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

	public override void ManagerUpdate()
	{
		throw new NotImplementedException();
	}
}
public enum ConnectType {
	Connect,
	Disconnect,
	Toggle,
}