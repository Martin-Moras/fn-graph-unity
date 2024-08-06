using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    public string nodePath { get; private set; }
    public List<Node> connectedNodes{ get; set; } = new();
    public List<Connection> connections{ get; set; } = new();
    public void NodeConstructor(string path, List<Node> nodes, List<Connection> connections){
        this.nodePath = path;
        if (nodes == null) nodes = new();
        else this.connectedNodes = nodes;
        if (connections == null) connections = new();
        else this.connections = connections;
        
    }
    void Awake(){
        if(nodePath == "" || nodePath == null) 
            nodePath = UnityEngine.Random.Range(0, 500000).ToString();
    }
}
