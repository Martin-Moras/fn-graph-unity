using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Node : MonoBehaviour
{
    public string id { get; private set; }
    public void NodeConstructor(string id, List<string> identifiers, List<Connection> connections){
        this.id = id;
        this.identifiers = identifiers;
        this.connections = connections;

    }
    //selected
    public List<string> identifiers;
    public List<Connection> connections;
    void Awake(){
        if(id == "" || id == null) 
        id = UnityEngine.Random.Range(0, 500000).ToString();
    }
    void Start()
    {
        
    }
    public void hideConnectedNodes(){
        foreach (Connection connection in connections){
            //connection.outputNode.
        }
    } 
    // Update is called once per frame
    void Update()
    {
        
    }
}
