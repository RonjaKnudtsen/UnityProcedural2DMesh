using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// THE VOXEL MAP IS A REPRESENTATION OF A BIGGER WORLD. 
// "voxel represents a value on a regular grid in three-dimensional space" - wikipedia
public class VoxelMap : MonoBehaviour {
    public int resolution = 256;
    public float perlinOffsetX = 40;
    public float perlinOffsetY = 40;
    public float perlinThickness = 0.5f;
    public float perlinScale = 20f;

    public int size;
    private bool[,] voxels; // on and off values. 

    public MeshGenerator meshGenerator;

    public void refreshPerlinNoiseMap() {
        AddPerlinNoise();
        DrawMap();
    }

    private void Awake() {
        voxels = new bool[resolution, resolution];
        bool defaultState = false;
        for (int y = 0; y < resolution; y++) {
            for (int x = 0; x < resolution; x++) {
                voxels[x, y] = defaultState;
            }
        }
        AddPerlinNoise();
        DrawMap();
    }

    private void AddPerlinNoise() {
        for (int y = 0; y < resolution; y++) {
            for (int x = 0; x < resolution; x++) {
                float xCoord = (float)x / resolution * perlinScale + perlinOffsetX; // must be between 0-1
                float yCoord = (float)y / resolution * perlinScale + perlinOffsetY;
                

                float v = Mathf.PerlinNoise(xCoord, yCoord);
                if(v < perlinThickness) {
                    voxels[x, y] = true;
                } else {
                    voxels[x, y] = false;
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

    private void OnGUI() {
       /* GUILayout.BeginArea(new Rect(4f, 4f, 150f, 500f), "area 1");
        GUILayout.Label("Perlin parameters");
        GUILayout.Label("Scale");
        perlinScale = GUILayout.HorizontalSlider(perlinScale, 1f, 100f);
        GUILayout.Label("Offset Horizontal");
        perlinOffsetX = GUILayout.HorizontalSlider(perlinOffsetX, 0f, 1000f);
        GUILayout.Label("Offset Vertical");
        perlinOffsetY = GUILayout.HorizontalSlider(perlinOffsetY, 0f, 1000f);
        GUILayout.Label("Thickness");
        perlinThickness = GUILayout.HorizontalSlider(perlinThickness, 0.1f, 1f);

        if (GUILayout.Button("Refresh")) {
            Debug.Log("REFRESH");
            refreshPerlinNoiseMap();
        }
        GUILayout.EndArea();
        GUILayout.BeginArea(new Rect(4f, 4f, 150f, 500f), "area 2");
        GUILayout.Label("Thickness");
        GUILayout.EndArea();*/
    }

}
