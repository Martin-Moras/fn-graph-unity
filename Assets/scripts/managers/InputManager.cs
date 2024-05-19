using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
    public delegate void EventHandler();/*
    public static event EventHandler newNode;
    public static event EventHandler nodeSelected;
    public static event EventHandler load;
    public static event EventHandler save;
    public static event EventHandler newChildNode;*/
    #endregion
    #region Singleton
    public static InputManager Instance { get; private set;}
    void SingletonizeThis()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
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
        if (mainInput.mainScene.Select.triggered) NetManager.Instance.NodeSelected();
        if (mainInput.mainScene.NewChildNode.triggered) NetManager.Instance.NewChildNode();
        if (mainInput.mainScene.Load.triggered) NetManager.Instance.Load();
        if (mainInput.mainScene.Save.triggered) NetManager.Instance.Save();
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
