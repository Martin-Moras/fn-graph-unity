using System.Collections;
using System.Collections.Generic;
using System;

public class DataNode
{
    public string nodePath { get; private set; }
    public uint nodeId { get; private set; }
    public List<uint> connectedNodeIds { get; set; } = new();
    public List<DataNode> connectedNodes { get; set; } = new();
    public DataNode(string nodePath, uint nodeId, List<uint> connectedNodeIds) 
    {
		this.nodePath = nodePath;
        this.nodeId = nodeId;
        if (connectedNodeIds == null) this.connectedNodeIds = new();
        else this.connectedNodeIds = connectedNodeIds;
        if (connectedNodeIds == null) this.connectedNodeIds = new();
        else this.connectedNodeIds = connectedNodeIds;
    }
	public void SetPath(string path)
	{
		this.nodePath = path;
	}
}
