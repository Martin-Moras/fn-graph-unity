using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Connection : MonoBehaviour
{
	[SerializeField] Transform inputNode;
    [SerializeField] Transform outputNode;
	TextMeshPro text;
    Color color;
	SpringJoint2D joint;
    LineRenderer connectionRenderer;


    void Start()
    {
        initializeVariables();
    }

    // Update is called once per frame
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
            joint = inputNode.AddComponent<SpringJoint2D>();
            joint.connectedBody = outputNode.GetComponent<Rigidbody2D>();
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
