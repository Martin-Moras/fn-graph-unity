using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTypeList
{
    public string listPath;
    public List<Node> nodes;
    public Color color;
    public TypeListRequirement[] requirements;

    public NodeTypeList(string listPath, List<Node> nodes, Color color, TypeListRequirement[] requirements)
    {
        this.listPath = listPath;
        this.nodes = nodes;
        this.color = color;
        this.requirements = requirements;
    }
}
