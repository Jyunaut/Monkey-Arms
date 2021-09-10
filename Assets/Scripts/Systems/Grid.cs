using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grid
{
    private int _x, _y;
    private int _width, _height;
    private float _cellSize;
    private Vector2 _origin;
    private bool[,] _grid;

    public Grid(int width, int height, float cellSize, Vector2 origin)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _origin = origin;

        _grid = new bool[_width, _height];

        // Debug
        bool DebugLines = true;
        if(DebugLines)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.black, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.black, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.black, 100f);
            Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.black, 100f);
        }
    }

    public void SetGridCell(int x, int y, bool cell)
    {
        if (x >= 0 && 
            y >= 0 && 
            x < _width && 
            y < _height)
        {
            _grid[x, y] = cell;
        }
    }

    public void SetGridCell(Vector2 worldPosition, bool cell)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridCell(x, y, cell);
    }

    public bool GetGridCell(int x, int y)
    {
        return (x >= 0 && y >= 0 && x < _width && y < _height) ? _grid[x, y] : default(bool);
    }

    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * _cellSize + _origin;
    }    
    
    public bool GetGridCell(Vector2 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridCell(x, y);
    }

    public void GetXY(Vector2 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - _origin).x / _cellSize);
        y = Mathf.FloorToInt((worldPosition - _origin).y / _cellSize);
    }
}
