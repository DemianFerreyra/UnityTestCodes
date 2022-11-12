using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class AssetsManager : MonoBehaviour
{
    public List<Vector3> RoadPatterns = new List<Vector3>();

    void Start()
    {
    DirectoryInfo d = new DirectoryInfo(Application.streamingAssetsPath);
    FileInfo[] Files = d.GetFiles("*");
    foreach(FileInfo file in Files)
    {
        if(file.Extension == ""){
         var bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, file.Name));
         var asset = bundle.LoadAsset(file.Name);
         var instance = Instantiate(asset);
        }    
    }
    }
}
