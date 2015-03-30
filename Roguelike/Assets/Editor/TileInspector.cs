using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tile))]
public class TileInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Tile theTile = (Tile)target;

        if (GUILayout.Button("Test"))
            theTile.CreateFeature(theTile);
            
    }
}