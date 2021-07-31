using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: we also need batching.. 

public class MeshGenerator : MonoBehaviour {
    public VoxelMap voxelMap;
    public float meshSize = 10f;

    public GameObject boxPrefab; // boxes before we start traingulating.
    private float squareSize;


    private Mesh mesh;

    private List<Vector3> vertices;
    private List<int> triangles;

    private void Awake() {
        /*GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "VoxelGrid Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();*/





    }


    public void createMesh() {
        Debug.Log("Create mesh");

        squareSize = meshSize / voxelMap.resolution;
        for (int y = 0; y < voxelMap.resolution; y++) {
            for (int x = 0; x < voxelMap.resolution; x++) {
                CreateTempVerticy(x, y);
            }
        }
    }

    private void CreateTempVerticy(int x, int y) {
        GameObject o = Instantiate(boxPrefab) as GameObject;
        o.transform.parent = transform;
        o.transform.localPosition = new Vector3((x + 0.5f) * squareSize, (y + 0.5f) * squareSize);
        o.transform.localScale = Vector3.one * squareSize;
    }



}
