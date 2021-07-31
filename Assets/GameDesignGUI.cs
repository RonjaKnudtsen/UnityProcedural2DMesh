using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDesignGUI : MonoBehaviour{
    public VoxelMap voxelMap;
    public MeshGenerator meshGenerator;
    // Start is called before the first frame update
    void Awake(){
        if (!voxelMap) {
            Debug.Log("GUI needs voxelMap");
        }
        if (!meshGenerator) {
            Debug.Log("GUI needs mesGenerator");
        }

    }

    private void OnGUI() {
        GUILayout.BeginArea(new Rect(4f, 4f, 150f, 500f));

        GUILayout.Label("Scale");
        voxelMap.perlinScale = GUILayout.HorizontalSlider(voxelMap.perlinScale, 1f, 100f);
        GUILayout.Label("Offset Horizontal");
        voxelMap.perlinOffsetX = GUILayout.HorizontalSlider(voxelMap.perlinOffsetX, 0f, 1000f);
        GUILayout.Label("Offset Vertical");
        voxelMap.perlinOffsetY = GUILayout.HorizontalSlider(voxelMap.perlinOffsetY, 0f, 1000f);
        GUILayout.Label("Thickness");
        voxelMap.perlinThickness = GUILayout.HorizontalSlider(voxelMap.perlinThickness, 0.1f, 1f);


        if (GUILayout.Button("Refresh")) {
            voxelMap.refreshPerlinNoiseMap();
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Create Cave")) {
            meshGenerator.createMesh();
        }

        GUILayout.EndArea();
    }
}
