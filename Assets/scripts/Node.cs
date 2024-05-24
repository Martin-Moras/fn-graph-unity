using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    public string nodePath { get; private set; }
    public List<Node> nodes{ get; set; }
    public void NodeConstructor(string path, List<Node> nodes){
        this.nodePath = path;
        this.nodes = nodes;
    }
    void Awake(){
        if(nodePath == "" || nodePath == null) 
        nodePath = UnityEngine.Random.Range(0, 500000).ToString();
    }
}
