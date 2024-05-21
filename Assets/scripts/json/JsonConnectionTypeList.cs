using System;

[Serializable]
public class JsonConnectionTypeList{
    public string name;
    public string id;
    public string[] connectionIds;
    //behaviour
    //color
    public float color_r;
    public float color_g;
    public float color_b;
    public float color_a;
    //requirements
    public string connectionName; 
    public string inNodeName;
    public string inNodeId;
    public string outNodeName;
    public string outNodeId;
}