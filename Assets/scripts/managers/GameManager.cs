using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private DataNode temp_allsaver;

	public NetBehaviourManager netBehaviourManager { get; private set; }
	public NetContentManager netContentManager { get; private set; }
	public NetInteractionManager netInteractionManager { get; private set; }
	public NetVisualManager netVisualManager { get; private set; }
	public SpecialNodeManager specialNodeManager { get; private set; }
	public VariableManager variableManager { get; private set; }
	public BackupManager backupManager { get; private set; }
	public CameraManager cameraManager { get; private set; }
	#region Singleton
	public static GameManager inst { get; private set;}
	private void SingletonizeThis()
	{
		var thisClass = this;
		if (inst != null && inst != thisClass) Destroy(thisClass);
		else inst = thisClass;
	}
	#endregion
	private void Awake() {
		SingletonizeThis();

		variableManager = GetComponent<VariableManager>();
		netBehaviourManager = GetComponent<NetBehaviourManager>();
		netContentManager = GetComponent<NetContentManager>();
		netInteractionManager = GetComponent<NetInteractionManager>();
		netVisualManager = GetComponent<NetVisualManager>();
		specialNodeManager = GetComponent<SpecialNodeManager>();
		backupManager = GetComponent<BackupManager>();
		cameraManager = GetComponent<CameraManager>();

		variableManager.Initialize();
		backupManager.Initialize();
		specialNodeManager.Initialize();
		netContentManager.Initialize();
		netBehaviourManager.Initialize();
		netInteractionManager.Initialize();
		netVisualManager.Initialize();
		cameraManager.Initialize();

		temp_allsaver = specialNodeManager.NewSaverNode("All saver");
	}
	void Update()
	{
		if (inst == null)
			SingletonizeThis();
		// netInteractionManager.ManageInputs();j
		variableManager.ManagerUpdate();
		netInteractionManager.ManagerUpdate();
		netContentManager.ManagerUpdate();
		netBehaviourManager.ManagerUpdate();
		foreach (var newNode in netContentManager.thisFrame_newDataNodes) {
			if (newNode != specialNodeManager.allNodes_sp && 
				newNode != specialNodeManager.saverNode_sp && 
				newNode != temp_allsaver)
				netContentManager.HandleNodeConnection(temp_allsaver, newNode);
		}
		foreach (var deletedNode in netContentManager.thisFrame_deletedDataNodes)
			netContentManager.HandleNodeConnection(temp_allsaver, deletedNode, ConnectType.Disconnect);
		netVisualManager.ManagerUpdate();
		specialNodeManager.ManagerUpdate();
		backupManager.ManagerUpdate();
		cameraManager.ManagerUpdate();


		ClearPerFrameLists();
		/*
		!variables
			x-nodes created this frame
			x-nodes deleted this frame
			-nodes shown this frame
			-nodes hidden this frame
			x - GameManager should clear all those at the begining of the frame
		Visuals:
			x get all data nodes
			x turn them into Visual nodes
			x add them to the allVisualNodes list
			x connect visual nodes
			-handle life spriteRenderer change based on connected nodes
			-update positions of lineRenderers of connections
			-show only nodes which are connected to the special node "visable"
			-delete visual nodes once they aren't connected to the "visable" node
			-add functionality to the summerize node
			-add functionality to the category node
				-visualManager part
				-contentManager part

		load
			x create new data nodes
			x add them to a list called newDataNodes
			x connect them to existing nodes
			x add them to all nodes -----------------------¬
			x turn them into visual nodes					 |
														 |
		special nodes <-----------------------------------
			x Make shure all SpecialNodes are existent
			Code behaviour for special nodes
				x All
				x Saver
				x Selected
				Visable
				Summerize
				Category
		*/
	}
	private void ClearPerFrameLists()
	{
		netContentManager.thisFrame_newDataNodes.Clear();
		netContentManager.thisFrame_deletedDataNodes.Clear();
		netContentManager.thisFrame_changedNodes.Clear();
		netVisualManager.thisFrame_DeletedConnections.Clear();
		netVisualManager.thisFrame_DeletedVisualNodes.Clear();
		netVisualManager.thisFrame_newConnections.Clear();
		netVisualManager.thisFrame_newVisualNodes.Clear();
	}
}
