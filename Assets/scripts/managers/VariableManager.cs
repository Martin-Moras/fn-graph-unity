using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    #region prefabs
    public GameObject NodePefab;
    public GameObject ConnectionPrefab;
    public GameObject s;
    public GameObject a;
    #endregion
    public float nodeSelectionRadius;
    public float cameraMoveSpeed;
    #region Save/Load
    public string netSavePath;
    #endregion
    #region Singleton
    public static VariableManager Instance { get; private set;}
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
}
