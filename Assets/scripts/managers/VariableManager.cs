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
	public string netSavePath;
	public string specialNodesSavePath;
	public char[] saveFileSeperatorStr = new[] {';'};
	public string netSaveFileExtentionPattern = @"\.fnet$";
	public string netSaveFileExtention = @".fnet";
	#endregion Save/Load
	private uint lastGeneratedId;
	#region Singleton
	public static VariableManager inst { get; private set;}
	public override void SingletonizeThis()
	{
		if (inst != null && inst != this) Destroy(this);
		else inst = this;
	}
	#endregion Singleton
	public override void Initialize()
	{
		SingletonizeThis();
		rootPath = Application.dataPath;
		netSavePath = Path.Combine(rootPath, "saved_nets");
		specialNodesSavePath = Path.Combine(netSavePath, "special_nodes", netSaveFileExtention);
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
		throw new NotImplementedException();
	}
}