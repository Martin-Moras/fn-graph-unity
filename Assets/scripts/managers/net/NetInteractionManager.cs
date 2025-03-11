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
	public DataNode nodeUnderCursor;
	private DataNode lastSelectedNode;
	private bool selectPressed_lastFrame;
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
		nodeUnderCursor = GetNodeUnderCursor();

		//connect all selected nodes to a new node
		if (mainInput.mainScene.NewChildNode.triggered) {
			var newNode = GameManager.inst.netContentManager.NewNode();
			foreach (var selectedNode in GameManager.inst.specialNodeManager.selected_sp.connectedNodes) {
				GameManager.inst.netContentManager.HandleNodeConnection(selectedNode, newNode);
			}
		}
		else if (mainInput.mainScene.NewNode.triggered) 
			GameManager.inst.netContentManager.NewNode();
		#region Backup
		//Load all
		if (mainInput.mainScene.Load.triggered) 
			GameManager.inst.backupManager.LoadDirectory(GameManager.inst.variableManager.netSavePath);
		//Save all
		if (mainInput.mainScene.Save.triggered)
			GameManager.inst.backupManager.SaveNodes(GameManager.inst.specialNodeManager.saverNode_sp.connectedNodes);
		#endregion
		//Connect to selected nodes
		if (mainInput.mainScene.ConnectToSelectedNodes.triggered) 
			GameManager.inst.netContentManager.ConnectSelectedNodes(nodeUnderCursor, ConnectType.Connect, true);
		//Connect selected nodes
		if (mainInput.mainScene.ConnectSelectedNodes.triggered) 
			GameManager.inst.netContentManager.ConnectSelectedNodes(nodeUnderCursor, ConnectType.Connect, false);
		#region Select
		//Select Node under Cursor
		if (mainInput.mainScene.SelectMultiple.inProgress) {
			SelectMultipleNodes();
			selectPressed_lastFrame = true;
		}
		//deselect all nodes and toggle selection of nodeUnderCursor
		else if (mainInput.mainScene.Select.inProgress) {
			SelectNode();
			selectPressed_lastFrame = true;
		}
		else {
			selectPressed_lastFrame = false;
		}
		#endregion

		#region Camera
		moveCamera = mainInput.mainScene.MoveCamera.ReadValue<Vector2>();
		changeCameraSize = MathF.Sign(mainInput.mainScene.ChangeCameraSize.ReadValue<float>());
		#endregion
		#region Cursor position
		mousePosScreen = Mouse.current.position.ReadValue();
		mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
		#endregion


		void SelectNode() {
			//if lastSelectedNode == nodeUnderCursor return. To prevent node from being constantly selected and deselected.
			if (nodeUnderCursor == lastSelectedNode && selectPressed_lastFrame)
				return;
			//figure out if nodeUnderCursor is selected
			bool isSelected = GameManager.inst.specialNodeManager.selected_sp.connectedNodes.Contains(nodeUnderCursor);
			int selectedNodeAmount = 0;
			var selectedNodes = GameManager.inst.specialNodeManager.selected_sp.connectedNodes.ToArray();
			//deselect all nodes
			foreach (var selectedNode in selectedNodes) {
				GameManager.inst.netBehaviourManager.SelectNode(selectedNode, SelectAction.Deselect);
				selectedNodeAmount++;
			}
			//were any other nodes besides nodeUnderCursor selected?
			bool otherNodesSelected = selectedNodeAmount > 1 || (!isSelected && selectedNodeAmount > 0);
			//if other nodes were selected
			if (otherNodesSelected) {
				//select nodeUnderCursor
					GameManager.inst.netBehaviourManager.SelectNode(nodeUnderCursor, SelectAction.Select);
			}
			//if other nodes weren't selected
			else {
				//Toggle selection of nodeUnderCursor
				if (isSelected)
					GameManager.inst.netBehaviourManager.SelectNode(nodeUnderCursor, SelectAction.Deselect);
				else
					GameManager.inst.netBehaviourManager.SelectNode(nodeUnderCursor, SelectAction.Select);
			}
			lastSelectedNode = nodeUnderCursor;
		}
		void SelectMultipleNodes() {
			if (nodeUnderCursor == lastSelectedNode && selectPressed_lastFrame)
				return;
			GameManager.inst.netBehaviourManager.SelectNode(nodeUnderCursor, SelectAction.Toggle);
			lastSelectedNode = nodeUnderCursor;
		}
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
