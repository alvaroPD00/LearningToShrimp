using UnityEngine;

[ExecuteInEditMode]
public class Subdivisions : MonoBehaviour
{
    [Range(1, 256)]
    public int resolution = 10;

    [ContextMenu("Subdivide Plane")]
    void GenerateSubdividedPlane()
    {
        GameObject original = this.gameObject;

        // Crear nuevo GameObject hijo
        GameObject subdivided = new GameObject("Subdivided Plane");
        subdivided.transform.parent = original.transform;
        subdivided.transform.localPosition = Vector3.zero;
        subdivided.transform.localRotation = Quaternion.identity;
        subdivided.transform.localScale = Vector3.one;

        // Agregar componentes necesarios
        MeshFilter mf = subdivided.AddComponent<MeshFilter>();
        MeshRenderer mr = subdivided.AddComponent<MeshRenderer>();

        // Copiar material si existe
        MeshRenderer originalRenderer = original.GetComponent<MeshRenderer>();
        if (originalRenderer != null)
        {
            mr.sharedMaterial = originalRenderer.sharedMaterial;
        }

        Mesh mesh = new Mesh();
        mesh.name = "Generated Subdivided Plane";

        int vertCount = (resolution + 1) * (resolution + 1);
        Vector3[] vertices = new Vector3[vertCount];
        Vector2[] uv = new Vector2[vertCount];
        int[] triangles = new int[resolution * resolution * 6];

        float step = 1.0f / resolution;

        // Generar vértices y UVs
        int i = 0;
        for (int z = 0; z <= resolution; z++)
        {
            for (int x = 0; x <= resolution; x++)
            {
                // Centrado en (0,0)
                vertices[i] = new Vector3(x * step - 0.5f, 0, z * step - 0.5f);
                uv[i] = new Vector2(x * step, z * step);
                i++;
            }
        }

        // Generar triángulos
        int ti = 0;
        for (int z = 0; z < resolution; z++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int start = x + z * (resolution + 1);
                triangles[ti++] = start;
                triangles[ti++] = start + resolution + 1;
                triangles[ti++] = start + 1;

                triangles[ti++] = start + 1;
                triangles[ti++] = start + resolution + 1;
                triangles[ti++] = start + resolution + 2;
            }
        }

        // Aplicar a la malla
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        mf.mesh = mesh;
    }
}
