using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NoiseTest;

// THE VOXEL MAP IS A REPRESENTATION OF A BIGGER WORLD. 
// "voxel represents a value on a regular grid in three-dimensional space" - wikipedia
public class VoxelMap : MonoBehaviour {
    VoxelMap() {
        openSimplex = new OpenSimplexNoise();
    }
    public OpenSimplexNoise openSimplex;
    public int resolution = 256;
    public float perlinOffsetX = 40;
    public float perlinOffsetY = 40;
    public float perlinThickness = 0.5f;
    public float perlinScale = 20f;

    public int size;
    private bool[,] voxels; // on and off values. 
    private float[,,] voxels3D;

    public MeshGenerator meshGenerator;

    public GameObject cubePrefab;

    public bool getVoxelValue(int x, int y) {
        return voxels[x, y];
    }

    public void refreshPerlinNoiseMap() {
        AddPerlinNoise();
        DrawMap();
    }

    private void Awake() {
        
        //AddPerlinNoise();
        //DrawMap();
    }

    public void AddSimplexNoise() {
        

        voxels3D = new float[resolution, resolution, resolution];
        bool defaultState = false;
        /*for (int y = 0; y < resolution; y++) {
            for (int x = 0; x < resolution; x++) {
                for(int z = 0; z < resolution; z++) {
                    voxels3D[x, y, z] = 0f;
                }
            }
        }*/

        for (int y = 0; y < resolution; y++) {
            for (int x = 0; x < resolution; x++) {
                for(int z = 0; z < resolution; z++) {
                    float xCoord = (float)x / resolution * perlinScale + perlinOffsetX; // must be between 0-1
                    float yCoord = (float)y / resolution * perlinScale + perlinOffsetX;
                    float zCoord = (float)z / resolution * perlinScale + perlinOffsetX;
                    double d = openSimplex.Evaluate(xCoord, yCoord, zCoord);
                    float v = (float)d;

                    voxels3D[x, y, z] = v;
                    
                }

                

            }
        }
    }

    private void AddPerlinNoise() {
        voxels = new bool[resolution, resolution];
        bool defaultState = false;
        for (int y = 0; y < resolution; y++) {
            for (int x = 0; x < resolution; x++) {
                voxels[x, y] = defaultState;
            }
        }

        for (int y = 0; y < resolution; y++) {
            for (int x = 0; x < resolution; x++) {
                float xCoord = (float)x / resolution * perlinScale + perlinOffsetX; // must be between 0-1
                float yCoord = (float)y / resolution * perlinScale + perlinOffsetY;

                double d = openSimplex.Evaluate(xCoord, yCoord);
                float v2 = (float)d;

                //float v = Mathf.PerlinNoise(xCoord, yCoord);
                if(v2 < perlinThickness) {
                    voxels[x, y] = true;
                } else {
                    voxels[x, y] = false;
                }
                
            }
        }
       
    }

    public void DrawCubes() {
        for (int y = 0; y < resolution; y++) {
            for (int x = 0; x < resolution; x++) {
                for (int z = 0; z < resolution; z++) {


                    // Check if true. 
                    float squareSize = 0.5f;
                    bool draw = voxels3D[x, y, z] > perlinThickness;

                    if (draw) {
                        GameObject o = Instantiate(cubePrefab) as GameObject;
                        o.transform.parent = transform;
                        o.name = $"Cube X:{x}, Y:{y}, Z{z}.";
                        o.transform.localPosition = new Vector3((x + 0.5f) * squareSize, (y + 0.5f) * squareSize, (z + 0.5f) * squareSize);
                        o.transform.localScale = Vector3.one * squareSize;

                        
                    }


                    // Draw Cube.

                }
            }
        }
    }

    private void DrawMap() {
        bool mipChain = false;
        Texture2D texture = new Texture2D(resolution, resolution);

        for (int y = 0; y < resolution; y++) {
            for (int x = 0; x < resolution; x++) {
                Color color = (voxels[x,y] ? Color.black : Color.white);
                texture.SetPixel(x, y, color);
            }
        }

        // Apply all SetPixel calls
        texture.Apply();

        // connect texture to material of GameObject this script is attached to
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = texture;
    }


}
