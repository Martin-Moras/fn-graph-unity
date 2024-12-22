using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetInteractionManager : I_Manager
{
	public MainInput mainInput;
	public Vector2 mousePosWorld;
	public Vector2 mousePosScreen;
	public Vector2 moveCamera;
	public float changeCameraSize;
	public override void Initialize()
	{
		if (mainInput == null)
			mainInput = new();
		mainInput.mainScene.Enable();
	}

	public override void ManagerUpdate()
	{
		ManageInputs();
	}
	public void ManageInputs(){
		if (mainInput.mainScene.NewNode.triggered) 
			GameManager.inst.netContentManager.NewNode("unnamed");
		//connect new node to all selected nodes
		if (mainInput.mainScene.NewChildNode.triggered) {
			foreach (var selectedNode in GameManager.inst.specialNodeManager.selected_sp.connectedNodes) {
				GameManager.inst.netContentManager.ConnectNewNode(selectedNode);
			}
		}
		//Load all
		if (mainInput.mainScene.Load.triggered) 
			GameManager.inst.backupManager.LoadDirectory(GameManager.inst.variableManager.netSavePath);
		//Save all
		if (mainInput.mainScene.Save.triggered)
			GameManager.inst.backupManager.SaveNodes(GameManager.inst.specialNodeManager.saverNode_sp.connectedNodes);
		//Connect to selected nodes
		if (mainInput.mainScene.ConnectToSelectedNodes.triggered) 
			GameManager.inst.netContentManager.ConnectSelectedNodes(GetNodeUnderCursor(), ConnectType.Connect, true);
		//Connect selected nodes
		if (mainInput.mainScene.ConnectSelectedNodes.triggered) 
			GameManager.inst.netContentManager.ConnectSelectedNodes(GetNodeUnderCursor(), ConnectType.Connect, false);
		//Select Node under Cursor
		if (mainInput.mainScene.Select.triggered) 
			GameManager.inst.netBehaviourManager.SelectNode(GetNodeUnderCursor(), SelectAction.Toggle);
		//Camera
		moveCamera = mainInput.mainScene.MoveCamera.ReadValue<Vector2>();
		changeCameraSize = MathF.Sign(mainInput.mainScene.ChangeCameraSize.ReadValue<float>());
		//Manage cursor position
		mousePosScreen = Mouse.current.position.ReadValue();
		mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
	}
	public DataNode GetNodeUnderCursor(){
		var objectsUnderCursor = Physics2D.OverlapCircleAll(mousePosWorld, GameManager.inst.variableManager.nodeSelectionRadius);

		Collider2D closestCollider = null;
		foreach (var objectUnderCursor in objectsUnderCursor)
		{
			if (objectUnderCursor.GetComponent<VisualNode>() == null) 
				continue;
			if (objectUnderCursor.isTrigger) 
				continue;
			if (closestCollider == null) 
				closestCollider = objectUnderCursor;
			var currObjectDist = ((Vector2)objectUnderCursor.transform.position - mousePosWorld).magnitude;
			var closestColliderDist = ((Vector2)closestCollider.transform.position - mousePosWorld).magnitude;
			//if the current objectUnderCursor is closer than closestCollider set closestCollider to it
			if (currObjectDist < closestColliderDist) closestCollider = objectUnderCursor;
		}
		return closestCollider?.GetComponent<VisualNode>().dataNode;
	}
	
	private void DragNode(){

	}
	private void DropNode(){
	
	}
	private void OnEnable(){
		if (mainInput != null)
			mainInput.mainScene.Enable();
	}
	private void OnDisable(){
		if (mainInput != null)
			mainInput.mainScene.Disable();
	}
}
