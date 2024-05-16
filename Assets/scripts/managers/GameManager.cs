using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set;}
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
    
    void Update()
    {
        
    }
}
