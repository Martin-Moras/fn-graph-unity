using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public string id { get; private set; }
    public void connectionConstructor(string id, List<string> identifiers, Node outputNode, Node inputNode){
        this.id = id;
        //child
        this.identifiers = identifiers;
        this.inputNode = inputNode;
        this.outputNode = outputNode;
    }
    #region Identifier
    //selected
    public List<string> identifiers {get; private set;}
    public void AddIdentifier(string identifier)
    {
        identifiers.Add(identifier);
        manageSprite();
    }
    public void AddRangeIdentifier(string[] identifier)
    {
        identifiers.AddRange(identifier);
        manageSprite();
    }

    public void RemoveIdentifier(string identifier)
    {
        if (identifiers.Remove(identifier))
        {
            manageSprite();
        }
    }
    #endregion

    [SerializeField] public Node inputNode;
    [SerializeField] public Node outputNode;
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
    public void connectNodes(Node inputNode = null, Node outputNode = null){
        this.inputNode = inputNode;
        this.outputNode = outputNode;
    }
   
    private void updatePos(){
        Vector3[] positions = { joint.transform.position, joint.connectedBody.position };
        connectionRenderer.SetPositions(positions);
    }
    private void manageJoint(){
        if(joint == null){
            joint = inputNode.gameObject.AddComponent<SpringJoint2D>();
            joint.connectedBody = outputNode.gameObject.GetComponent<Rigidbody2D>();
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

    public void manageSprite()
    {
        if (identifiers.Contains("selected")){
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
