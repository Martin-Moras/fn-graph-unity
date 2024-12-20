using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public NetBehaviourManager netBehaviourManager;
	public NetContentManager netContentManager;
	public NetInteractionManager netInteractionManager;
	public NetVisualManager netVisualManager;
	public SpecialNodeManager specialNodeManager;
	public VariableManager variableManager;
	public BackupManager backupManager;
	public CameraManager cameraManager;
	#region Singleton
	public static GameManager inst { get; private set;}
	void SingletonizeThis()
	{
		if (inst != null && inst != this) Destroy(this);
		else inst = this;
	}
	#endregion
	private void Awake() {
		SingletonizeThis();

		variableManager = GetComponent<VariableManager>();
		netBehaviourManager = GetComponent<NetBehaviourManager>();
		netContentManager = GetComponent<NetContentManager>();
		netInteractionManager = FindObjectOfType<NetInteractionManager>();
		netVisualManager = GetComponent<NetVisualManager>();
		specialNodeManager = GetComponent<SpecialNodeManager>();
		backupManager = GetComponent<BackupManager>();
		cameraManager = GetComponent<CameraManager>();

		variableManager.SingletonizeThis();
		backupManager.SingletonizeThis();
		netBehaviourManager.SingletonizeThis();
		netContentManager.SingletonizeThis();
		netInteractionManager.SingletonizeThis();
		netVisualManager.SingletonizeThis();
		specialNodeManager.SingletonizeThis();
		cameraManager.SingletonizeThis();
		
		variableManager.Initialize();
		backupManager.Initialize();
		specialNodeManager.Initialize();
		netContentManager.Initialize();
		netBehaviourManager.Initialize();
		netInteractionManager.Initialize();
		netVisualManager.Initialize();
		cameraManager.Initialize();

	}
	void Update()
	{
		// netInteractionManager.ManageInputs();j
		variableManager.ManagerUpdate();
		netInteractionManager.ManagerUpdate();
		netContentManager.ManagerUpdate();
		netBehaviourManager.ManagerUpdate();
		netVisualManager.ManagerUpdate();
		specialNodeManager.ManagerUpdate();
		cameraManager.ManagerUpdate();
		backupManager.ManagerUpdate();

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
