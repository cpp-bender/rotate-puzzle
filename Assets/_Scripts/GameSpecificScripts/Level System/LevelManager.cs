using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    [Header("DEPENDENCIES")]
    public List<LevelData> easyLevels;
    public List<LevelData> hardLevels;

    [Header("DEBUG")]
    public LevelData currentLevel;
    public int levelIndex;

    private GridManager gridManager;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        InitLevel();
    }

    private void InitLevel()
    {
        gridManager = FindObjectOfType<GridManager>();

        if (GameManager.instance.currentLevel <= 40)
        {
            levelIndex = (GameManager.instance.currentLevel - 1) % easyLevels.Count;
            currentLevel = easyLevels[levelIndex];
        }
        else
        {
            levelIndex = (GameManager.instance.currentLevel - 1) % hardLevels.Count;
            currentLevel = hardLevels[levelIndex];
        }

        gridManager.InitTempGrid();

        gridManager.InitGrid(currentLevel.GridSettings);

        InitBricks();

        InitCamera();

        InitFrame();
    }

    private void InitBricks()
    {
        var virtualGrid = gridManager.tempGrid;

        var brickData = currentLevel.BrickData;

        for (int i = 0; i < brickData.Count; i++)
        {
            int index = brickData[i].gridIndex;
            BaseBrickController brickGO = brickData[i].parentBrick;
            Vector3 worldPos = virtualGrid[index].worldPos + (Vector3.up * 1f);

            Vector3 initialRot = brickGO.BrickRotationToEulerAngles(brickData[i].initialRotation);
            Vector3 nextRot = brickGO.BrickRotationToEulerAngles(brickData[i].nextRotation);

            var brick = Instantiate(brickGO);

            brick.InitialSetup(initialRot, nextRot, brickData[i].material, null, brickData[i].initialRotation, brickData[i].nextRotation);
            brick.transform.position = worldPos;
            brick.transform.localRotation = Quaternion.Euler(initialRot);

            HandleChildren(brickData[i].children, brick.transform);
        }
    }

    private void HandleChildren(List<LevelBrickData> children, Transform parent)
    {
        var virtualGrid = gridManager.tempGrid;
        foreach (var child in children)
        {
            int chidlIndex = child.gridIndex;
            BaseBrickController chidlBrickGO = child.parentBrick;
            Vector3 chidlWorldPos = virtualGrid[chidlIndex].worldPos + (Vector3.up * 1f);

            Vector3 childInitialRot = chidlBrickGO.BrickRotationToEulerAngles(child.initialRotation);
            Vector3 childNextRot = chidlBrickGO.BrickRotationToEulerAngles(child.nextRotation);

            var childBrick = Instantiate(chidlBrickGO);

            childBrick.InitialSetup(childInitialRot, childNextRot, child.material, parent, child.initialRotation, child.nextRotation);

            childBrick.transform.position = chidlWorldPos;
            childBrick.transform.localRotation = Quaternion.Euler(childInitialRot);

            if (child.children.Count > 0)
            {
                HandleChildren(child.children, childBrick.transform);
            }
        }
    }

    private void InitCamera()
    {
        var cam = Camera.main;

        cam.transform.position = currentLevel.CameraSettings.worldPos;
        cam.transform.rotation = Quaternion.Euler(currentLevel.CameraSettings.worldRot);
    }

    private void InitFrame()
    {
        var frame = Instantiate(currentLevel.FrameSettings.framePrefab);

        frame.transform.position = currentLevel.FrameSettings.worldPos;

        frame.transform.localScale = currentLevel.FrameSettings.scale;
    }
}
