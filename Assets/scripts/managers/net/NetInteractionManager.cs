using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetInteractionManager : I_Manager
{
	private MainInput mainInput;
	public Vector2 mousePosWorld;
	public Vector2 mousePosScreen;
	public Vector2 moveCamera;
	public float changeCameraSize;
	#region events
	public delegate void EventHandler();
	#endregion
	#region Singleton
	public static NetInteractionManager inst { get; private set;}
	public override void SingletonizeThis()
	{
		if (inst != null && inst != this) Destroy(this);
		else inst = this;
	}
	#endregion
	public override void Initialize()
	{
	}
	private void Awake() {
		mainInput = new();
		mainInput.Enable();
	}

	public void ManageInputs(){
		if (mainInput.mainScene.NewNode.triggered) NetContentManager.inst.NewNode(VariableManager.inst.GenerateId(), null, "unnamed");
		//connect new node to all selected nodes
		if (mainInput.mainScene.NewChildNode.triggered) {
			foreach (var selectedNode in SpecialNodeManager.inst.selected_sp.connectedNodes) {
				NetContentManager.inst.ConnectNewNode(selectedNode);
			}
		}
		//Load all
		if (mainInput.mainScene.Load.triggered) BackupManager.inst.LoadDirectory(VariableManager.inst.netSavePath);
		//Save all
		if (mainInput.mainScene.Save.triggered)
			BackupManager.inst.SaveNodes(NetContentManager.inst.GetAllNodes().ToList());
		//Connect to selected nodes
		if (mainInput.mainScene.ConnectToSelectedNodes.triggered) 
			NetContentManager.inst.ConnectSelectedNodes(GetNodeUnderCursor(), ConnectType.Connect, true);
		//Connect selected nodes
		if (mainInput.mainScene.ConnectSelectedNodes.triggered) 
			NetContentManager.inst.ConnectSelectedNodes(GetNodeUnderCursor(), ConnectType.Connect, false);
		//Select Node under Cursor
		if (mainInput.mainScene.Select.triggered) 
			NetBehaviourManager.inst.SelectNode(GetNodeUnderCursor(), SelectAction.Toggle);
		//Camera
		moveCamera = mainInput.mainScene.MoveCamera.ReadValue<Vector2>();
		changeCameraSize = MathF.Sign(mainInput.mainScene.ChangeCameraSize.ReadValue<float>());
		//Manage cursor position
		mousePosScreen = Input.mousePosition;
		mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
	}
	public DataNode GetNodeUnderCursor(){
		var objectsUnderCursor = Physics2D.OverlapCircleAll(mousePosWorld, VariableManager.inst.nodeSelectionRadius);

		Collider2D closestCollider = objectsUnderCursor.FirstOrDefault();
		foreach (var objectUnderCursor in objectsUnderCursor)
		{
			if (objectUnderCursor.GetComponent<DataNode>() == null) continue;
			var currObjectDist = ((Vector2)objectUnderCursor.transform.position - mousePosWorld).magnitude;
			var closestColliderDist = ((Vector2)closestCollider.transform.position - mousePosWorld).magnitude;
			//if the current objectUnderCursor is closer than closestCollider set closestCollider to it
			if (currObjectDist < closestColliderDist) closestCollider = objectUnderCursor;
		}
		if (closestCollider == null) return null;
		return closestCollider.GetComponent<DataNode>();
	}
	
	private void DragNode(){

	}
	private void DropNode(){
	
	}
	public void SelectNodes(DataNode[] nodesToSelect){
		
	}
	private void OnEnable(){
		mainInput.Enable();
	}
	private void OnDisable(){
		mainInput.Disable();
	}

	public override void ManagerUpdate()
	{
		throw new NotImplementedException();
	}
}
