using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MathNet.Numerics;
using UnityEngine.Assertions.Must;

public class VisualNode : MonoBehaviour
{
    public DataNode dataNode { get; private set; }
    public List<Connection> connections { get; set; }
    public SpriteRenderer sprite { get; private set; }
	public TextMeshPro text { get; private set; }
    public uint size { get; private set; }
    public void NodeConstructor(DataNode dataNode, List<Connection> connections){
		if (dataNode == null) {
			Debug.LogWarning("Visual node constructor: dataNode can't be null");
			Destroy(gameObject);
			return;
		}
		sprite = GetComponent<SpriteRenderer>();
		text = GetComponentInChildren<TextMeshPro>();
        this.dataNode = dataNode;
        //set the name of the node gameobject to "path" for easier testing in the unity editor
		gameObject.name = this.dataNode.nodePath.ToString();
		SetText(this.dataNode.nodePath.ToString());
        if (connections == null) 
            this.connections = new();
        else 
            this.connections = connections;

		// connections == null ? this.connections = connections | this.connections = new();
    }
	public void SetText(string str){
		text.text = str;
	}
	public void SetSize(uint size)
	{
		this.size = size; 
	}
}
