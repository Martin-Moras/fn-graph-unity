using System;

[Serializable]
public class JsonConnection{
    public string id;
    public string[] identifiers;
    public string inputNodeId;
    public string outputNodeId;

    public float colorR;
    public float colorG;
    public float colorB;
    public float colorA;
}