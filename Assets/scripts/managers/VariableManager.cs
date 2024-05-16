using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    #region prefabs
    public GameObject Node;
    public GameObject Connection;
    public GameObject s;
    public GameObject a;
    #endregion
    public float nodeSelectionRadius;

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
