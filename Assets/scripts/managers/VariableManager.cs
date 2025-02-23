using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MathNet.Numerics.Providers.FourierTransform;
using UnityEngine;

public class VariableManager : I_Manager
{
	#region prefabs
	public GameObject NodePrefab;
	public GameObject ConnectionPrefab;
	#endregion prefabs
	public float nodeSelectionRadius;
	#region Camera
	public float cameraMoveSpeed;
	public float cameraSizechangeSpeed;
	public float cameraDefaultSize;
	#endregion Camera
	#region Save/Load
	public string rootPath {get; private set;}
	public string netSavePath {get; private set;}
	public string relativeSpecialNodeSavePath;
	public char[] saveFileSeperatorStr = new[] {';'};
	public string netSaveFileExtentionRegex = @"\.fnet$";
	public string netSaveFileExtention = @".fnet";
	#endregion Save/Load
	#region Special nodes
	public string specialNodeName = "Special node";
	#endregion Special nodes
	#region Visuals
	public Material oneWayConnectionMat;
	public Material twoWayConnectionMat;
	#endregion
	private uint lastGeneratedId;
	public override void Initialize()
	{
		rootPath = Application.dataPath;
		netSavePath = Path.Combine(rootPath, "saved_nets");
		relativeSpecialNodeSavePath = specialNodeName;
		relativeSpecialNodeSavePath = Path.ChangeExtension(relativeSpecialNodeSavePath, netSaveFileExtention);

	}
	public uint GenerateId()
	{
		DateTime currentDateTime = DateTime.Now;
        // Get the start of the current year
        DateTime startOfYear = new DateTime(currentDateTime.Year, 1, 1);
        // Calculate the time difference between the start of the year and now
        TimeSpan timeElapsed = currentDateTime - startOfYear;
        // Convert the time difference to milliseconds
		uint generatedId = (uint)(timeElapsed.TotalMilliseconds / 10);
		//Prevent id duplicates
		if (generatedId <= lastGeneratedId)
			generatedId = lastGeneratedId + 1;
		lastGeneratedId = generatedId;
        return generatedId;
	}

	public override void ManagerUpdate()
	{
	}
}