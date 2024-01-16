using UnityEngine;
using System;

[Serializable]
public class EditorGridData
{
    public int width = 6;
    public int height = 6;
    public float widthThreshold = 1.5f;
    public float heightThreshold = 1.5f;
    public Vector3 initialPos = Vector3.zero;
    public GameObject gridPrefab;
}
