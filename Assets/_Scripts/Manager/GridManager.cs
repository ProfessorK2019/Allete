using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{   
    public static GridManager Instance;
    [SerializeField] private Grid gridMap;
    public Tilemap tileMap { private set; get; }
    public float gridSizeX { private set; get; }
    public float gridSizeY { private set; get; }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        tileMap = gridMap.GetComponentInChildren<Tilemap>();
        if (tileMap == null) Debug.LogWarning("No Tilemap found");

        gridSizeX = gridMap.cellSize.x + gridMap.cellGap.x;
        gridSizeY = gridMap.cellSize.y + gridMap.cellGap.y;
    }
    public Grid GetGridMap() => gridMap;
    private Vector3Int GetGridPosition(Vector3 position) =>gridMap.WorldToCell(position);
    public Vector3 GetGridCenterPosition(Vector3 position)
    {
        return gridMap.GetCellCenterWorld(GetGridPosition(position));
    }    
    

}
