using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTypeList
{
    public string listName;
    public string listId;
    public List<Node> nodes;
    public Color color;
    public string nodeName {get; private set;} 
    public string inGoingConnectionName {get; private set;} 
    public string inGoingConnectionId {get; private set;} 
    public string outGoingConnectionName {get; private set;}
    public string outGoingConnectionId {get; private set;}

    public NodeTypeList(string listName, string listId, List<Node> nodes, Color color, string nodeName, string inGoingConnectionName, string inGoingConnectionId, string outGoingConnectionName, string outGoingConnectionId)
    {
        this.listName = listName;
        this.listId = listId;
        this.nodes = nodes;
        this.color = color;
        this.nodeName = nodeName;
        this.inGoingConnectionName = inGoingConnectionName;
        this.inGoingConnectionId = inGoingConnectionId;
        this.outGoingConnectionName = outGoingConnectionName;
        this.outGoingConnectionId = outGoingConnectionId;
    }
}

