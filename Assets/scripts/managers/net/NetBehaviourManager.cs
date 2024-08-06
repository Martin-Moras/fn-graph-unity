using System;
using MathNet.Numerics.Interpolation;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;


public class NetBehaviourManager : MonoBehaviour
{
	public float springStiffness;
	//how long the spring should be
	public float springRestLenght;
	public float springDamping;
	#region Singleton
	public static NetBehaviourManager Instance { get; private set;}
	void SingletonizeThis()
	{
		if (Instance != null && Instance != this) Destroy(this);
		else Instance = this;
	}
	#endregion
	
	void Awake()
	{
		SingletonizeThis();
	}
	void Update()
	{
		ManageConnectionForces();
	}
	/// <summary>
	/// applyes a spring force to each connection between nodes
	/// </summary>
    private void ManageConnectionForces(){
        foreach (var node in NetContentManager.Instance.GetAllNodes())
		{
			foreach (var connectedNode in node.connectedNodes)
			{
				var relativeNodePos = connectedNode.transform.position - node.transform.position;
				var nodeDistance = relativeNodePos.magnitude;
				//the difference between node distance and how far they should be apart
				var offsetFromSpringRestLenght = nodeDistance - springRestLenght;
				//get rigitbodys from node and connectedNode
				var connectedNodeRb = connectedNode.GetComponent<Rigidbody2D>();
				var nodeRb = node.GetComponent<Rigidbody2D>();
				//get the velocity relative to the direction of the other node
				var nodeSpringVel = Vector2.Dot(nodeRb.velocity, relativeNodePos.normalized);
				var connectedNodeSpringVel = Vector2.Dot(connectedNodeRb.velocity, relativeNodePos.normalized);
				//springVel shows how fast the spring is expanding/retracting
				var springVel = nodeSpringVel - connectedNodeSpringVel;
				//apply the damping coeficent to springVel
				var dampingForce = springVel * springDamping;
				
				//calcualte the force that should be applied both nodes
				var springForce = offsetFromSpringRestLenght * springStiffness - dampingForce;
				//apply spring force
				nodeRb.AddForce(relativeNodePos.normalized * springForce);
				connectedNodeRb.AddForce(relativeNodePos.normalized * -springForce);
			}
		}
    }
}