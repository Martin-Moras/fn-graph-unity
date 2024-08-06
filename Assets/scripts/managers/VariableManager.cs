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
    #endregion prefabs
    public float nodeSelectionRadius;
    #region Camera
    public float cameraMoveSpeed;
    public float cameraSizechangeSpeed;
    public float cameraDefaultSize;
    #endregion Camera
    #region Save/Load
    public string netSavePath;
    #endregion Save/Load
    #region string pattern
    //a string starts with:"o-"
    public string originNodePattern = @"^o-";
    public string originNodeString = @"o-";
    //a string starts with:"i" + <some number> + "-"
    public string instanceNodePattern = @"^i\d+-";
    public string nodeFileEndingPattern = @"\.node$";
    public string nodeFileEndingString = @".node";
    #endregion string pattern
    #region Singleton
    public static VariableManager inst { get; private set;}
    void SingletonizeThis()
    {
        if (inst != null && inst != this) Destroy(this);
        else inst = this;
    }
    #endregion Singleton
    void Awake()
    {
        SingletonizeThis();
    }
}