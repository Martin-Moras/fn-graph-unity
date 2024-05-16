using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NetManager : MonoBehaviour
{
    #region Singleton
    
    public static NetManager Instance { get; private set;}
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

    // Update is called once per frame
    void Update()
    {
        
    }
    void newNode(){
        Instantiate(VariableManager.Instance.Node);
    }
    void OnEnable(){
        InputManager.newNode += newNode;
    }
    void OnDisable(){
        InputManager.newNode -= newNode;
    }
}
