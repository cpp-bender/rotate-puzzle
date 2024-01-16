using UnityEngine;

[CreateAssetMenu(menuName = "Hit Puzzle/Frame Settings", fileName = "Frame Settings")]
public class FrameSettings : ScriptableObject
{
    public GameObject framePrefab;
    public Vector3 worldPos = new Vector3(1f, 1f, 1f);
    public Vector3 scale = Vector3.one;
}
