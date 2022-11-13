using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedRoadBuild : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>();
    public RoadDrawer rd;
    public float resolution = 2;

    public void CalcBezierCurve(Vector3 pos1, Vector3 pos2, Vector3 pivot)
    {
        int numPoints = Mathf.CeilToInt(Vector3.Distance(pos1, pos2) / resolution);
        for (int i = 0; i < numPoints + 1; i++)
        {
            float t = i / (float)numPoints;
            positions.Add(CalculateQuadraticPoint(t, pos1, pivot, pos2));
        }

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

    public void VerifyBezierCurve(Transform road)
    {
        RaycastHit hit;
        int pillarSpace = 0;
        List<Road> roads = new List<Road>();
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
                            pillarSpace = 5;
                        }
                        else
                        {
                            pillarSpace -= 1;
                        }
                    }
                }
                if (rayCheck(pos) != null)
                {
                    roads.Add(rayCheck(pos));
                }
            }
        }
        if (roads.Count >= 1)
        {
            //calcular la curva de bezier de cada road, luego buscar el punto mas cercano dentro de esa bezier, luego usamos 2 referencias, y las desplazamos desde ese punto, la mitad del ancho de la calle hacia ambos lados,. Seguido, rehacemos la malla de ese road
        }
        else
        {
            rd.CalculateRoad();
            rd.DrawRoad(road);
        }
    }

    private Road rayCheck(Vector3 pos)
    {
        Road roads = null;
        RaycastHit hit;
        if (Physics.Raycast(pos + new Vector3(0, 1, 0), transform.TransformDirection(Vector3.down), out hit, 7f) && hit.transform.tag == "Road")
        {
            Debug.Log("colisionamos con camino");
            roads = hit.transform.GetComponent<Road>();
        }
        else if (Physics.Raycast(pos + new Vector3(0, 1, 0), transform.TransformDirection(Vector3.down), out hit, 7f) && hit.transform.tag == "Terrain")
        {
            Debug.Log("colisionamos con terreno");
        }
        return roads;
    }
}
