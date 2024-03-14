using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerate : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    TileBase currentTile => LevelManagerMaster.Instance.tiles[selectedTileIndex].tile;
    [SerializeField] private Camera cam;

    int selectedTileIndex;
    void Update()
    {
        Vector3Int pos = tilemap.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetMouseButton(0)) PlaceTile(pos);
        if (Input.GetMouseButton(1)) DeleteTile(pos);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedTileIndex++;
            if (selectedTileIndex >= LevelManagerMaster.Instance.tiles.Count) selectedTileIndex = 0;
            Debug.Log(LevelManagerMaster.Instance.tiles[selectedTileIndex].name);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            selectedTileIndex--;
            if (selectedTileIndex < 0) selectedTileIndex = LevelManagerMaster.Instance.tiles.Count - 1;
            Debug.Log(LevelManagerMaster.Instance.tiles[selectedTileIndex].name);
        }

    }
    void PlaceTile(Vector3Int pos)
    {
        Vector3Int tilePos = new Vector3Int(pos.x, pos.y, 0);
        tilemap.SetTile(tilePos, currentTile);
    }
    void DeleteTile(Vector3Int pos)
    {
        Vector3Int tilePos = new Vector3Int(pos.x, pos.y, 0);
        tilemap.SetTile(tilePos, null);
    }
}
