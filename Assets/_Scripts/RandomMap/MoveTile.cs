using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveTiles : MonoBehaviour
{
    [Header("NOTE: click on connectTile to see it position")]
    [SerializeField] private Vector3Int endTilePos;
    [SerializeField] private Vector3Int startTilePos;
    private Tilemap tilemap;
    private GameObject player;
    private Vector3Int connectPos = new Vector3Int(-2,-1,0);
    private List<Vector3Int> tileList = new List<Vector3Int>();
    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        player = transform.GetChild(0).gameObject;
    }
    void Start()
    {
        FindTile();
        SetTilePosition();
    }
    private void FindTile()
    {
        BoundsInt bounds = tilemap.cellBounds;
        //seach all tilemap to find Tile
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(cellPosition);

                if (tile != null)
                {
                    //if has tile add to the list
                    tileList.Add(cellPosition);

                }
            }
        }
    }
    private void SetTilePosition()
    {
        Vector3Int distance = SetEndTilePosAndCalculateDistance();
        MoveRemainingTiles(distance);
    }

    private Vector3Int SetEndTilePosAndCalculateDistance()
    {
        foreach (Vector3Int tilePos in tileList)
        {
            if (tilePos == endTilePos)//Find connectTile
            {
                MoveTile(tilePos, connectPos);
                return connectPos - tilePos;
            }
        }
        //not find any connectTile
        return Vector3Int.zero;
    }

    private void MoveRemainingTiles(Vector3Int distance)
    {
        foreach (Vector3Int tilePos in tileList)
        {
            if (tilePos != endTilePos)
            {
                if (tilePos == startTilePos)
                {
                    Vector3Int newPos = tilePos + distance;
                    MoveTile(tilePos, newPos);
                    //set player Pos to startTilePos
                    player.transform.position = tilemap.GetCellCenterWorld(newPos);
                }
                else
                {
                    Vector3Int newPos = tilePos + distance;
                    MoveTile(tilePos, newPos);
                }
            }
        }
    }
    private void MoveTile(Vector3Int tileToMovePos, Vector3Int newPos)
    {
        TileBase tile = tilemap.GetTile(tileToMovePos);
        tilemap.SetTile(tileToMovePos, null);
        tilemap.SetTile(newPos, tile);
    }
}
