using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualsManager : MonoBehaviour
{
    #region Singleton
    public static VisualsManager Instance { get; private set;}
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
        manageSprite();
    }
    public void manageSprite(){
        foreach (var connection in NetManager.Instance.identifierConnections){
            if (connection.outNode.nodeName == "selected") connection.inNode.GetComponent<SpriteRenderer>().color = Color.white;
            else connection.inNode.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}