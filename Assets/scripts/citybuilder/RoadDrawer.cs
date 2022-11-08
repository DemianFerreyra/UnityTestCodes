using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadDrawer : MonoBehaviour
{
    public List<Vector3> pattern = new List<Vector3>();

    public CurvedRoadBuild curvedBuilder;

    public Mesh mesh;

    public List<Vector3> vertices = new List<Vector3>();

    public List<int> tris = new List<int>();

    public Transform pos1,pos2;

    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            CalculateRoad(pattern.Max(vector => vector.z));
        }
    }

    private void CalculateRoad(float RoadWidth)
    {
        pos1 =
            Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube),
            this.transform.position,
            Quaternion.identity,
            this.transform).transform;
        pos2 =
            Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube),
            this.transform.position,
            Quaternion.identity,
            this.transform).transform;

        for (int i = 0; i < curvedBuilder.leftPositions.Count; i++)
        {
            pos1.position = curvedBuilder.rightPositions[i];
            pos2.position = curvedBuilder.leftPositions[i];
            pos1.LookAt (pos2);

            //Trasladamos la pos1 1 metro, asi lo dejamos siempre centrado, y ademas, luego lo desplazamos la mitad del ancho de la calle, asi lo dejamos del lado exterior
            pos1.Translate(Vector3.forward * 1);
            pos1.Translate(Vector3.back * (RoadWidth / 2));
            foreach (var patternPoint in pattern)
            {
                pos1.Translate(Vector3.forward * patternPoint.z);
                pos1.Translate(Vector3.up * patternPoint.x);
                vertices.Add(pos1.position);
                Debug.Log(pos1.position);
                pos1.Translate(Vector3.back * patternPoint.z);
                pos1.Translate(Vector3.down * patternPoint.x);
            }
        }

        //Ahora que tenemos todos los puntos, necesitamos empezar con los triangulos
        for (int f = 0; f < vertices.Count - pattern.Count - 1; f++)
        {
            //left bottom triangle
            tris.Add(f);
            tris.Add(f + pattern.Count);
            tris.Add(f + pattern.Count + 1);

            //right top triangle
            tris.Add (f);
            tris.Add(f + pattern.Count + 1);
            tris.Add(f + 1);
        }
        DrawRoad();
    }

    private void DrawRoad()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.RecalculateNormals();
    }
}
