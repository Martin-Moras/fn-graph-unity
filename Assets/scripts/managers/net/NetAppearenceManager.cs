using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetAppearenceManager : MonoBehaviour
{
	#region Singleton
	public static NetAppearenceManager Instance { get; private set;}
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
		ManageSprite();
		ManageConnectionLine();
	}
	private void ManageSprite(){
		foreach (var node in NetContentManager.Instance.GetAllNodes()){
			var renderer = node.GetComponent<SpriteRenderer>();
			var selectedTList = NetContentManager.Instance.nodeTypeLists.Find(x => x.listPath == "selected.tlist");
			if (selectedTList != null && selectedTList.nodes.Contains(node)) 
				renderer.color = Color.white;
			else renderer.color = Color.red; 
		}
	}
	private void ManageConnectionLine(){
		foreach (var node in NetContentManager.Instance.GetAllNodes()){
			foreach (var connection in node.connections)
			{
				connection.line.SetPositions(new Vector3[]{node.transform.position, connection.outNode.transform.position});
			}
		}
	}
}