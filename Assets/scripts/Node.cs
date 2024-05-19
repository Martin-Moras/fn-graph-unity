using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    public string nodeId { get; private set; }
    public string nodeName { get; set;}
    public void NodeConstructor(string id, string name){
        this.nodeId = id;
        this.nodeName = name;
    }
    void Awake(){
        if(nodeId == "" || nodeId == null) 
        nodeId = UnityEngine.Random.Range(0, 500000).ToString();
    }
}
