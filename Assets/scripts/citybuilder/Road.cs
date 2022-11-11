using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Road : MonoBehaviour
{
    public Vector3 start, end, pivot;
    public CurvedRoadBuild CurvedBuilder;
    public List<Road> subroad = new List<Road>();

    public void Generate() {
      CurvedBuilder = transform.parent.GetComponent<CurvedRoadBuild>();

      CurvedBuilder.CalcBezierCurve(start, end, pivot, this.transform);
    }
}