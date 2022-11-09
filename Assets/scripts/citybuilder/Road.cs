using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Road : MonoBehaviour
{
    public Vector3 start, end, pivot;
    public List<Road> subroad = new List<Road>();
}
