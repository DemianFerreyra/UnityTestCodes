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
    public List<Vector2> uv = new List<Vector2>();

    public Transform pos1,pos2;

    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            CalculateRoad();
        }
    }

    public void CalculateRoad()
    {
        float RoadWidth = pattern.Max(vector => vector.z);
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

        for (int i = 0; i < curvedBuilder.positions.Count - 1; i++)
        {
            pos1.position = curvedBuilder.positions[i];
            pos2.position = curvedBuilder.positions[i + 1];
            pos1.LookAt(pos2);
            foreach (var patternPoint in pattern)
            {
                pos1.Translate(Vector3.left * (patternPoint.z - RoadWidth / 2));
                pos1.Translate(Vector3.up * patternPoint.x);
                vertices.Add(pos1.position);
                pos1.Translate(Vector3.right * (patternPoint.z - RoadWidth / 2));
                pos1.Translate(Vector3.down * patternPoint.x);
            }
        }

        //Ahora que tenemos todos los puntos, necesitamos empezar con los triangulos
        for (int f = 0; f < vertices.Count - pattern.Count - 1; f++)
        {
            //right top triangle
            tris.Add(f);
            tris.Add(f + 1);
            tris.Add(f + pattern.Count + 1);

            //left bottom triangle
            tris.Add(f);
            tris.Add(f + pattern.Count + 1);
            tris.Add(f + pattern.Count);
        }


        //Y por ultimo los UV
        for (int i = 0; i < curvedBuilder.positions.Count - 1; i++)
        {
            uv.Add(new Vector2(0, 1));
            uv.Add(new Vector2(0, 0));
            uv.Add(new Vector2(1, 0));
            uv.Add(new Vector2(1, 1));
        }
        curvedBuilder.positions.Clear();
        DrawRoad();
    }
    private void DrawRoad()
    {
        GameObject actualRoad = new GameObject();
        mesh = new Mesh();
        actualRoad.AddComponent<MeshFilter>();
        actualRoad.AddComponent<MeshRenderer>();
        actualRoad.transform.GetComponent<MeshFilter>().mesh = mesh;
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uv.ToArray();
        mesh.RecalculateNormals();
        uv.Clear();
        vertices.Clear();
        tris.Clear();
    }
}
