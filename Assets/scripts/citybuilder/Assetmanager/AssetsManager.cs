using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsManager : MonoBehaviour
{
    public List<Vector3> RoadPatterns = new List<Vector3>();
    public List<BuildingAsset> Buildings = new List<BuildingAsset>();
    
    // Start is called before the first frame update
    void Start()
    {
        Buildings[0].Spawn();
        if(Buildings[0].loadedObject != null){
            Debug.Log("Objeto cargado y listo para usarse");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
