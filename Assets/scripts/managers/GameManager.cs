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
		netBehaviourManager.ManagerUpdate();
		netInteractionManager.ManageInputs();
		variableManager.ManagerUpdate();
		netContentManager.ManagerUpdate();
		netVisualManager.ManagerUpdate();
		specialNodeManager.ManagerUpdate();
		/*
		!variables
			x-nodes created this frame
			x-nodes deleted this frame
			-nodes shown this frame
			-nodes hidden this framöe
			- GameManager should clear all those 
				at the begining of the frame
		Visuals:
			get all data nodes
			turn them into Visual nodes
			add them to the allVisualNodes list

		load
			x create new data nodes
			x add them to a list called newDataNodes
			x connect them to existing nodes
			x add them to all nodes -----------------------¬
			turn them into visual nodes					 |
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
		netContentManager.newDataNodes.Clear();
		netContentManager.deletedDataNodes.Clear();

	}
}
