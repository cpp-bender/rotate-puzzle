using UnityEngine;

[CreateAssetMenu(menuName = "Hit Puzzle/Grid Settings", fileName = "Grid Settings")]
public class GridSettings : ScriptableObject
{
    public GameObject gridPrefab;
    public Vector3 initialPos = Vector3.zero;
    public int width = 6;
    public int height = 6;
    public float widthThreshold = 1f;
    public float heightThreshold = 1f;
}
