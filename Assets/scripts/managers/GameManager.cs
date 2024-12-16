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

		netBehaviourManager = GetComponent<NetBehaviourManager>();
		netContentManager = GetComponent<NetContentManager>();
		netInteractionManager = GetComponent<NetInteractionManager>();
		netVisualManager = GetComponent<NetVisualManager>();
		specialNodeManager = GetComponent<SpecialNodeManager>();

		netBehaviourManager.Initiallize();
		netContentManager.Initiallize();
		netInteractionManager.Initiallize();
		netVisualManager.Initiallize();
		specialNodeManager.Initiallize();

	}
	void Update()
	{
		netInteractionManager.ManageInputs();
		/*
		!variables
			-nodes created this frame
			-nodes deleted this frame
			-nodes shown this frame
			-nodes hidden this frame
			- GameManager should clear all those 
				at the begining of the frame
		Visuals:
			get all data nodes
			turn them into Visual nodes
			add them to the allVisualNodes list

		load
			x create new data nodes
			add them to a list called newDataNodes
			connect them to existing nodes
			add them to all nodes -----------------------¬
			turn them into visual nodes					 |
														 |
		special nodes <-----------------------------------
			Make shure all SpecialNodes are existent
			Code behaviour for special nodes
		*/
	}
}
