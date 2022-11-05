using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public Vector3 pointA, pointB, pivot;
    public CurvedRoadBuild CurvedBuilder;

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
                    CurvedBuilder.getPoint(pointA);
                }
                else if (pointB == new Vector3(0, 0, 0))
                {
                    pointB = hit.point;
                    CurvedBuilder.getPoint(pointB);
                }
                else if (pivot == new Vector3(0, 0, 0))
                {
                    pivot = hit.point;
                    CurvedBuilder.getPoint(pivot);
                    CurvedBuilder.CalcBezierCurve(pointA, pointB, pivot);
                    pointA = new Vector3(0, 0, 0);
                    pointB = new Vector3(0, 0, 0);
                    pivot = new Vector3(0, 0, 0);
                }
            }
        }
    }
}
