using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class SpecialNodeManager : I_Manager
{
	private string[] specialNodePahts;
	public DataNode specialNode_sp { get; private set; }
	public DataNode allNodes_sp { get; private set; }
	public DataNode visible_sp { get; private set; }
	public DataNode selected_sp { get; private set; }
	public DataNode summerize_sp { get; private set; }
	public DataNode category_sp { get; private set; }
	public DataNode saverNode_sp { get; private set; }
    public override void Initialize()
    {
        specialNodePahts = new[] {
			// {"All", allNodes_sp},
			// {"Saver", saverNode_sp},
			"Visable",
			"Selected",
			"Summerize",
			"Category"
		};
		//load special Nodes from a file
		allNodes_sp = GameManager.inst.netContentManager.NewNode("All", 0);
		saverNode_sp = GameManager.inst.netContentManager.NewNode("Saver", 1);

		var loadedSaverNode = LoadSaverNode();
		specialNode_sp = loadedSaverNode;
		//Set special node if no spaecial saver file was found
		if (specialNode_sp == null)
			specialNode_sp = GameManager.inst.netContentManager.NewNode(GameManager.inst.variableManager.specialNodeName);
		GameManager.inst.netContentManager.HandleNodeConnection(specialNode_sp, allNodes_sp);
		GameManager.inst.netContentManager.HandleNodeConnection(specialNode_sp, saverNode_sp);
		//Setup specialNodes_dict
		foreach (var specialNodePath in specialNodePahts)
			SetSpecialNodeVariables(specialNode_sp, specialNodePath);
		//Create special Node File if it doesn't exist 
		if (loadedSaverNode == null) {
			GameManager.inst.backupManager.SaveNode(specialNode_sp);
		}


		DataNode LoadSaverNode()
		{
			var specialPath = Path.Combine(GameManager.inst.variableManager.netSavePath, GameManager.inst.variableManager.relativeSpecialNodeSavePath);
			return GameManager.inst.backupManager.LoadFile(specialPath);
		}
    }

	public override void ManagerUpdate()
	{
	}
	public void AddSaverNode(DataNode saverNode)
	{
		GameManager.inst.netContentManager.HandleNodeConnection(saverNode_sp, saverNode);
	}
	public DataNode NewSaverNode(string saverNodePath, uint? saverNodeId = null, List<DataNode> connectedNodes = null)
	{
		//Create Saver-node
		DataNode saverNode = GameManager.inst.netContentManager.NewNode(saverNodePath, saverNodeId, connectedNodes?.Select(x => x.nodeId).ToList());
		if (saverNode == null)
			return null;
		GameManager.inst.netContentManager.HandleNodeConnection(saverNode_sp, saverNode);
		return saverNode;
	}
	private void SetSpecialNodeVariables(DataNode specialSaverNode, string specialNodePath) 
	{
		DataNode specialNode = specialSaverNode.connectedNodes.Find(x=>x.nodePath == specialNodePath);
		if (specialNode == null)
			specialNode = GameManager.inst.netContentManager.NewNode(specialNodePath);
		switch (specialNodePath)
		{
			case "Visable":
				visible_sp = specialNode;
				break;
			case "Selected":
				selected_sp = specialNode;
				break;
			case "Summerize":
				summerize_sp = specialNode;
				break;
			case "Category":
				category_sp = specialNode;
				break;
			default:
				Debug.LogWarning($"Unrecognized special node path: {specialNodePath}");
				break;
		}
		GameManager.inst.netContentManager.HandleNodeConnection(specialNode_sp, specialNode);
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