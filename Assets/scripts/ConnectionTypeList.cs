using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionTypeList
{
    public string listName;
    public string listId;
    public List<Connection> connections;
    public Color color;
    //requirements
    public string connectionName {get; private set;} 
    public string inNodeName {get; private set;} 
    public string inNodeId {get; private set;} 
    public string outNodeName {get; private set;}
    public string outNodeId {get; private set;}

    public ConnectionTypeList(string listName, string listId, List<Connection> connections, Color color, string connectionName, string inNodeName, string inNodeId, string outNodeName, string outNodeId)
    {
        this.listName = listName;
        this.listId = listId;
        this.connections = connections;
        this.color = color;
        this.connectionName = connectionName;
        this.inNodeName = inNodeName;
        this.inNodeId = inNodeId;
        this.outNodeName = outNodeName;
        this.outNodeId = outNodeId;
    }
}

