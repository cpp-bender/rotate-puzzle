using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hit Puzzle/Level Data", fileName = "Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Grid Settings")]
    [SerializeField] GridSettings gridSettings;

    [Header("Object Settings")]
    [SerializeField] List<LevelBrickData> brickData;

    [Header("Camera Settings")]
    [SerializeField] CameraSettings cameraSettings;

    [Header("Frame Settings")]
    [SerializeField] FrameSettings frameSettings;

    public GridSettings GridSettings { get => gridSettings; }
    public List<LevelBrickData> BrickData { get => brickData; }
    public CameraSettings CameraSettings { get => cameraSettings; }
    public FrameSettings FrameSettings { get => frameSettings; }
}
