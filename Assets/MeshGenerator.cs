using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: we also need batching.. 
// X and Y is flat to floor in this mesh, so we might need to flip it later,  in unity Y is up and  down. 


public class MeshGenerator : MonoBehaviour {

    
    // Source: http://paulbourke.net/geometry/polygonise/ 
    public VoxelMap voxelMap;
    public float meshSize = 10f;

    public GameObject boxPrefab; // boxes before we start traingulating.
    private float squareSize;
    private float  halfSize; // of the square

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
        // TriangulateMesh();
        CreateTempBoxes();


    }

    private void TriangulateMesh() {

    }

    private void CreateTempBoxes() {
        squareSize = meshSize / voxelMap.resolution;
        halfSize = meshSize * 0.5f;
        for (int y = 0; y < voxelMap.resolution; y++) {
            for (int x = 0; x < voxelMap.resolution; x++) {
                CreateTempVerticy(x, y);
            }
        }
    }

    private void CreateTempVerticy(int x, int y) {
        GameObject o = Instantiate(boxPrefab) as GameObject;
        o.transform.parent = transform;
        o.name = $"Voxel X:{x},Y:{y}.";

        bool centerMesh = true;
        Vector3 localPos;

        if (centerMesh) {
            // Probably a good idea to add these kinds of things in the end? Or have the batches centered, not verticies
            localPos = new Vector3((x + 0.5f) * squareSize - halfSize, (y + 0.5f) * squareSize - halfSize);
        } else {
            localPos = new Vector3((x + 0.5f) * squareSize, (y + 0.5f) * squareSize);
        }
        o.transform.localPosition = localPos;

        o.transform.localScale = Vector3.one * squareSize;

        
        if(voxelMap.getVoxelValue(x, y)) {
            o.GetComponent<MeshRenderer>().material.color = Color.black;
        } else {
            o.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        
    }

    private void TempColorVerticies() {

    }





}
