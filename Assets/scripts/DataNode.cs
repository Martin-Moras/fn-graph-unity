using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataNode
{
    public string nodePath; //{ get; private set; } add it back !!!!!
    public uint nodeId; //{ get; private set; } add it back !!!!!
    public List<uint> connectedNodeIds { get; set; } = new();
    public List<DataNode> connectedNodes { get; set; } = new();
    public DataNode(string nodePath, uint nodeId, List<uint> connectedNodeIds) 
    {
        if(this.nodePath == "" || this.nodePath == null) 
            this.nodePath = UnityEngine.Random.Range(0, 500000).ToString();
        else
			this.nodePath = nodePath;
        this.nodeId = nodeId;
        if (connectedNodeIds == null) this.connectedNodeIds = new();
        else this.connectedNodeIds = connectedNodeIds;
    }
}
