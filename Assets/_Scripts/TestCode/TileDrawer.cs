using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Use for drawing,deleting Tile
/// </summary>

public class TileDrawer : MonoBehaviour
{
    public static TileDrawer Instance;
    public event Action<int> OnTileChange;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Camera cam;
    TileBase currentTile => LevelManagerMaster.Instance.tiles[selectedTileIndex].tile;
    private int selectedTileIndex;
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        Vector3Int pos = tilemap.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetMouseButton(0)) PlaceTile(pos);// Left-Click to draw tile
        if (Input.GetMouseButton(1)) DeleteTile(pos);// Right-Click to delete tile

        //Q and E to change tile
        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedTileIndex++;
            if (selectedTileIndex >= LevelManagerMaster.Instance.tiles.Count) selectedTileIndex = 0;
            OnTileChange?.Invoke(selectedTileIndex);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            selectedTileIndex--;
            if (selectedTileIndex < 0) selectedTileIndex = LevelManagerMaster.Instance.tiles.Count - 1;
            OnTileChange?.Invoke(selectedTileIndex);
        }

    }
    void PlaceTile(Vector3Int pos)
    {
        List<Vector3> gridPosList = GridDraw.GetGridPosList();
        Vector3Int tilePos = FixCameraPosition(pos);

        foreach (Vector3 gridPos in gridPosList)//Check if tile can draw or not
        {
            Vector3Int drawableTilePos = tilemap.WorldToCell(gridPos);

            if (drawableTilePos == tilePos)
            {
                tilemap.SetTile(tilePos, currentTile);
                break;
            }
        }
    }
    void DeleteTile(Vector3Int pos)
    {
        tilemap.SetTile(FixCameraPosition(pos), null);
    }
    private Vector3Int FixCameraPosition(Vector3Int pos)
    {
        return new Vector3Int(pos.x, pos.y, 0);
    }
}
