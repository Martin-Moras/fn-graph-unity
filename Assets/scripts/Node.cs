using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    public string id { get; private set; }
    public void NodeConstructor(string id, List<string> identifiers, List<Connection> connections){
        this.id = id;
        this.identifiers = identifiers;
        this.connections = connections;

    }
    #region Identifier
    //selected
    private List<string> identifiers;
    public void AddIdentifier(string identifier)
    {
        identifiers.Add(identifier);
        manageSprite();
    }
    public void AddRangeIdentifier(string[] identifier)
    {
        identifiers.AddRange(identifier);
        manageSprite();
    }

    public void RemoveIdentifier(string identifier)
    {
        if (identifiers.Remove(identifier))
        {
            manageSprite();
        }
    }
    public List<string> GetIdentifiers(){
        return identifiers;
    }
    #endregion


    public List<Connection> connections;
    void Awake(){
        if(id == "" || id == null) 
        id = UnityEngine.Random.Range(0, 500000).ToString();
    }
    void Start()
    {
        
    }
    public void hideConnectedNodes(){
        foreach (Connection connection in connections){
            //connection.outputNode.
        }
    } 
    public void manageSprite(){
        if (identifiers.Contains("selected")){
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
