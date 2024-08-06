using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class BackupManager : MonoBehaviour
{
	string netDir;
	string nodesDir;
	string typeListsDir;

	#region Singleton
	public static BackupManager inst { get; private set;}
	void SingletonizeThis()
	{
		if (inst != null && inst != this) Destroy(this);
		else inst = this;
	}
	#endregion
	void Awake()
	{
		SingletonizeThis();
	}
	private void Start() {
		InitializeDirectories();
		
	}
	private void InitializeDirectories(){
		netDir = VariableManager.inst.netSavePath;
		nodesDir = Path.Combine(netDir, "nodes");
		typeListsDir = Path.Combine(netDir, "type-lists");
		//Create net directory
		if (!Directory.Exists(netDir)) 
			Directory.CreateDirectory(netDir);
		//Create nodes directory
		if (!Directory.Exists(nodesDir)) 
			Directory.CreateDirectory(nodesDir);
		//Create type-lists directory
		if (!Directory.Exists(typeListsDir)) 
			Directory.CreateDirectory(typeListsDir);
	}
	#region Save/Load
	public void SaveNodes(List<Node> nodesToSave){
		foreach (var node in nodesToSave)
		{
			string nodePath;
			if (node.nodePath == null || node.nodePath == "") nodePath = Path.Combine(nodesDir, "unnamed.node");
			else nodePath = Path.Combine(nodesDir, node.nodePath);
			
			if (!Directory.Exists(Path.GetDirectoryName(nodePath))) 
			Directory.CreateDirectory(Path.GetDirectoryName(nodePath));
			//Create node file
			var nodeFile = File.Create(nodePath);
			nodeFile.Close();
			File.WriteAllText(nodePath, NodeToText(node));
			nodeFile.Close();

			string NodeToText(Node node){
				string nodeText = "";
				nodeText += $"node-name:\n{node.nodePath}\n";
				nodeText += $"connections:";
				foreach (var connectedNode in node.connectedNodes)
				{
					nodeText += $"\n{connectedNode.nodePath}";
				}
				return nodeText;
			}
		}
	}
	public void SaveTypeLists(List<NodeTypeList> nodeTypeListsToSave){

		foreach (var typeList in nodeTypeListsToSave)
		{
			string listPath;
			if (typeList.listPath == null || typeList.listPath == "") 
				listPath = Path.Combine(typeListsDir, "unnamed.tlist");
			else listPath = Path.Combine(typeListsDir, typeList.listPath);

			if (!Directory.Exists(Path.GetDirectoryName(listPath)))
			Directory.CreateDirectory(Path.GetDirectoryName(listPath));
			//Create file
			var listFile = File.Create(listPath);
			listFile.Close();
			File.WriteAllText(listPath, TypeListToText(typeList));
			listFile.Close();

			string TypeListToText(NodeTypeList typeList){
				string listText = "";
				listText += $"list-name:\n{typeList.listPath}\n";
				listText += $"requirements:\n";
				listText += RequirementsToText(typeList.requirements);
				return listText;

				string RequirementsToText(TypeListRequirement[] requirements){
					var requirementText = "";
					if (requirements == null) return requirementText;
					foreach (var requirement in requirements)
					{
						requirementText += $"node-name:\n{requirement.path}\n";
						var req = RequirementsToText(requirement.connectedNodeRequirements);
						string newLine;
						if (req == "") newLine = "";
						else newLine = "\n";
						requirementText += $"[\n{req}\n]{newLine}";
					}
					return requirementText;
				}
			}
		}
	}
	public void Load()
    {
        var newNodes = DeserializeNodes();
		ConnectNewNodes(newNodes);
		AddNodeTypeLists();
        NetContentManager.inst.UpdateTypeLists();

		
		List<(Node, List<string>)> DeserializeNodes(){
			var newNodes = new List<(Node, List<string>)>();
			var nodeFiles = GetNodeFiles(nodesDir, "node");
			foreach (var nodeFile in nodeFiles)	
			{
				var lines = nodeFile.Split().ToList();
				var nameIndex = lines.IndexOf("node-name:") + 1;
				string path = lines[nameIndex];
				var newNode = (NetContentManager.inst.NewNode(path), new List<string>());
				newNodes.Add(newNode);
				
				var nodesStartingIndex = lines.IndexOf("connections:") + 1;
				for (int i = nodesStartingIndex; i < lines.Count; i++)
				{
					newNode.Item2.Add(lines[i]);
				}
			}
			return newNodes;


		}
		void ConnectNewNodes(List<(Node, List<string>)> newNodes){
			var allNodes = NetContentManager.inst.GetAllNodes().ToList();

			foreach (var node in newNodes) {
				foreach (var connectedNodePath in node.Item2)
				{
					var connectedNode = allNodes.Find(x=>x.nodePath == connectedNodePath);
					//Create Node if doesn't exist
					if (connectedNode == null){
						connectedNode = NetContentManager.inst.NewNode(connectedNodePath);
						allNodes.Add(connectedNode);
					}
					//Connect Node
					NetContentManager.inst.ConnectNodes(node.Item1, connectedNode);
				}
			}
		}
		
		void AddNodeTypeLists(){
			var newTypeLists = new List<NodeTypeList>();
			var listFiles = GetNodeFiles(typeListsDir, "tlist");
			

			foreach (var listFile in listFiles)
			{
				var lines = listFile.Split().ToList();
				var pathIndex = lines.IndexOf("list-name:") + 1;
				var newList = new NodeTypeList(lines[pathIndex], null, Color.clear, DeserializeRequirements());
				NetContentManager.inst.nodeTypeLists.Add(newList);

				TypeListRequirement[] DeserializeRequirements(){
					var currentIndex = lines.IndexOf("requirements:") + 1;
					var outsd = NewConnectedNodeRequirement();
					return outsd;

					TypeListRequirement[] NewConnectedNodeRequirement(){
						var outSubRequirements = new List<TypeListRequirement>();
						while (lines[currentIndex] == "node-name:") {
							currentIndex++;
							outSubRequirements.Add(NewRequirement());
							if (lines.Count - 1 >= currentIndex) return outSubRequirements.ToArray();
						}
						if (lines[currentIndex] == "]") currentIndex++;
						else currentIndex += 2;
						if (outSubRequirements.Count == 0 || outSubRequirements[0] == null) return null;
						return outSubRequirements.ToArray();
					}

					TypeListRequirement NewRequirement(){
						var nodePath = lines[currentIndex];
						currentIndex += 2;
						return new TypeListRequirement(nodePath, NewConnectedNodeRequirement());
					}
				}
			}
		}
		List<string> GetNodeFiles(string path, string extention){
			var outFiles = new List<string>();
			foreach (var file in Directory.GetFiles(path, $"*.{extention}")){
				outFiles.Add(File.ReadAllText(file));
			}
			foreach (var dir in Directory.GetDirectories(path)){
				//add all files that are contained in "dir"
				outFiles.AddRange(GetNodeFiles(dir, extention));
			}
			return outFiles;
		}
    }
	#endregion
}
