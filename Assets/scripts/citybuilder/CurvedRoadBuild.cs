using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedRoadBuild : MonoBehaviour
{
    public Transform parent;
    public GameObject test;
    public Vector3[] positions = new Vector3[100];

    public void getPoint(Vector3 pos)
    {
        Instantiate(test, pos, Quaternion.identity, parent);
    }

    public void CalcBezierCurve(Vector3 pos1, Vector3 pos2, Vector3 pivot)
    {
        int numPoints = Mathf.CeilToInt(Vector3.Distance(pos1, pos2) / 2);
        for (int i = 0; i < numPoints + 1; i++)
        {
            float t = i / (float)numPoints;
            positions[i] = CalculateQuadraticPoint(t, pos1, pivot, pos2);
        }
        DrawBezierCurve();
    }

    private Vector3 CalculateQuadraticPoint(float t, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 pointPos = uu * p1;
        pointPos += 2 * u * t * p2;
        pointPos += tt * p3;
        return pointPos;
    }

    private void DrawBezierCurve()
    {
        RaycastHit hit;
        int pillarSpace = 0;
        foreach (var pos in positions)
        {
            if (pos != new Vector3(0, 0, 0))
            {
                if (Physics.Raycast(pos, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
                {
                    if (Vector3.Distance(hit.point, pos) >= 6)
                    {
                        if (pillarSpace == 0)
                        {
                            Instantiate(test, hit.point, Quaternion.identity, this.transform);
                            pillarSpace = 5;
                        }
                        else
                        {
                            pillarSpace -= 1;
                        }
                    }
                }
                Instantiate(test, pos, Quaternion.identity, parent);
                rayCheck(pos);
            }
        }
    }

    private void rayCheck(Vector3 goTo)
    {
        RaycastHit hit;
        if (Physics.Raycast(goTo, transform.TransformDirection(Vector3.down), out hit, 600f) && hit.transform.tag == "Road")
        {
            Debug.Log("Pisamos camino");
        }
        if (Physics.SphereCast(goTo, 10f, transform.forward, out hit, 10) && hit.transform.tag == "Road")
        {
            Debug.Log("Pisamos camino");
        }
    }
}