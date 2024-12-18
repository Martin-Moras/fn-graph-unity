using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using System.Text;
using System.Data.Common;

public class BackupManager : I_Manager
{
	string netDir;

	#region Singleton
	public static BackupManager inst { get; private set;}
	public override void SingletonizeThis()
	{
		if (inst != null && inst != this) Destroy(this);
		else inst = this;
	}
	#endregion
	public override void Initialize()
	{
	}
	private void Start() {
		InitializeDirectories();
		
	}
	private void InitializeDirectories(){
		netDir = VariableManager.inst.netSavePath;
		//Create net directory
		CreateDirIfDoesntExist(netDir);
	}
	#region Save
	public List<string> SaveNodes(List<DataNode> saverNodes)
	{
		var netSaveFilePaths = new List<string>();
		foreach (var saverNode in saverNodes)
		{
			netSaveFilePaths.Add(SaveNode(saverNode));
		}
		return netSaveFilePaths;
	}
	public string SaveNode(DataNode saverNode)
	{
		string netSaveFilePath;
		if (saverNode.nodePath == null || saverNode.nodePath == "") 
			netSaveFilePath = Path.Combine(netDir, "unnamed");
		else 
			netSaveFilePath = Path.Combine(netDir, saverNode.nodePath);
		if (GetPathType(netSaveFilePath) != PathType.File) {
			Debug.LogError($"The name {saverNode.nodePath} is invalid for a Saver-node");
			return null;
		}
		var nodeFile = File.Create(netSaveFilePath);
		nodeFile.Close();
		var saveFileBuilder = new StringBuilder();
		foreach (var node in saverNode.connectedNodes) {
			//Add node Id
			saveFileBuilder.Append(node.nodeId);
			saveFileBuilder.Append(VariableManager.inst.saveFileSeperatorStr);
			//Add node Name
			saveFileBuilder.Append(node.nodePath);
			saveFileBuilder.Append(VariableManager.inst.saveFileSeperatorStr);

			//Add connected nodes
			foreach (var connectedNode in node.connectedNodes)
			{
				saveFileBuilder.Append(connectedNode.nodeId);
				saveFileBuilder.Append(VariableManager.inst.saveFileSeperatorStr);
			}
			//Add new line
			saveFileBuilder.AppendLine();
		}
		File.WriteAllText(netSaveFilePath, saveFileBuilder.ToString());
		nodeFile.Close();
		return netSaveFilePath;
	}
	#endregion Save
	#region Load
	/// <summary>
	/// if saveFilePath is a valid file it will load the file.
	/// if saveFilePath is invalide it will print out an error.
	/// if saveFilePath is a valid folder it will load all .fnet files and its subdirectories untill maxFolderDepth is reached
	/// if maxFolderDepth is smaller than 0 it will load all subdirectories
	/// the Load function returns all Saver-Nodes it has loaded
	/// if no .fnet files were found it will return an empty list
	/// if saveFilePath is invalide it will return null and print out an error
	/// </summary>
	/// <param name="saveFilePath"></param>
	/// <returns></returns>
	public List<DataNode> LoadDirectory(string saveFilePath, int maxFolderDepth = -1)
    {
		var newSaverNodes = new List<DataNode>();
		var pathType = GetPathType(saveFilePath);
		List<string> filePaths;
		//return null if path is invalide
		if (pathType == PathType.Invalid)
			return null;
		//if is file
		if (pathType == PathType.File)
			filePaths = new() {saveFilePath};
		//if is directory
		else
			filePaths = GetFilePaths(saveFilePath, VariableManager.inst.netSaveFileExtention, maxFolderDepth);
		//turn each file in "filePaths" into Saver-nodes and add them to "newSaverNodes"
		foreach (var filePath in filePaths) {
			newSaverNodes.Add(LoadFile(filePath, false));
		}
		//return all loaded Saver-nodes and from path and its subdirectories
		return newSaverNodes;
    }
	public DataNode LoadFile(string filePath, bool checkPathValidity = true)
	{
		if (checkPathValidity) {
			var pathType = GetPathType(filePath);
			//return null if path is invalide
			if (pathType != PathType.File)
				return null;
		}
		var deserializedNodes = new List<DataNode>();
		//get file contents from "filePath"
		var fileContents = File.ReadAllText(filePath);
		//split "fileContents" into its lines
		var lines = fileContents.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();
		//deserialize each line into a "DataNode" and add it into "deserializedNodes"
		foreach (var line in lines){
			deserializedNodes.Add(DeserializeNode(line));
		}
		//create a saverNode
		var saverNode = NetBehaviourManager.inst.NewSaverNode(
			VariableManager.inst.GenerateId(), 
			deserializedNodes, 
			Path.GetFileNameWithoutExtension(filePath));
		SpecialNodeManager.inst.AddSaverNode(saverNode);
		return saverNode;
	}
	/// <summary>
	/// Create a new node based on the contents of "saveFileLine"
	/// </summary>
	/// <param name="saveFileLine"></param>
	/// <returns></returns>
	DataNode DeserializeNode(string saveFileLine)
	{
		uint nodeId;
		string nodePath;
		var connectedNodeIds = new List<uint>();

		if (saveFileLine == null)
			return null;
		//Split line into:
		//nodeId
		//nodePath
		//connected node ids
		var nodeParameters = saveFileLine.Split(VariableManager.inst.saveFileSeperatorStr, StringSplitOptions.RemoveEmptyEntries);
		//get "nodeId" and return null if it isn't a uint
		if (!uint.TryParse(nodeParameters[0], out nodeId)) {
			Debug.LogWarning($"the line :{saveFileLine} has an invalid Id. It must be parsable to an uint");
			return null;
		}
		//get "nodePath"
		nodePath = nodeParameters[1];
		//Create a list with connected node ids
		for(int i = 2; i < nodeParameters.Length; i++) {
			//get "connectedNodeId" and continue to the next connectedNodeId if it isn't a uint
			if (!uint.TryParse(nodeParameters[i - 2], out uint connectedNodeId)) {
				Debug.LogWarning($"in the line :{saveFileLine} is the {i - 2}th connected node invalide. It must be parsable to an uint");
				continue;
			}
			connectedNodeIds.Add(connectedNodeId);
		}
		//Create a new node based on the contents of "saveFileLine"
		DataNode newNode = NetContentManager.inst.NewNode(nodeId, connectedNodeIds, nodePath);
		return newNode;
	}
	List<string> GetFilePaths(string directoryPath, string extention, int maxFolderDepth, int depth = 0)
	{
		var outFilePaths = new List<string>();
		//Get all files in "directoryPath" and add them to "outFilePaths"
		foreach (var filePath in Directory.GetFiles(directoryPath, $"*.{extention}"))
			outFilePaths.Add(filePath);
		//loop through all subdirectories if "maxFolderDepth" isn't reached and add their contents to "outFilePaths"
		foreach (var dir in Directory.GetDirectories(directoryPath)){
			if (depth == maxFolderDepth)
				break;
			//add all files that are contained in "dir"
			outFilePaths.AddRange(GetFilePaths(dir, extention, maxFolderDepth, depth + 1));
		}
		//return all files in "directoryPath" and in its subdirectories
		return outFilePaths;
	}
	#endregion
	private void CreateDirIfDoesntExist(string dirPath) 
	{
		if (!Directory.Exists(Path.GetDirectoryName(dirPath))) 
			Directory.CreateDirectory(Path.GetDirectoryName(dirPath));
	}
	PathType GetPathType(string path)
	{
		//If path is a file
		if (File.Exists(path)) {
			//If the file hasn't the .fnet extention it is invalide
			if (Path.GetExtension(path) != VariableManager.inst.netSaveFileExtention) {
				Debug.LogError($"the file: {path} doesn't have the {VariableManager.inst.netSaveFileExtention}");
				return PathType.Invalid;
			}
			//If file has the .fnet connection
			return PathType.File;
		}
		//If path is a folder
		if (Directory.Exists(path))
			return PathType.Directory;
		//If path is no folder and no file it has to be invalide
		return PathType.Invalid;
	}

	public override void ManagerUpdate()
	{
		throw new NotImplementedException();
	}

	private enum PathType {
		Invalid = 0,
		File = 1,
		Directory = 2,
	}
}
