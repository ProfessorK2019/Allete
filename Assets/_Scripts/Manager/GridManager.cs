using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private Grid gridMap;
    public List<Tilemap> tileMaps { private set; get; }
    public float gridSizeX { private set; get; }
    public float gridSizeY { private set; get; }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateTileMaps();

        gridSizeX = gridMap.cellSize.x + gridMap.cellGap.x;
        gridSizeY = gridMap.cellSize.y + gridMap.cellGap.y;
    }
    public void UpdateTileMaps()
    {
        tileMaps = new List<Tilemap>();
        Tilemap[] allTilemaps = gridMap.GetComponentsInChildren<Tilemap>();
        foreach (Tilemap tilemap in allTilemaps)
        {
            if (tilemap != null && tilemap.gameObject != null) tileMaps.Add(tilemap);
        }
        // if (tileMaps.Count == 0) Debug.LogWarning("No Tilemaps found");
    }
    public Grid GetGridMap() => gridMap;
    public Tilemap GetTileMap(Vector3Int position)
    {
        foreach (Tilemap tilemap in tileMaps)
        {
            if (tilemap != null && tilemap.gameObject != null && tilemap.GetTile(position) != null)
            {
                return tilemap;
            }
        }
        return null;
    }
    public Vector3Int GetGridPosition(Vector3 position) => gridMap.WorldToCell(position);
    public Vector3 GetGridCenterPosition(Vector3 position)
    {
        return gridMap.GetCellCenterWorld(GetGridPosition(position));
    }
    public Vector3 GetCenterPositionFromGridPosition(Vector3Int position)
    {
        return gridMap.GetCellCenterWorld(position);
    }

}
