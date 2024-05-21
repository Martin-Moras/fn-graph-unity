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
        foreach (var node in NetManager.Instance.GetAllNodes()){
            node.GetComponent<SpriteRenderer>().color = Color.red;
            
            foreach (var connection in NetManager.Instance.GetConnections(node))
            {
                if (connection.outNode.nodeName == "selected") 
                node.GetComponent<SpriteRenderer>().color = Color.white;
                
            }
        }
    }
}