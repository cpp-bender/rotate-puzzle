using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var gridManager = (GridManager)target;

        if (GUILayout.Button("Show Grid"))
        {
            var tempGridObj = GameObject.Find("temp-grid");

            if (tempGridObj != null)
            {
                DestroyImmediate(tempGridObj);
            }

            gridManager.InitGridForEditor();
        }

        if (GUILayout.Button("Clear Grid"))
        {
            var tempGridObj = GameObject.Find("temp-grid");

            if (tempGridObj != null)
            {
                DestroyImmediate(tempGridObj);
            }
        }
    }
}
