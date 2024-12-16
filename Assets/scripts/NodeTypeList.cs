using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTypeList
{
    public string listPath;
    public List<DataNode> nodes;
    public Color color;
    public TypeListRequirement[] requirements;

    public NodeTypeList(string listPath, List<DataNode> nodes, Color color, TypeListRequirement[] requirements)
    {
        this.listPath = listPath;
        if (nodes == null) this.nodes = new List<DataNode>();
        else this.nodes = nodes;
        this.color = color;
        this.requirements = requirements;
    }
}
