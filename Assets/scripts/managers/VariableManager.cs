using System.Collections;
using System;
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
	public string netSavePath = "saved_nets";
	public string nodeSavePath;
	public string typeListSavePath;
	public string saveFileSeperatorStr;
	#endregion Save/Load
	#region string pattern
	public string netSaveFileExtentionPattern = @"\.fnet$";
	public string netSaveFileExtention = @".fnet";
	#endregion string pattern
	private uint lastGeneratedId;
	#region Singleton
	public static VariableManager inst { get; private set;}
	private void SingletonizeThis()
	{
		if (inst != null && inst != this) Destroy(this);
		else inst = this;
	}
	#endregion Singleton
	public override void Initiallize()
	{
		SingletonizeThis();
		rootPath = Application.dataPath;
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
}