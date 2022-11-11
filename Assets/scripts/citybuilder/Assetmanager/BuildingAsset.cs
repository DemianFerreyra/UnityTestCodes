using Dummiesman;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BuildingAsset", menuName = "TestZone/BuildingAsset", order = 0)]
public class BuildingAsset : ScriptableObject {
    public string objPath = string.Empty;
    public GameObject loadedObject;
    public void Spawn() {
        Debug.Log("dataPath : " + Application.dataPath + objPath);
        if (File.Exists(objPath)){
            loadedObject = new OBJLoader().Load(Application.dataPath + objPath);
        }
    }
}