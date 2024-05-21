using System;

[Serializable]
public class JsonNet{
    public string id;
    public string name;
    public JsonNode[] nodes;
    public JsonConnection[] connections;
    public JsonNodeTypeList[] nodeTypeLists;
    public JsonConnectionTypeList[] connectionTypeLists;
}