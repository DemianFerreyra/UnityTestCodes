using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadDrawer : MonoBehaviour
{
    public List<Vector3> pattern = new List<Vector3>();
    public int[] roadpath;
    public int[] walkpath;

    public CurvedRoadBuild curvedBuilder;
    public List<Vector3> vertices = new List<Vector3>();
    public List<int> roadTris = new List<int>();
    public List<int> walkTris = new List<int>();
    public List<Vector2> uv = new List<Vector2>();
    public Mesh mesh;
    public Transform pos1,pos2;

    public void CalculateRoad()
    {
        float RoadWidth = pattern.Max(vector => vector.z);
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
        int iteratorHelper = 0;
        for (int f = 0; f < vertices.Count - pattern.Count - 1; f++)
        {
            if(iteratorHelper > pattern.Count - 1){
              iteratorHelper = 0;
            }
            if(roadpath.Contains(iteratorHelper)){
            //right top triangle
            roadTris.Add(f);
            roadTris.Add(f + 1);
            roadTris.Add(f + pattern.Count + 1);

            //left bottom triangle
            roadTris.Add(f);
            roadTris.Add(f + pattern.Count + 1);
            roadTris.Add(f + pattern.Count);
            }else{
            //right top triangle
            walkTris.Add(f);
            walkTris.Add(f + 1);
            walkTris.Add(f + pattern.Count + 1);

            //left bottom triangle
            walkTris.Add(f);
            walkTris.Add(f + pattern.Count + 1);
            walkTris.Add(f + pattern.Count);
            }      
            iteratorHelper++;  
        }


        for (int x = 0; x < vertices.Count / 4; x++)
        {
            uv.Add(new Vector2(1, 0));
            uv.Add(new Vector2(1, 0.3f));
            uv.Add(new Vector2(0, 0.3f) );
            uv.Add(new Vector2(0, 0));
        }
        curvedBuilder.positions.Clear();
        DrawRoad();
    }
    private void DrawRoad()
    {
        GameObject actualRoad = new GameObject();
        mesh = new Mesh();  
        actualRoad.AddComponent<MeshFilter>();
        MeshRenderer mr = actualRoad.AddComponent<MeshRenderer>();
        actualRoad.transform.GetComponent<MeshFilter>().mesh = mesh;
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.subMeshCount = 2;
        mr.materials = new Material[2];
        mesh.SetTriangles (walkTris.ToArray(), 0);
        mesh.SetTriangles (roadTris.ToArray(), 1);
        mesh.uv = uv.ToArray();
        mesh.RecalculateNormals();
        vertices.Clear();
        roadTris.Clear();
        walkTris.Clear();
        uv.Clear();
    }
}
