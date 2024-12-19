using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class SpecialNodeManager : I_Manager
{
	public static SpecialNodeManager inst;
	public Dictionary<string, DataNode> specialNodes_dict;
	public DataNode specialNode_sp;
	public DataNode allNodes_sp;
	public DataNode visible_sp;
	public DataNode selected_sp;
	public DataNode summerize_sp;
	public DataNode category_sp;
	public DataNode saverNode_sp;
	public override void SingletonizeThis()
	{
		if (inst != null && inst != this) Destroy(this);
		else inst = this;
	}
    public override void Initialize()
    {
        specialNodes_dict = new() {
			{"All", allNodes_sp},
			{"Saver", saverNode_sp},
			{"Visable", visible_sp},
			{"Selected", selected_sp},
			{"Summerize", summerize_sp},
			{"Category", category_sp},
		};
		//load special Nodes from a file
		var loadedSaverNode = BackupManager.inst.LoadFile(VariableManager.inst.specialNodesSavePath);
		
		SetSpecialNodeVariables(loadedSaverNode, specialNodes_dict);
		//Create special Node File if it doesn't exist 
		if (loadedSaverNode == null) {
			BackupManager.inst.SaveNode(specialNode_sp);
		}
		//add 
		AddSaverNode(loadedSaverNode);
    }

	public override void ManagerUpdate()
	{
		throw new NotImplementedException();
	}
	public void AddSaverNode(DataNode saverNode)
	{
		if (saverNode == null || saverNode_sp == null) 
			return;
		NetContentManager.inst.HandleNodeConnection(saverNode_sp, saverNode);
	}
	private void SetSpecialNodeVariables(DataNode specialSaverNode, Dictionary<string, DataNode> specialNodes_dict) 
	{
		if (specialSaverNode == null)
			specialNode_sp = NetContentManager.inst.NewNode(VariableManager.inst.GenerateId(), null, VariableManager.inst.specialNodeName);
		else
			specialNode_sp = specialSaverNode;
		foreach (var specialNodeKey in specialNodes_dict.Keys) {
			specialNodes_dict[specialNodeKey] = specialNode_sp.connectedNodes.Find(x=>x.nodePath == specialNodeKey);
			if (specialNodes_dict[specialNodeKey] == null)
				specialNodes_dict[specialNodeKey] = NetContentManager.inst.NewNode(VariableManager.inst.GenerateId(), null, specialNodeKey);
			NetContentManager.inst.HandleNodeConnection(specialNode_sp, specialNodes_dict[specialNodeKey]);
		}
	}
	private void SetupSpe
}
public enum PatternIdentifierNodes
{
	Text,
	Size,
	Color,
	TextColor,
	Shape,
	RepellForce,
	ConnectionStrength,
	ConnectionLength,

}