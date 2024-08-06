using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualsManager : MonoBehaviour
{
	#region Singleton
	public static VisualsManager Instance { get; private set;}
	void SingletonizeThis()
	{
		if (Instance != null && Instance != this) Destroy(this);
		else Instance = this;
	}
	#endregion
	void Awake()
	{
		SingletonizeThis();
	}
	
	void Update()
	{
		manageSprite();
	}
	public void manageSprite(){
		foreach (var selectedNode in NetManager.Instance.GetAllNodes())
		{
			var renderer = selectedNode.GetComponent<SpriteRenderer>();

			if (NetManager.Instance.nodeTypeLists.Find(x => x.listPath == "selected.tlist").nodes.Contains(selectedNode)) renderer.color = Color.white;
			else renderer.color = Color.red; 
		}
	}
}