using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPopulate : MonoBehaviour
{
    [field: SerializeField] public GameObject SpawnObject { get; set; }
    [field: SerializeField] public Rect SpawningArea { get; set; }
    [field: SerializeField] public int DistanceBetweenObjects { get; set; } = 3;
    [field: SerializeField] public float CellSize { get; set; } = 1f;
    private List<GameObject> _spawnedObjects = new List<GameObject>();
    private Grid _grid;

    private void Awake()
    {
        if (SpawnObject == null) Debug.Log($"Missing {SpawnObject}");

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
        PopulateLevel();
    }

    private void Spawn(Vector3 position)
    {
        _spawnedObjects.Add(Instantiate(SpawnObject, new Vector2(position.x + CellSize * 0.5f, position.y + CellSize * 0.5f), Quaternion.identity));
    }

    private void PopulateLevel()
    {
        List<Vector2> currentSpawnList = new List<Vector2>();
        
        for(int y = 0; y < (int)SpawningArea.height; y++)
        {
            currentSpawnList = GetValidSpawns(y);

            if(currentSpawnList.Count <= 0)
                continue;

            while(true)
            {
                Spawn(currentSpawnList[Random.Range(0, currentSpawnList.Count - 1)]);
                currentSpawnList = GetValidSpawns(y);
                if(currentSpawnList.Count <= 0)
                    break;
            }
        }
    }

    private List<Vector2> GetValidSpawns(int y)
    {
        List<Vector2> validSpawns = new List<Vector2>();
        for(int x = 0; x < (int)SpawningArea.width; x++)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_grid.GetWorldPosition(x, y), DistanceBetweenObjects, LayerMask.GetMask(GlobalStrings.kHandle));
            if (colliders.Length <= 0) // Add _grid.GetGridCell(x, y) check IF USING TREES
                validSpawns.Add(_grid.GetWorldPosition(x, y));
        }
        return validSpawns;
    }
}
