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
    #region events
    public delegate void EventHandler();
    #endregion
    #region Singleton
    public static NetInteractionManager Instance { get; private set;}
    void SingletonizeThis()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnRuntimeMethodLoad()
    {
		Instance = FindObjectOfType<NetInteractionManager>();
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
        if (mainInput.mainScene.NewNode.triggered) NetContentManager.Instance.NewNode();
        // if (mainInput.mainScene.ConnectSelectedNodes.triggered) NetManager.Instance.ConnectSelectedNodes();
        // if (mainInput.mainScene.NewChildNode.triggered) NetManager.Instance.NewChildNode();
        if (mainInput.mainScene.Load.triggered) BackupManager.Instance.Load();
        if (mainInput.mainScene.Save.triggered) {
            BackupManager.Instance.SaveNodes(NetContentManager.Instance.GetAllNodes().ToList());
            BackupManager.Instance.SaveTypeLists(NetContentManager.Instance.nodeTypeLists);
        }
        if (mainInput.mainScene.ConnectToSelectedNodes.triggered) NetContentManager.Instance.ConnectSelectedNodes(GetNodeUnderCursor(), true);
        else if (mainInput.mainScene.ConnectSelectedNodes.triggered) NetContentManager.Instance.ConnectSelectedNodes(GetNodeUnderCursor(), false);
        else if (mainInput.mainScene.Select.triggered) NetContentManager.Instance.SelectNodes(new Node[]{GetNodeUnderCursor()});
        //Camera
        moveCamera = mainInput.mainScene.MoveCamera.ReadValue<Vector2>();
        //Manage cursor position
        mousePosScreen = Input.mousePosition;
        mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
    }
    public Node GetNodeUnderCursor(){
        var objectsUnderCursor = Physics2D.OverlapCircleAll(mousePosWorld, VariableManager.Instance.nodeSelectionRadius);

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
