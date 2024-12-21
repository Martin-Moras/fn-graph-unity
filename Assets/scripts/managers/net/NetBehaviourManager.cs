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
	public override void Initialize()
	{
	}
	public override void ManagerUpdate()
	{
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
				GameManager.inst.netContentManager.HandleNodeConnection(GameManager.inst.specialNodeManager.selected_sp, node);
				break;
			case SelectAction.Deselect:
				GameManager.inst.netContentManager.HandleNodeConnection(GameManager.inst.specialNodeManager.selected_sp, node, ConnectType.Disconnect);
				break;
			case SelectAction.Toggle:
				GameManager.inst.netContentManager.HandleNodeConnection(GameManager.inst.specialNodeManager.selected_sp, node, ConnectType.Toggle);
				break;
		}
	}
}
public enum SelectAction {
	Select,
	Deselect,
	Toggle,
}