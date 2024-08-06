using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor.Scripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]public MainInput mainInput;
    public Vector2 mousePosWorld;
    public Vector2 mousePosScreen;
    public Vector2 moveCamera;
    #region events
    public delegate void EventHandler();
    #endregion
    #region Singleton
    public static InputManager Instance { get; private set;}
    void SingletonizeThis()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnRuntimeMethodLoad()
    {
		Instance = FindObjectOfType<InputManager>();
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
        manageInputs();
    }
    private void manageInputs(){
        if (mainInput.mainScene.NewNode.triggered) NetManager.Instance.NewNode();
        // if (mainInput.mainScene.ConnectSelectedNodes.triggered) NetManager.Instance.ConnectSelectedNodes();
        if (mainInput.mainScene.Select.triggered) NetManager.Instance.SelectNode();
        // if (mainInput.mainScene.NewChildNode.triggered) NetManager.Instance.NewChildNode();
        if (mainInput.mainScene.Load.triggered) BackupManager.Instance.Load();
        if (mainInput.mainScene.Save.triggered) {
            BackupManager.Instance.SaveNodes(NetManager.Instance.GetAllNodes().ToList());
            BackupManager.Instance.SaveTypeLists(NetManager.Instance.nodeTypeLists);
        }
        //Camera
        moveCamera = Vector2.zero;
        moveCamera += mainInput.mainScene.MoveCamera.ReadValue<Vector2>();
        //Manage cursor position
        mousePosScreen = Input.mousePosition;
        mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
    }
    private void OnEnable(){
        mainInput.Enable();
    }
    private void OnDisable(){
        mainInput.Disable();
    }
}
