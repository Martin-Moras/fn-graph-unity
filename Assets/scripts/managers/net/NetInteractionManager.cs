using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor.Scripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetInteractionManager : MonoBehaviour
{
    [SerializeField]public MainInput mainInput;
    public Vector2 mousePosWorld;
    public Vector2 mousePosScreen;
    public Vector2 moveCamera;
    public float changeCameraSize;
    #region events
    public delegate void EventHandler();
    #endregion
    #region Singleton
    public static NetInteractionManager inst { get; private set;}
    void SingletonizeThis()
    {
        if (inst != null && inst != this) Destroy(this);
        else inst = this;
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnRuntimeMethodLoad()
    {
		inst = FindObjectOfType<NetInteractionManager>();
	}
    #endregion
    void Awake()
    {
        SingletonizeThis();
        mainInput = new();
        //Add node
        //mainInput.mainScene.NewNode.started += context => onNewNode();
        //Select node
        //mainInput.mainScene.Select.started += context => nodeSelected();
    }

    void Update()
    {
        ManageInputs();
    }
    private void ManageInputs(){
        if (mainInput.mainScene.NewNode.triggered) NetContentManager.inst.NewNode();
        // if (mainInput.mainScene.ConnectSelectedNodes.triggered) NetManager.inst.ConnectSelectedNodes();
        // if (mainInput.mainScene.NewChildNode.triggered) NetManager.inst.NewChildNode();
        if (mainInput.mainScene.Load.triggered) BackupManager.inst.Load();
        if (mainInput.mainScene.Save.triggered) {
            BackupManager.inst.SaveNodes(NetContentManager.inst.GetAllNodes().ToList());
            BackupManager.inst.SaveTypeLists(NetContentManager.inst.nodeTypeLists);
        }
        if (mainInput.mainScene.ConnectToSelectedNodes.triggered) NetContentManager.inst.ConnectSelectedNodes(GetNodeUnderCursor(), true);
        else if (mainInput.mainScene.ConnectSelectedNodes.triggered) NetContentManager.inst.ConnectSelectedNodes(GetNodeUnderCursor(), false);
        else if (mainInput.mainScene.Select.triggered) NetContentManager.inst.SelectNodes(new Node[]{GetNodeUnderCursor()});
        //Camera
        moveCamera = mainInput.mainScene.MoveCamera.ReadValue<Vector2>();
        changeCameraSize = MathF.Sign(mainInput.mainScene.ChangeCameraSize.ReadValue<float>());
        //Manage cursor position
        mousePosScreen = Input.mousePosition;
        mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
    }
    public Node GetNodeUnderCursor(){
        var objectsUnderCursor = Physics2D.OverlapCircleAll(mousePosWorld, VariableManager.inst.nodeSelectionRadius);

        Collider2D closestCollider = objectsUnderCursor.FirstOrDefault();
        foreach (var objectUnderCursor in objectsUnderCursor)
        {
            if (objectUnderCursor.GetComponent<Node>() == null) continue;
            var currObjectDist = ((Vector2)objectUnderCursor.transform.position - mousePosWorld).magnitude;
            var closestColliderDist = ((Vector2)closestCollider.transform.position - mousePosWorld).magnitude;
            //if the current objectUnderCursor is closer than closestCollider set closestCollider to it
            if (currObjectDist < closestColliderDist) closestCollider = objectUnderCursor;
        }
        if (closestCollider == null) return null;
        return closestCollider.GetComponent<Node>();
    }
    private void OnEnable(){
        mainInput.Enable();
    }
    private void OnDisable(){
        mainInput.Disable();
    }
}
