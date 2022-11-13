using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public Vector3 pointA, pointB, pivot;
    public Road actualRoad;
    public List<Road> roads = new List<Road>();
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
                }
                else if (pointB == new Vector3(0, 0, 0))
                {
                    pointB = hit.point;
                }
                else if (pivot == new Vector3(0, 0, 0))
                {
                    pivot = hit.point;
                    actualRoad = new GameObject("new Road").AddComponent<Road>();
                    actualRoad.start = pointA;
                    actualRoad.end = pointB;
                    actualRoad.pivot = pivot;
                    actualRoad.Generate();
                    pointA = new Vector3(0, 0, 0);
                    pointB = new Vector3(0, 0, 0);
                    pivot = new Vector3(0, 0, 0);
                }
            }
        }
    }
}
