using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadDrawer : MonoBehaviour
{
    public GameObject test;
    public Transform pos1, pos2, pivot;
    private int numPoints;
    private Vector3[] positions = new Vector3[100];

    // Start is called before the first frame update
    void Start(){
        numPoints = Mathf.CeilToInt(Vector3.Distance(pos1.position, pos2.position) / 2);
        CalcBezierCurve();
    }

    // Update is called once per frame
    void Update(){
    }

    private void CalcBezierCurve(){
        for (int i = 0; i < numPoints + 1; i++){
            float t = i / (float) numPoints;
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
                            Instantiate(test, hit.point, Quaternion.identity);
                            pillarSpace = 3;
                        }
                        else{
                            pillarSpace -= 1;
                        }
                    }
                }
                Instantiate(test, pos, Quaternion.identity, this.transform);
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
}
