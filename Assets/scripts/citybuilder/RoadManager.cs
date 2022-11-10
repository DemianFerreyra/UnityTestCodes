using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public Vector3 pointA, pointB, pivot;
    public CurvedRoadBuild CurvedBuilder;
    public Road actualRoad;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 600f))
            {
                if (pointA == new Vector3(0, 0, 0))
                {
                    pointA = hit.point;
                    ValidateNear(hit.point, 1);
                }
                else if (pointB == new Vector3(0, 0, 0))
                {
                    pointB = hit.point;
                    ValidateNear(hit.point, 2);
                }
                else if (pivot == new Vector3(0, 0, 0))
                {
                    pivot = hit.point;
                    ValidateNear(hit.point, 3);
                    CurvedBuilder.CalcBezierCurve(pointA, pointB, pivot);
                    pointA = new Vector3(0, 0, 0);
                    pointB = new Vector3(0, 0, 0);
                    pivot = new Vector3(0, 0, 0);
                }
            }
        }
    }
    private void ValidateNear(Vector3 pos, int iteration){
        if(iteration == 1){
            actualRoad = new GameObject("new Road").AddComponent<Road>();
            actualRoad.start = pos;
        }else if(iteration == 2){
            actualRoad.end = pos;
        }else{
            actualRoad.pivot = pos;
        }  
        RaycastHit hit;
        if (Physics.Raycast(pos + new Vector3(0,1,0), transform.TransformDirection(Vector3.down), out hit, 7f) && hit.transform.tag == "Road"){      
            Debug.Log("colisionamos con camino");
            hit.transform.GetComponent<Road>().subroad.Add(actualRoad);
        }else{
            Debug.Log("no colisionamos con camino");
        }
    }
}
