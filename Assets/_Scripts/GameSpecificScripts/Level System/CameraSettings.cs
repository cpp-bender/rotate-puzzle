using UnityEngine;

[CreateAssetMenu(menuName = "Hit Puzzle/Camera Settings", fileName = "Camera Settings")]
public class CameraSettings : ScriptableObject
{
    public Vector3 worldPos = Vector3.zero;
    public Vector3 worldRot = Quaternion.identity.eulerAngles;
}
