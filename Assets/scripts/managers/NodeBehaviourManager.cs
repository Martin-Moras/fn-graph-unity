using System;
using MathNet.Numerics.Interpolation;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;


public class NodeBehaviourManager : MonoBehaviour
{
	#region Singleton
	
	public static NodeBehaviourManager Instance { get; private set;}
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
	}
    private void ManageConnectionForces(){
        
    }
}