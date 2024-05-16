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
    #region events
    public delegate void EventHandler();
    public static event EventHandler newNode;
    public static event EventHandler nodeSelected;
    public static event EventHandler load;
    public static event EventHandler save;
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
    public void onNewNode(){
        if (newNode != null) newNode();
    }
    public void onSelectNode(){
        if (newNode != null) nodeSelected();
    }
    public void onLoad(){
        if (newNode != null) load();
    }
    public void onSave(){
        if (newNode != null) save();
    }

    void Update()
    {
        manageInputs();
    }
    private void manageInputs(){
        if (mainInput.mainScene.NewNode.triggered) onNewNode();
        if (mainInput.mainScene.Select.triggered) onSelectNode();
        if (mainInput.mainScene.Load.triggered) onLoad();
        if (mainInput.mainScene.Save.triggered) onSave();
        //Manage curson position
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
