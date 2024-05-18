using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    public string nodeId { get; private set; }
    public string nodeName { get; set;}
    public void NodeConstructor(string id, string name, List<Connection> connections){
        this.nodeId = id;
        this.connections = connections;
        this.nodeName = name;
    }
    //selected


    public List<Connection> connections;
    void Awake(){
        if(nodeId == "" || nodeId == null) 
        nodeId = UnityEngine.Random.Range(0, 500000).ToString();
    }
    void Start()
    {
        
    }
	private void Update() {
		manageSprite();
	}
    public void hideConnectedNodes(){
        foreach (Connection connection in connections){
        }
    } 
    public void manageSprite(){
        foreach (Connection connection in connections.FindAll(x=>x.connectionName=="identifier")){
            if (!connection.connectionName.Contains("identifier"))continue;
			GetComponent<SpriteRenderer>().color = Color.yellow;
            if (connection.outputNode.nodeName.Contains("selected")) GetComponent<SpriteRenderer>().color = Color.white;
            
        }
    }
    // Update is called once per frame
    public void DeleteNode()
    {
        foreach (var connection in connections)
        {
            if (connection.inputNode == this) connection.inputNode = null;
            if (connection.outputNode == this) connection.outputNode = null;
        }
        Destroy(gameObject);
    }
}
