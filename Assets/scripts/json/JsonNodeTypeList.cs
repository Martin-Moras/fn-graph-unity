using System;

[Serializable]
public class JsonNodeTypeList{
    public string name;
    public string id;
    public string[] nodeIds;
    //behaviour
    //color
    public float color_r;
    public float color_g;
    public float color_b;
    public float color_a;
    //requirements
    public string nodeName; 
    public string inGoingConnectionName; 
    public string inGoingConnectionId; 
    public string outGoingConnectionName;
    public string outGoingConnectionId;

}