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
		netInteractionManager = GetComponent<NetInteractionManager>();
		netVisualManager = GetComponent<NetVisualManager>();
		specialNodeManager = GetComponent<SpecialNodeManager>();

		variableManager.SingletonizeThis();
		netBehaviourManager.SingletonizeThis();
		netContentManager.SingletonizeThis();
		netInteractionManager.SingletonizeThis();
		netVisualManager.SingletonizeThis();
		specialNodeManager.SingletonizeThis();
		
		variableManager.Initialize();
		netBehaviourManager.Initialize();
		netContentManager.Initialize();
		netInteractionManager.Initialize();
		netVisualManager.Initialize();
		specialNodeManager.Initialize();

	}
	void Update()
	{
		netInteractionManager.ManageInputs();
		netContentManager.ManagerUpdate();
		netBehaviourManager.ManagerUpdate();
		netVisualManager.ManagerUpdate();
		specialNodeManager.ManagerUpdate();
		variableManager.ManagerUpdate();
		/*
		!variables
			x-nodes created this frame
			x-nodes deleted this frame
			-nodes shown this frame
			-nodes hidden this frame
			x - GameManager should clear all those 
				at the begining of the frame
		Visuals:
			x get all data nodes
			x turn them into Visual nodes
			x add them to the allVisualNodes list
			connect visual nodes

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
		ClearPerFrameLists();
	}
	private void ClearPerFrameLists()
	{
		netContentManager.thisFrame_newDataNodes.Clear();
		netContentManager.thisFrame_deletedDataNodes.Clear();
		netVisualManager.thisFrame_DeletedConnections.Clear();
		netVisualManager.thisFrame_DeletedVisualNodes.Clear();
		netVisualManager.thisFrame_newConnections.Clear();
		netVisualManager.thisFrame_newVisualNodes.Clear();

	}
}
