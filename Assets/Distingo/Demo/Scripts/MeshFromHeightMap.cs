using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class MeshFromHeightMap : MonoBehaviour 
{
    public Texture2D HeightMap;

    Mesh thisMesh;

    [Range(0,200)]
    public float MaxHeight = 30;

    Vector3[] verts;
    int[] tris;
    Vector2[] uvs;

    public int VertCount = 0;

    public bool RebuildTerrain = false;
    

    // Use this for initialization
    void Start () 
    {
	    // Build Terrain from
        BuildTerrain();
	}

    void BuildTerrain()
    {
        if (HeightMap == null)
            return; 

        thisMesh = new Mesh();

        int width = HeightMap.width;
        int height = HeightMap.height;

        verts = new Vector3[width * height];
        uvs = new Vector2[width * height];
        tris = new int[(width - 1) * (height - 1) * 6];

        VertCount = verts.Length;

        Vector3 posOffset = new Vector3(transform.position.x - (width*.5f), 0, transform.position.z - (height*.5f));

        Color[] map = HeightMap.GetPixels();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                verts[x + y * width] = posOffset + new Vector3(y, Mathf.Lerp(0, MaxHeight, map[x + y * width].r), x);
                uvs[x + y * width] = new Vector2((float)x / width, (float)y / width);
            }
        }

        
        for (int x = 0; x < width - 1; x++)
        {
            for (int y = 0; y < height - 1; y++)
            {
                tris[(x + y * (width - 1)) * 6 + 5] = ((x + 1) + (y + 1) * width);
                tris[(x + y * (width - 1)) * 6 + 4] = ((x + 1) + y * width);
                tris[(x + y * (width - 1)) * 6 + 3] = (x + y * width);

                tris[(x + y * (width - 1)) * 6 + 2] = ((x + 1) + (y + 1) * width);
                tris[(x + y * (width - 1)) * 6 + 1] = (x + y * width);
                tris[(x + y * (width - 1)) * 6] = (x + (y + 1) * width);
            }
        }

        thisMesh.vertices = verts;
        thisMesh.uv = uvs;
        thisMesh.triangles = tris;

        thisMesh.RecalculateBounds();
        thisMesh.RecalculateNormals();

        GetComponent<MeshFilter>().sharedMesh = thisMesh;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (RebuildTerrain)
        {
            RebuildTerrain = false;
            BuildTerrain();
        }
	}
}
