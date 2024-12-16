using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using System.Text;

public class BackupManager : I_Manager
{
	string netDir;

	#region Singleton
	public static BackupManager inst { get; private set;}
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
	private void Start() {
		InitializeDirectories();
		
	}
	private void InitializeDirectories(){
		netDir = VariableManager.inst.rootPath + VariableManager.inst.netSavePath;
		//Create net directory
		CreateDirIfDoesntExist(netDir);
	}
	#region Save
	public void SaveNodes(DataNode saverNode){
		string nodePath;
		if (saverNode.nodePath == null || saverNode.nodePath == "") 
			nodePath = Path.Combine(netDir, "unnamed");
		else 
			nodePath = Path.Combine(netDir, saverNode.nodePath);
		var nodeFile = File.Create(nodePath);
		nodeFile.Close();
		var saveFileBuilder = new StringBuilder();
		foreach (var node in saverNode.connectedNodes)
		{
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
		File.WriteAllText(nodePath, saveFileBuilder.ToString());
		nodeFile.Close();
	}
	#endregion Save
	#region Load
	public void Load()
    {
        var newNodes = DeserializeNodes();

		ConnectNewNodes(newNodes);
		
		List<DataNode> DeserializeNodes(){
			var newNodes = new List<DataNode>();
			var nodeFiles = GetFiles(netDir, VariableManager.inst.netSaveFileExtention);

			foreach (var nodeFile in nodeFiles)	{
				var lines = nodeFile.Split().ToList();
				foreach (var line in lines){
					newNodes.Add(DeserializeNode(line));
				}
			}
			return newNodes;

			DataNode DeserializeNode(string saveFileLine)
			{
				uint nodeId;
				string nodePath;
				var connectedNodeIds = new List<uint>();

				if (saveFileLine == null)
					return null;
				var nodeParameters = saveFileLine.Split(new char[1]{';'});
				if (!uint.TryParse(nodeParameters[0], out nodeId)) {
					Debug.LogWarning($"the line :{saveFileLine} has an invalid Id. It must be parsable to an uint");
					return null;
				}
				nodePath = nodeParameters[1];
				//Create a list with connected node ids
				for(int i = 2; i < nodeParameters.Length; i++) {
					if (!uint.TryParse(nodeParameters[i - 2], out uint connectedNodeId)) {
						Debug.LogWarning($"in the line :{saveFileLine} is the {i - 2} connected node invalide. It must be parsable to an uint");
						continue;
					}
					connectedNodeIds.Add(connectedNodeId);
				}
				DataNode newNode = NetContentManager.inst.NewNode(nodeId, connectedNodeIds, nodePath);
				return newNode;
			}
		}
		void ConnectNewNodes(List<DataNode> newNodes)
		{
			var allNodes = NetContentManager.inst.GetAllNodes().ToList();

			foreach (var node in newNodes) {
				foreach (var connectedNodeId in node.connectedNodeIds) {
					var connectedNode = allNodes.Find(x=>x.nodeId == connectedNodeId);
					//continue if connected node doesn't exist
					if (connectedNode == null) 
						continue;
					//Connect Node
					NetContentManager.inst.ConnectNodes(node, connectedNode);
				}
			}
		}
		List<string> GetFiles(string path, string extention){
			var outFiles = new List<string>();
			foreach (var file in Directory.GetFiles(path, $"*.{extention}")){
				outFiles.Add(File.ReadAllText(file));
			}
			foreach (var dir in Directory.GetDirectories(path)){
				//add all files that are contained in "dir"
				outFiles.AddRange(GetFiles(dir, extention));
			}
			return outFiles;
		}
    }
	#endregion
	private void CreateDirIfDoesntExist(string dirPath) 
	{
		if (!Directory.Exists(Path.GetDirectoryName(dirPath))) 
			Directory.CreateDirectory(Path.GetDirectoryName(dirPath));
	}
}
