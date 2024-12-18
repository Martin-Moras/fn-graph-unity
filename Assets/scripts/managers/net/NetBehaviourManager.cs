using System;
using MathNet.Numerics.Interpolation;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEditor.Experimental.GraphView;


public class NetBehaviourManager : I_Manager
{
	#region Singleton
	public static NetBehaviourManager inst { get; private set;}
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
		AddNewNodesToAllNodes();
	}
	public DataNode NewSaverNode(uint saverNodeId, List<DataNode> connectedNodes, string saverNodePath)
	{
		//Create Saver-node
		var saverNode = NetContentManager.inst.NewNode(saverNodeId, connectedNodes.Select(x => x.nodeId).ToList(), saverNodePath);
		SpecialNodeManager.inst.saverNode_sp.connectedNodes.Add(saverNode);
		return saverNode;
	}
	public void SelectNodes(List<DataNode> nodes, SelectAction selectAction)
	{
		foreach (var node in nodes)
			SelectNode(node, selectAction);
	}
	public void SelectNode(DataNode node, SelectAction selectAction)
	{
		switch (selectAction){
			case SelectAction.Select:
				NetContentManager.inst.HandleNodeConnection(SpecialNodeManager.inst.selected_sp, node);
				break;
			case SelectAction.Deselect:
				NetContentManager.inst.HandleNodeConnection(SpecialNodeManager.inst.selected_sp, node, ConnectType.Disconnect);
				break;
			case SelectAction.Toggle:
				NetContentManager.inst.HandleNodeConnection(SpecialNodeManager.inst.selected_sp, node, ConnectType.Toggle);
				break;
		}
	}
	private void AddNewNodesToAllNodes()
	{
		foreach (var newNode in NetContentManager.inst.thisFrame_newDataNodes) {
			NetContentManager.inst.HandleNodeConnection(SpecialNodeManager.inst.allNodes_sp, newNode);
		}
	}
}
public enum SelectAction {
	Select,
	Deselect,
	Toggle,
}