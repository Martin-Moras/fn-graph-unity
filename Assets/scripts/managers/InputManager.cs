using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor.Scripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputActionReference inputAction;
    #region events
    // Declare the delegate (if using non-generic pattern).
    public delegate void EventHandler();
    public static event EventHandler newNode;
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
    }
    public void onNewNode(){
        if (newNode != null) newNode();
    }

    void Update()
    {
        manageInputs();
    }
    private void manageInputs(){
        //Add node
        if (inputAction.action.ReadValue<float>() != 0) {
            onNewNode();
            Debug.Log("button pressed");
        }
    }
}
