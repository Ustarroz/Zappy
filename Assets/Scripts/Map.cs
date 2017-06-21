using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Map : MonoBehaviour {

    public static readonly Vector3 North = new Vector3(0, 0, 1);
    public static readonly Vector3 East = new Vector3(1, 0, 0);
    public static readonly Vector3 South = new Vector3(0, 0, -1);
    public static readonly Vector3 West = new Vector3(-1, 0, 0);
    public static readonly Vector3 Up = new Vector3(0, 1, 0);
    public static readonly Vector3 Down = new Vector3(0, -1, 0);

    public Transform root;
    public GameObject prefab;
    public Vector2 dimension;
    public ZappyCell[,] cells;

    Mesh mesh;

    private void Awake()
    {
        CreateMap();
    }

    void CreateMap () {
        cells = new ZappyCell[(int)dimension.x, (int)dimension.y];

        mesh = prefab.GetComponent<MeshFilter>().sharedMesh;
        for (int x = 0; x < dimension.x ; x++)
        {
            for (int y = 0; y < dimension.y; y++)
            {
               GameObject go = Instantiate(prefab, new Vector3(mesh.bounds.size.x * x, 0, mesh.bounds.size.z * y), Quaternion.identity, transform);
                cells[x , y] = go.GetComponent<ZappyCell>();
                cells[x, y].gridPos = new Vector2(x, y);
            }
        }	
	}
	
    public Vector3 WorldToGrid(Vector3 worldPoint)
    {
        print("worldToGRid : " + worldPoint.x / mesh.bounds.size.x + " - " + Math.Ceiling(worldPoint.z / mesh.bounds.size.z));
        return (new Vector3((int)Math.Ceiling(worldPoint.x / mesh.bounds.size.x), 0, (int)Math.Ceiling(worldPoint.z / mesh.bounds.size.z)));
    }

    public Vector3 GridToWorld(Vector2 gridPoint)
    {
        return (new Vector3(gridPoint.x * mesh.bounds.size.x, 0, gridPoint.y * mesh.bounds.size.z));
    }

    public bool Contains(Vector3 vec)
    {
        return (vec.x >= 0 && vec.y >= 0 && vec.x < cells.GetLength(0) && vec.z < cells.GetLength(1));
    }
}
