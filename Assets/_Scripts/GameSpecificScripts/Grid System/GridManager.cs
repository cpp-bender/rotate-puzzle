using System.Collections.Generic;
using UnityEngine;

public class GridManager : SingletonMonoBehaviour<GridManager>
{
    [Header("DEBUG"), Space(5f)]
    public List<VirtualGrid> virtualGrid;
    public List<VirtualGrid> tempGrid;
    public LayerMask layerMask;

    [Header("EDITOR")]
    public GridSettings gridData;

    private const float RAYDISTANCE = 2f;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        CheckGameOver();
    }

    private void Update()
    {
#if UNITY_EDITOR
        foreach (var grid in virtualGrid)
        {
            Debug.DrawRay(grid.worldPos + Vector3.up * 3f, Vector3.down * 3f, Color.green);

            Ray ray = new Ray(grid.worldPos + Vector3.up * 3f, Vector3.down * 3f);
            RaycastHit[] hits = Physics.RaycastAll(ray, 6f, layerMask);


            if (hits.Length == 1)
            {
                Debug.DrawRay(grid.worldPos + Vector3.up * 3f, Vector3.down * 3f, Color.black);
            }

            if (hits.Length > 1)
            {
                Debug.DrawRay(grid.worldPos + Vector3.up * 3f, Vector3.down * 3f, Color.cyan);
            }
        }
#endif
    }

    public void InitGrid(GridSettings gridSettings)
    {
        int height = gridSettings.height;
        int width = gridSettings.width;

        float widthThreshold = gridSettings.widthThreshold;
        float heightThreshold = gridSettings.heightThreshold;

        Vector3 worldPos = default(Vector3);
        Vector3 initialPos = gridSettings.initialPos;

        for (int z = 0; z < gridSettings.height; z++)
        {
            for (int x = 0; x < gridSettings.width; x++)
            {
                worldPos = initialPos + new Vector3(x * widthThreshold, 1f, z * heightThreshold);
                var gridObj = Instantiate(gridSettings.gridPrefab, worldPos, Quaternion.identity);
                gridObj.transform.SetParent(transform);
                virtualGrid.Add(new VirtualGrid(x, z, worldPos, true));
            }
        }
    }

    public void InitGridForEditor()
    {
        var grid = new GameObject("temp-grid");

        int width = gridData.width;
        int height = gridData.height;

        int index = 0;

        float widthThreshold = gridData.widthThreshold;
        float heightThreshold = gridData.heightThreshold;

        Vector3 worldPos = default(Vector3);
        Vector3 initialPos = gridData.initialPos;

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                worldPos = initialPos + new Vector3(x * widthThreshold, 0f, z * heightThreshold);
                var gridObj = Instantiate(gridData.gridPrefab, worldPos, Quaternion.identity);
                gridObj.GetComponent<GridObjectController>().SetText(index);
                gridObj.transform.SetParent(grid.transform);
                index++;
            }
        }
    }

    public void InitTempGrid()
    {
        //var tempGridData = Resources.Load<GridSettings>("temp-grid-settings");
        var tempGridData = gridData;

        int height = tempGridData.height;
        int width = tempGridData.width;

        int index = 0;

        float widthThreshold = tempGridData.widthThreshold;
        float heightThreshold = tempGridData.heightThreshold;

        Vector3 worldPos = default(Vector3);
        Vector3 initialPos = tempGridData.initialPos;

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                worldPos = initialPos + new Vector3(x * widthThreshold, 0f, z * heightThreshold);
                var gridObj = Instantiate(tempGridData.gridPrefab, worldPos, Quaternion.identity);
                gridObj.GetComponent<GridObjectController>().SetText(index);
                gridObj.transform.SetParent(transform);
                tempGrid.Add(new VirtualGrid(x, z, worldPos, true));
                index++;
            }
        }
    }

    public void CheckGameOver()
    {
        List<Vector3> collisionPositions = new List<Vector3>();

        int emptyGridCount = 0, collisionGridCount = 0;

        foreach (var grid in virtualGrid)
        {
            Ray ray = new Ray(grid.worldPos + Vector3.up * 3f, Vector3.down * 3f);
            RaycastHit[] hits = Physics.RaycastAll(ray, 6f, layerMask);

            if (hits.Length > 1)
            {
                collisionGridCount++;
                collisionPositions.Add(grid.worldPos + Vector3.up * 0.5f);
            }
            else if (hits.Length == 0)
            {
                emptyGridCount++;
            }
        }

        if (emptyGridCount == 0 && collisionGridCount == 0)
        {
            ReferenceManager.Instance.brickState = BrickState.Stop;
            GameManager.instance.LevelComplete();
        }
    }

    public void PlayEffects()
    {
        List<Vector3> collisionPositions = new List<Vector3>();

        int collisionGridCount = 0;

        foreach (var grid in virtualGrid)
        {
            Ray ray = new Ray(grid.worldPos + Vector3.up * 3f, Vector3.down * 3f);
            RaycastHit[] hits = Physics.RaycastAll(ray, 6f, layerMask);

            if (hits.Length > 1)
            {
                collisionGridCount++;
                collisionPositions.Add(grid.worldPos + Vector3.up * 0.5f);
            }
        }

        EffectManager.Instance.ShowEffects(collisionPositions);
    }
}
