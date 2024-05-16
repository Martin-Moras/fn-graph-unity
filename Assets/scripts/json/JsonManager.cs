using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonManager{
    public static T LoadClassFromJson<T>(string path) where T : class {
        string jsonFileText = File.ReadAllText(path+".json");
        try{
			var obj = JsonUtility.FromJson<T>(jsonFileText);
            return obj;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error Deserializing JSON file: {e.Message}");
        }
        return null;
    }
    public static bool SaveClassAsJson<T>(T saveClass, string path)where T : class {
        CreateDirectory();
        string filePath = path + ".json";
        FileStream file;
        if (!File.Exists(filePath)) 
            file = File.Create(filePath);
        else{
			Debug.LogError($"the file: {filePath} alredy exists");
			return false;
		}
        file.Close();
        try{
            var json = JsonUtility.ToJson(saveClass, true);
            File.WriteAllText(filePath, json);
        }
        catch(Exception e){
            Debug.LogError($"Error writing JSON file: {e.Message}");
			return false;
        }
        return true;
		void CreateDirectory(){
			if (!Directory.Exists(path))
            	Directory.CreateDirectory(path);
		}
    }

}