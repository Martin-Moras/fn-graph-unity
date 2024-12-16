using System;
using MathNet.Numerics.Interpolation;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;


public class NetBehaviourManager : I_Manager
{
	public DataNode allNodes_sp;
	public DataNode visible_sp;
	public DataNode selected_sp;
	public DataNode summerize_sp;
	public DataNode category_sp;
	public List<DataNode> saverNodes_sp;
	#region Singleton
	public static NetBehaviourManager inst { get; private set;}
	void SingletonizeThis()
	{
		if (inst != null && inst != this) Destroy(this);
		else inst = this;
	}
	#endregion
	
	public override void Initiallize()
	{
		SingletonizeThis();
	}

}