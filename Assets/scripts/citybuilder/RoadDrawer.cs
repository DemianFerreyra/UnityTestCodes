using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        DrawRoad();
      }
    }
    private void DrawRoad(){
      pos1 = Instantiate(test, this.transform.position, Quaternion.identity, this.transform).transform;
      pos2 = Instantiate(test, this.transform.position, Quaternion.identity, this.transform).transform;
      for (int i = 0; i < curvedBuilder.leftPositions.Count; i++)
      {
        pos1.position = curvedBuilder.rightPositions[i];
        pos2.position = curvedBuilder.leftPositions[i];
        pos1.LookAt(pos2);
        
        Instantiate(test, pos1.position, pos1.rotation, this.transform);
        Transform p2 = Instantiate(test, pos1.position, pos1.rotation, this.transform).transform;
        p2.Translate(Vector3.forward * 3);
        Transform p3 = Instantiate(test, pos1.position, pos1.rotation, this.transform).transform;
        p3.Translate(Vector3.forward * 5);
        // p2.Translate(3,0,0, Space.World);
        // float angle = Vector3.Angle(curvedBuilder.leftPositions[i], curvedBuilder.rightPositions[i]);
        // for (int x = 0; x < pattern.Count; x++)
        // {
        //     //por cada punto del patron, vamos a: 
        //     //2) rotarlo segun "angle"
        //     // vertices.Add(Quaternion.AngleAxis(angle, Vector3.up) * curvedBuilder.leftPositions[i] + nPoint);
        //     if(x == 0){
        //       Instantiate(test,curvedBuilder.rightPositions[i], Quaternion.identity, this.transform);
        //     }else{
        //       Debug.Log("desplazamiento en Z de:" + pattern[x].z * Mathf.Cos(angle));
        //       Debug.Log("desplazamiento en X de:" + pattern[x].z * Mathf.Sin(angle));
        //       Instantiate(test,curvedBuilder.rightPositions[i], Quaternion.identity, this.transform);
        //     }
        // }
      }

    }
}
