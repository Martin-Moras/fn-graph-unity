using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Node : MonoBehaviour
{
    public string id { get; private set; }
    //selected
    public List<string> identifiers;
    public List<Connection> connections;
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
