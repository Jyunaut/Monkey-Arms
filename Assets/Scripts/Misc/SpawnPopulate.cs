using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPopulate : MonoBehaviour
{
    [field: SerializeField] public GameObject SpawnObject { get; set; }
    [field: SerializeField] public Rect SpawningArea { get; set; }
    [field: SerializeField] public float CellSize { get; set; } = 1f;
    [field: SerializeField] public float DistanceBetweenObjects { get; set; } = 3f;
    [field: SerializeField] public int MINFrequency { get; set; } = 6;
    [field: SerializeField] public int MAXFrequency { get; set; } = 2;
    [field: SerializeField] public int OffsetY { get; set; } = 0;
    [field: SerializeField] public bool SpawnAtMonkeyArmsLOL { get; set; } = true;
    private Transform _parent;
    private List<GameObject> _spawnedObjects = new List<GameObject>();
    private Grid _grid;

    private void Awake()
    {
        _parent = this.transform.parent;
        if (SpawnObject == null) Debug.Log($"Missing {SpawnObject}");

        _grid = new Grid((int)SpawningArea.width, (int)SpawningArea.height, CellSize, SpawningArea.position);
        for(int x = 0; x < (int)SpawningArea.width; x++)
        {
            for (int y = 0; y < (int)SpawningArea.height; y++)
            {
                // NOTE: Add this if there are separate lanes/trees. This is to set a boolean at each gridcell
                //float offset = 0.3f;
                //Vector2 centerPosition = _grid.GetWorldPosition(x, y) + new Vector2(CellSize, CellSize) * .5f;
                //Collider2D[] colliders = Physics2D.OverlapBoxAll(centerPosition, new Vector2(CellSize - offset, CellSize - offset), 0, LayerMask.GetMask(GlobalStrings.kTree));
                _grid.SetGridCell(x, y, true);
            }
        }
        SetMonkeyBranches();
        PopulateLevel();
    }

    private void Spawn(Vector3 position)
    {
        GameObject obj = Instantiate(SpawnObject, new Vector2(position.x + CellSize * 0.5f, position.y + CellSize * 0.5f), Quaternion.identity);
        obj.transform.parent = _parent;
        _spawnedObjects.Add(obj);
    }

    public void PopulateLevel(Rect grid)
    {
        SpawningArea = grid;
        _grid = new Grid((int)SpawningArea.width, (int)SpawningArea.height, CellSize, SpawningArea.position);
        for(int x = 0; x < (int)SpawningArea.width; x++)
        {
            for (int y = 0; y < (int)SpawningArea.height; y++)
            {
                // NOTE: Add this if there are separate lanes/trees
                //float offset = 0.3f;
                //Vector2 centerPosition = _grid.GetWorldPosition(x, y) + new Vector2(CellSize, CellSize) * .5f;
                //Collider2D[] colliders = Physics2D.OverlapBoxAll(centerPosition, new Vector2(CellSize - offset, CellSize - offset), 0, LayerMask.GetMask(GlobalStrings.kTree));
                _grid.SetGridCell(x, y, true);
            }
        }
        SetMonkeyBranches();
        PopulateLevel();
    }
    
    private void PopulateLevel()
    {
        List<Vector2> currentSpawnList = new List<Vector2>();
        
        for(int y = OffsetY; y < (int)SpawningArea.height; y++)
        {
            currentSpawnList = GetValidSpawns(y);

            if(currentSpawnList.Count <= 0)
                continue;
            int frequency = Random.Range(MINFrequency, MAXFrequency);
            for(int i = 0; i < frequency; i++)
            {
                Spawn(currentSpawnList[Random.Range(0, currentSpawnList.Count - 1)]);
                currentSpawnList = GetValidSpawns(y);
                if(currentSpawnList.Count <= 0)
                    break;
            }
        }
        _grid = null;
    }

    private List<Vector2> GetValidSpawns(int y)
    {
        List<Vector2> validSpawns = new List<Vector2>();
        for(int x = 0; x < (int)SpawningArea.width; x++)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_grid.GetWorldPosition(x, y), DistanceBetweenObjects, LayerMask.GetMask(LayerMask.LayerToName(SpawnObject.layer)));
            if (colliders.Length <= 0) // NOTE: Add bool check: _grid.GetGridCell(x, y) IF USING TREES
                validSpawns.Add(_grid.GetWorldPosition(x, y));
        }
        return validSpawns;
    }

    private void SetMonkeyBranches()
    {
        if(SpawnAtMonkeyArmsLOL)
        {
            _grid.GetXY(GameObject.FindGameObjectWithTag("Monkey Left Hand").transform.position, out int xL, out int yL);
            _grid.GetXY(GameObject.FindGameObjectWithTag("Monkey Right Hand").transform.position, out int xR, out int yR);
            Spawn(_grid.GetWorldPosition(xL, yL));
            Spawn(_grid.GetWorldPosition(xR, yR));
        }
    }
}
