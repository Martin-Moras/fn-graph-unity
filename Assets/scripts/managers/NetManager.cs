using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;


public class NetManager : MonoBehaviour
{
	public string id;
	public List<ConnectionTypeList> connectionTypeLists;
	public List<NodeTypeList> nodeTypeLists;
	#region Singleton
	
	public static NetManager Instance { get; private set;}

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
	public Node NewNode(string name = ""){
		var newNode = Instantiate(VariableManager.Instance.Node).GetComponent<Node>();
		newNode.NodeConstructor(name, new List<Node>());
		return newNode;
	}
	public void NewChildNode(){
		foreach (var node in GetAllNodes()){
			//if node is in "nodeSelectionRadius"
			if (((Vector2)node.transform.position - InputManager.Instance.mousePosWorld).magnitude > VariableManager.Instance.nodeSelectionRadius) continue;

		}
	}
	public void NodeSelected(){
		foreach (var node in GetAllNodes()){
			/*//if node is in "nodeSelectionRadius"
			if (((Vector2)node.transform.position - InputManager.Instance.mousePosWorld).magnitude > VariableManager.Instance.nodeSelectionRadius) continue;
			
			//if node has an identifier called "selected" remove it else add it
			var selectedIdentifierNodes = GetIdentifierNodes(new string[]{"selected"}, false);
			Node selectedIdentifierNode;
			if (selectedIdentifierNodes.Count == 0) selectedIdentifierNode = null;
			else selectedIdentifierNode = selectedIdentifierNodes[0];

			if (selectedIdentifierNode == null){
				AddIdentifiers(node, new string[]{"selected"}, true);
				return;
			}
			var selectConnections = GetConnections(node, selectedIdentifierNode);
			Connection selectConnection;
			if (selectConnections.Count == 0) selectConnection = null;
			else selectConnection = selectConnections[0];
			if (selectConnection == null){
				AddIdentifiers(node, new string[]{"selected"}, true);
			}
			else {
				DeleteConnection(selectConnection);
			}*/
		}
	}
	private void DragNode(){

	}
	private void DropNode(){

	}
	public void ConnectNewNode(Node node, out Node newNode){
		//instantiate a new node
		var newNodeObj = Instantiate(VariableManager.Instance.Node, node.transform.position, quaternion.identity);
		newNode = newNodeObj.GetComponent<Node>();
		ConnectNodes(node, newNode);
	}
	public void ConnectNodes(Node inputNode, Node outputNode){
		inputNode.nodes.Add(outputNode);
		UpdateTypeLists();
	}
	#region Delete
	/*public void DeleteConnection(Connection connection){

		foreach (var typeList in connectionTypeLists)
		{
			typeList.connections.Remove(connection);
		}
		Destroy(connection.gameObject);
	}*/
	public void DeleteNode(Node node){
		foreach (var refNode in GetAllNodes())
		{
			refNode.nodes.Remove(node);
		}
		foreach (var typeList in nodeTypeLists)
		{
			typeList.nodes.Remove(node);
		}
		Destroy(node.gameObject);
	}
	# endregion
	public Node[] GetAllNodes(){
		return GameObject.FindObjectsOfType<Node>();
	}

	#region Save/Load
	public void Load()
    {
        var newNodes = new List<Node>();
        InstantiateNodes();
		AddNodeTypeLists();

        UpdateTypeLists();

		void InstantiateNodes()
        {
            foreach (var node in clas.nodes)
            {
                var newNodeObj = Instantiate(VariableManager.Instance.Node);
                var newNode = newNodeObj.GetComponent<Node>();
                newNode.NodeConstructor(node.path, node.nodeName, node.nodes);
                newNodes.Add(newNode);
            }
        }
		void AddNodeTypeLists(){
			foreach (var nodeTypeList in clas.nodeTypeLists)
			{
				var existingTypeList = nodeTypeLists.Find(x=>x.listPath == nodeTypeList.path);
				if (existingTypeList == null) nodeTypeLists.Add(JsonTypeListToTypeList(nodeTypeList));
				else existingTypeList = JsonTypeListToTypeList(nodeTypeList);
			}

			NodeTypeList JsonTypeListToTypeList(string nodeTypeListPath){
				
				Color color;
				color.r = nodeTypeList.color_r;
				color.g = nodeTypeList.color_g;
				color.b = nodeTypeList.color_b;
				color.a = nodeTypeList.color_a;
				
				return new NodeTypeList(
						nodeTypeList.path,
						null,
						color,
						nodeTypeList.requirements
				);
			}
		}
    }
    public void Save(){
		//nodes/
		//typeLists/
		//Create net directory
		var netDir = Directory.CreateDirectory(VariableManager.Instance.netSavePath).FullName;
		var nodesDir = Directory.CreateDirectory(Path.Combine(netDir, "nodes")).FullName;
		var typeListsDir = Directory.CreateDirectory(Path.Combine(netDir, "type-lists")).FullName;
		SaveNodes();
		SaveTypeLists();

		void SaveNodes(){
			foreach (var node in GetAllNodes())
			{
				var nodePath = Path.Combine(nodesDir, node.nodePath);
				var nodeFile = File.Create(nodePath);
				File.WriteAllText(nodePath, NodeToText(node));
				nodeFile.Close();

				string NodeToText(Node node){
					string nodeText = "";
					nodeText += $"name:\n{node.nodePath}\n";
					nodeText += $"connections:\n";
					foreach (var connectedNode in node.nodes)
					{
						nodeText += $"{connectedNode.nodePath}\n";
					}
					return nodeText;
				}
			}
		}
		void SaveTypeLists(){
			foreach (var typeList in nodeTypeLists)
			{
				var typeListPath = Path.Combine(typeListsDir, typeList.listPath);
				var typeListFile = File.Create(typeListPath);
				File.WriteAllText(typeListPath, TypeListToText(typeList));
				typeListFile.Close();

				string TypeListToText(NodeTypeList typeList){
					string nodeText = "";
					nodeText += $"name: {typeList.listPath}\n";
					nodeText += $"requirements:\n";
					foreach (var requirement in typeList.requirements)
					{
						nodeText += RequirementsToText(requirement.connectedNodeRequirements);
					}
					return nodeText;

					string RequirementsToText(TypeListRequirement[] requirements){
						var requirementText = "";
						foreach (var requirement in requirements)
						{
							requirementText += $"name:\n{requirement.path}\n";
							requirementText += RequirementsToText(requirement.connectedNodeRequirements);
						}
						return requirementText;
					}
				}
			}
		}
    }
	#endregion
	#region TypeList
	public void UpdateTypeLists(){
		TryAddToNodesTypeLists(GetAllNodes(), nodeTypeLists.ToArray());
	}
	public List<NodeTypeList> TryAddToNodesTypeLists(Node[] nodes, NodeTypeList[] typeLists = null){
		NodeTypeList[] listsToCheck;
		if (typeLists == null) listsToCheck = nodeTypeLists.ToArray();
		else listsToCheck = typeLists.ToArray();
		var outTypeLists = new List<NodeTypeList>();

		foreach (var typeList in listsToCheck)
		{
			foreach (var node in nodes)
			{
				if (!AreRequirementsMet(typeList.requirements)) continue;
				typeList.nodes.Add(node);

				bool AreRequirementsMet(TypeListRequirement[] requirements){
					foreach (var requirement in requirements)
					{
						if (!PathMatches(node.nodePath, requirement.path)) return false;
						if (!AreRequirementsMet(requirement.connectedNodeRequirements)) return false;
					}
					//if requirements are met
					return true;
				}

			}
		}
		return outTypeLists;

		bool PathMatches(string path, string filter){
			var regex = new Regex(filter, RegexOptions.None);
            return regex.IsMatch(path);
		}
	}
	#endregion
}

