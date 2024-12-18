using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SpecialNodeManager : I_Manager
{
	public static SpecialNodeManager inst;
	public Dictionary<uint, string> specialNodes;
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
        specialNodes = new() {
			{1 , "AllNodes"},
			{2 , "SaverNode"},
			{3 , "Visable"},
			{4 , "Selected"},
			{5 , "Summerize"},
			{6 , "Category"},
		};
		
		var loadedSpecialNodes = BackupManager.inst.LoadFile(VariableManager.inst.specialNodesSavePath).connectedNodes;
		
		if (loadedSpecialNodes == null) {
			Debug.LogError($"special node save file is invalide");
			return;
		}

		allNodes_sp = loadedSpecialNodes.Find(x=>x.nodePath == specialNodes[1]);
		saverNode_sp = loadedSpecialNodes.Find(x=>x.nodePath == specialNodes[2]);
		visible_sp = loadedSpecialNodes.Find(x=>x.nodePath == specialNodes[3]);
		selected_sp = loadedSpecialNodes.Find(x=>x.nodePath == specialNodes[4]);
		summerize_sp = loadedSpecialNodes.Find(x=>x.nodePath == specialNodes[5]);
		category_sp = loadedSpecialNodes.Find(x=>x.nodePath == specialNodes[6]);

    }

	public override void ManagerUpdate()
	{
		throw new NotImplementedException();
	}
	public void AddSaverNode(DataNode saverNode)
	{
		if (saverNode == null || saverNode == null) 
			return;
		saverNode_sp.connectedNodes.Add(saverNode);
	}
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