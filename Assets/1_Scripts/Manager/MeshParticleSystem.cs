using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshParticleSystem : MonoBehaviour
{
    public static MeshParticleSystem Instance;

    const int MAX_QUAD_AMOUNT = 15000;
    Mesh mesh;


    Vector3[] vertices;
    Vector2[] uv;
    int[] triangles;

    int quadIndex;

    private void Awake()
    {
        Instance = this;

        mesh = new Mesh();

        vertices = new Vector3[4*MAX_QUAD_AMOUNT];
        uv = new Vector2[4*MAX_QUAD_AMOUNT];
        triangles = new int[6*MAX_QUAD_AMOUNT];

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void ShootFoot(Vector3 pos)
    {
        AddQuad(pos);

    }

    private void AddQuad(Vector3 position)
    {
        UpdateQuad(quadIndex, position, 0f, new Vector3(0.5f, 0.5f));

        quadIndex++;


    }

    public void UpdateQuad(int quadIndex, Vector3 position, float rotation, Vector3 quadSize)
    {
        Debug.Log(position);
        int vIndex = 0;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        vertices[vIndex0] = position + Quaternion.Euler(0, 0, rotation - 180) * quadSize;
        vertices[vIndex1] = position + Quaternion.Euler(0, 0, rotation - 270) * quadSize;
        vertices[vIndex2] = position + Quaternion.Euler(0, 0, rotation - 0) * quadSize;
        vertices[vIndex3] = position + Quaternion.Euler(0, 0, rotation - 90) * quadSize;

        //uv
        uv[vIndex0] = new Vector2(0, 0);
        uv[vIndex1] = new Vector2(0, 0.5f);
        uv[vIndex2] = new Vector2(0.5f, 0.5f);
        uv[vIndex3] = new Vector2(0.5f, 0);

        int tIndex = quadIndex * 6;

        triangles[tIndex + 0] = vIndex0;
        triangles[tIndex + 1] = vIndex1;
        triangles[tIndex + 2] = vIndex2;

        triangles[tIndex + 3] = vIndex0;
        triangles[tIndex + 4] = vIndex2;
        triangles[tIndex + 5] = vIndex3;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
    
}
