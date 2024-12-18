using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualNode : MonoBehaviour
{
    public DataNode dataNode { get; private set; }
    public List<Connection> connections { get; set; } = new();
    public SpriteRenderer rederer;
    public uint size;
    public void NodeConstructor(DataNode dataNode, List<Connection> connections){
        this.dataNode = dataNode;
        //set the name of the node gameobject to "path" for easier testing in the unity editor
		gameObject.name = this.dataNode.nodeId.ToString();
        if (connections == null) 
            this.connections = new();
        else 
            this.connections = connections;
    }
    void Awake(){
        if(this.dataNode.nodePath == "" || this.dataNode.nodePath == null) 
            this.dataNode.nodePath = UnityEngine.Random.Range(0, 500000).ToString();
    }
}
