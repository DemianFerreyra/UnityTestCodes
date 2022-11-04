using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadDrawer : MonoBehaviour
{
    public GameObject test;
    public Transform pos1, pos2, pivot, parent, pillarsParent;
    private int numPoints;
    private Vector3[] positions = new Vector3[100];
    private Vector3[] Last = new Vector3[2];

    // Start is called before the first frame update
    void Start(){
        numPoints = Mathf.CeilToInt(Vector3.Distance(pos1.position, pos2.position) / 2);
        Last[0] = pos1.position;
        Last[1] = pos2.position;
        CalcBezierCurve();
    }

    // Update is called once per frame
    void Update(){
        PositionChange(Last[0], pos1, 0);
        PositionChange(Last[1], pos2, 1);
    }

    private void PositionChange(Vector3 Lastpos, Transform Pos, int num){
         if(Lastpos != Pos.position){
            Clean();
            numPoints = Mathf.CeilToInt(Vector3.Distance(pos1.position, pos2.position) / 2);
            CalcBezierCurve();
            Last[num] = Pos.position;
        }
    }
    private void CalcBezierCurve(){
        for (int i = 0; i < numPoints + 1; i++){
            float t = i / (float) numPoints;
            Debug.Log(t);
            positions[i] = CalculateQuadraticPoint(t, pos1.position, pivot.position, pos2.position);
        }
        DrawBezierCurve();
    }

    private void DrawBezierCurve(){
        RaycastHit hit;
        int pillarSpace = 0;
        foreach (var pos in positions){
            if (pos != new Vector3(0, 0, 0)){
                if (Physics.Raycast(pos, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity)){
                    if (Vector3.Distance(hit.point, pos) >= 6){
                        if (pillarSpace == 0){
                            Instantiate(test, hit.point, Quaternion.identity, pillarsParent);
                            pillarSpace = 5;
                        }
                        else{
                            pillarSpace -= 1;
                        }
                    }
                }
                Instantiate(test, pos, Quaternion.identity, parent);
            }
        }
    }

    private Vector3 CalculateQuadraticPoint(float t, Vector3 p1, Vector3 p2, Vector3 p3){
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 pointPos = uu * p1;
        pointPos += 2 * u * t * p2;
        pointPos += tt * p3;
        return pointPos;
    }
    private void Clean(){
      foreach (Transform child in parent) {
        GameObject.Destroy(child.gameObject);
      }
      foreach (Transform child in pillarsParent) {
        GameObject.Destroy(child.gameObject);
      }
    }
}
