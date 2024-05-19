using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public string connectionId { get; private set; }
    public string connectionName;
    public void connectionConstructor(string id, string name, Node inputNode, Node outputNode){
        this.connectionId = id;
        this.connectionName = name;
        this.inNode = inputNode;
        this.outNode = outputNode;
    }
    [SerializeField] public Node inNode;
    [SerializeField] public Node outNode;
	TextMeshPro text;
	SpringJoint2D joint;
    LineRenderer connectionRenderer;


    void Awake(){
        if(connectionId == "" || connectionId == null) 
        connectionId = UnityEngine.Random.Range(0, 500000).ToString();
    }
    void Start()
    {
        initializeVariables();
    }
    void Update()
    {
        manageJoint();
        updatePos();
    }
    private void updatePos(){
        Vector3[] positions = { joint.transform.position, joint.connectedBody.position };
        connectionRenderer.SetPositions(positions);
    }
    private void manageJoint(){
        if(joint == null){
            joint = inNode.gameObject.AddComponent<SpringJoint2D>();
            joint.connectedBody = outNode.gameObject.GetComponent<Rigidbody2D>();
            joint.autoConfigureDistance = false;
            joint.distance = 3;
        }
    }
	private void initializeVariables(){
		connectionRenderer = GetComponent<LineRenderer>();
		//Text
		text = gameObject.AddComponent<TextMeshPro>();
		text.fontSize = 5;
		text.alignment = TextAlignmentOptions.Midline;
	}
}
