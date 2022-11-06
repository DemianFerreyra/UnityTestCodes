using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoadDrawer : MonoBehaviour
{
    public List<Vector3> pattern = new List<Vector3>();
    public CurvedRoadBuild curvedBuilder;
    public MeshFilter mf;
    public List<Vector3> vertices = new List<Vector3>();
    public Transform pos1, pos2;
    public GameObject test;

    // Update is called once per frame
    void Update(){
      if(Input.GetKeyDown("b")){
        DrawRoad(pattern.Max(vector => vector.z));
      }
    }
    private void DrawRoad(float RoadWidth){
      pos1 = Instantiate(test, this.transform.position, Quaternion.identity, this.transform).transform;
      pos2 = Instantiate(test, this.transform.position, Quaternion.identity, this.transform).transform;
      for (int i = 0; i < curvedBuilder.leftPositions.Count; i++)
      {
        pos1.position = curvedBuilder.rightPositions[i];
        pos2.position = curvedBuilder.leftPositions[i];
        pos1.LookAt(pos2);
        
        //Trasladamos la pos1 1 metro, asi lo dejamos siempre centrado, y ademas, lo desplazamos la mitad del ancho de la calle, asi lo dejamos del lado exterior
        pos1.Translate(Vector3.forward * 1);
        Debug.Log(RoadWidth);
        pos1.Translate(Vector3.back * (RoadWidth / 2));
        //luego:
        foreach (var patternPoint in pattern)
        {
            pos1.Translate(Vector3.forward * patternPoint.z);
            pos1.Translate(Vector3.up * patternPoint.x);
            Instantiate(test, pos1.position, pos1.rotation, this.transform);
            pos1.Translate(Vector3.back * patternPoint.z);
            pos1.Translate(Vector3.down * patternPoint.x);
        }
      }
    }
}
