using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Road : MonoBehaviour
{
    public Vector3 start, end, pivot;
    public CurvedRoadBuild CurvedBuilder;
    public List<Road> subroad = new List<Road>();
    private List<Vector3> interPoints = new List<Vector3>();

    public void Generate()
    {
        CurvedBuilder = FindObjectOfType<CurvedRoadBuild>();
        CurvedBuilder.CalcBezierCurve(start, end, pivot);
        CurvedBuilder.VerifyBezierCurve(this.transform);
    }
    // public void GenerateIntersection(){
    //   Road PRoad = transform.parent.GetComponent<Road>();
    //   CurvedBuilder = FindObjectOfType<CurvedRoadBuild>();
    //   CurvedBuilder.CalcBezierCurve(PRoad.start, PRoad.end, PRoad.pivot);
    //   //calcular donde empiezan los subroads:
    //   foreach (var road in PRoad.subroad)
    //   {
    //     interPoints.Add(road.start);
    //   }
    //   RoadDrawer rd = FindObjectOfType<RoadDrawer>();
    //   Vector3 offset = rd.OffsetFinder(start);
    //   CurvedBuilder.DrawBezierCurve(this.transform.parent, interPoints.ToArray());
    //   CurvedBuilder.CalcBezierCurve(offset, end, pivot);
    //   CurvedBuilder.DrawBezierCurve(this.transform, new Vector3[0]);
    // }
}