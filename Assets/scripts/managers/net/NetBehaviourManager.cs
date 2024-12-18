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
		
	}
	public DataNode NewSaverNode(uint saverNodeId, List<DataNode> connectedNodes, string saverNodePath)
	{
		//Create Saver-node
		var saverNode = NetContentManager.inst.NewNode(saverNodeId, connectedNodes.Select(x => x.nodeId).ToList(), saverNodePath);
		SpecialNodeManager.inst.saverNode_sp.connectedNodes.Add(saverNode);
		return saverNode;
	}
	public void SelectNodes(List<DataNode> nodes)
	{
		foreach (var node in nodes)
			SelectNode(node);
	}
	public void SelectNode(DataNode node)
	{
		NetContentManager.inst.ConnectNodes(selected_sp, node);
	}
}