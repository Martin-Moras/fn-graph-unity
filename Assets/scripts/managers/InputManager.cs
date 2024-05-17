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
    public delegate void EventHandler();
    public static event EventHandler newNode;
    public static event EventHandler nodeSelected;
    public static event EventHandler load;
    public static event EventHandler save;
    public static event EventHandler newChildNode;
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
        if (mainInput.mainScene.NewNode.triggered && newNode != null) newNode();
        if (mainInput.mainScene.Select.triggered && nodeSelected != null) nodeSelected();
        if (mainInput.mainScene.NewChildNode.triggered && newChildNode != null) newChildNode();
        //backup
        if (mainInput.mainScene.Load.triggered && load != null) load();
        if (mainInput.mainScene.Save.triggered && save != null) save();
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
