using System;
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
    [SerializeField] private GameObject spawnPointPrefab;
    private TileBase currentTile => LevelManagerMaster.Instance.tiles[selectedTileIndex].tile;
    private Dictionary<string, int> obstacleQuantities = new Dictionary<string, int>();
    private ObstacleSO currentObstacle;
    private Vector3Int mousePositionOnGrid;
    private int selectedTileIndex;
    private string winningTile = "sprite_IsometricGrass_pink";

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DragDropObject.OnDragEnd += OnDragEndHandler;
    }
    private void Update()
    {
        //get mousePos on Gridmap
        mousePositionOnGrid = FixCameraPosition(tilemap.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition)));

        // if (Input.GetMouseButton(0)) DrawTile();// Left-Click to draw tile
        if (Input.GetMouseButton(1)) DeleteObstacle();// Right-Click to delete tile

        if (Input.GetKeyDown(KeyCode.Z)) DrawTile();
        if (Input.GetKeyDown(KeyCode.X)) DeleteTile();
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
        // LogObstacleQuantities();// Use For Logging Dict
    }
    private void OnDragEndHandler(string obstacleName)
    {
        if (HasTile())//Check if has tile to draw obstacle
        {
            foreach (ObstacleSO obstacle in LevelManagerMaster.Instance.obstacles)
            {
                if (obstacleName == obstacle.id)
                {
                    currentObstacle = obstacle;
                    if (currentObstacle.id == "Spike")
                    {

                        if (CheckIfHasTileSurround(mousePositionOnGrid).Count >= 2)//Check if enough valid path or not
                        {
                            DrawObstacle();
                        }
                    }
                    else DrawObstacle();
                }
            }
        }

    }
    #region Obstacles
    private void UpdateObstacleQuantity(string obstacleID, int change)
    {
        if (obstacleQuantities.ContainsKey(obstacleID))
        {
            int newQuantity = obstacleQuantities[obstacleID] + change;

            // Check quantity range to update
            if (newQuantity >= 0 && newQuantity <= LevelManagerMaster.Instance.GetObstacleSOByID(obstacleID).quantity)
            {
                obstacleQuantities[obstacleID] = newQuantity;
            }
            else
            {
                Debug.LogWarning("Cannot update obstacle quantity. New quantity exceeds the limit.");
            }
        }
        else
        {
            obstacleQuantities[obstacleID] = change;
        }
    }

    private void DrawObstacle()
    {
        Vector3 worldPos = tilemap.GetCellCenterWorld(mousePositionOnGrid);

        if (CanDraw() && !IsOccupiedPosition() && EnoughQuantity(currentObstacle))
        {
            GameObject obstacle = Instantiate(currentObstacle.obstacle, worldPos, Quaternion.identity);
            if (currentObstacle.id == "Spike")
            {
                AddSpikeSpawnPoint(obstacle);//add spawnPoint to current Spike
            }
            UpdateObstacleQuantity(currentObstacle.id, 1);
            DragDropObjectUI.Instance.UpdateUI(obstacleQuantities);
        }

    }
    private void DeleteObstacle()
    {
        GameObject obstacleToDelete = LevelManagerMaster.Instance.GetObstacleAtPosition(mousePositionOnGrid);

        if (obstacleToDelete != null && !obstacleToDelete.CompareTag("UnDeletable"))
        {
            Destroy(obstacleToDelete);
            UpdateObstacleQuantity(obstacleToDelete.name.Replace("(Clone)", ""), -1);
            DragDropObjectUI.Instance.UpdateUI(obstacleQuantities);
        }
    }
    private bool EnoughQuantity(ObstacleSO obstacleSO)
    {
        if (!obstacleQuantities.ContainsKey(obstacleSO.id))
        {
            obstacleQuantities[obstacleSO.id] = 0;
        }

        return obstacleQuantities[obstacleSO.id] < obstacleSO.quantity;
    }

    private void AddSpikeSpawnPoint(GameObject parentObstacle)
    {
        Transform spawnPoint1 = parentObstacle.transform.GetChild(0);
        Transform spawnPoint2 = parentObstacle.transform.GetChild(1);

        List<Transform> spawnPointList = new List<Transform>
        {
            spawnPoint1,
            spawnPoint2
        };

        int count = 0, index = 0;
        foreach (Vector3Int pos in CheckIfHasTileSurround(mousePositionOnGrid))
        {
            spawnPointList[index].position = tilemap.GetCellCenterWorld(pos);
            index++;
            count++;
            if (count >= 2)
            {
                break;
            }
        }
    }

    private bool IsOccupiedPosition()
    {
        GameObject existingObject = LevelManagerMaster.Instance.GetObstacleAtPosition(mousePositionOnGrid);
        return existingObject != null;
    }
    #endregion
    #region Tiles
    private void DrawTile()
    {
        if (CanDraw())
        {
            if (LevelManagerMaster.Instance.tiles[selectedTileIndex].id == "WinningTile" && CheckIfHasWinningTile())
            {
                return;
            }
            tilemap.SetTile(mousePositionOnGrid, currentTile);
        }

    }
    private void DeleteTile()
    {
        tilemap.SetTile(mousePositionOnGrid, null);
    }
    private TileBase HasTile()
    {
        return tilemap.GetTile(mousePositionOnGrid);
    }
    private List<Vector3Int> CheckIfHasTileSurround(Vector3Int pos)
    {
        Vector3Int[] directions = { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };
        List<Vector3Int> validPos = new List<Vector3Int>();
        foreach (Vector3Int direction in directions)
        {
            Vector3Int targetPos = pos + direction;

            if (tilemap.GetTile(targetPos) != null)
            {

                validPos.Add(targetPos);

            }
        }
        return validPos;
    }
    #endregion
    #region others Func
    private Vector3Int FixCameraPosition(Vector3Int pos)//Turn CamZ to 0
    {
        return new Vector3Int(pos.x, pos.y, 0);
    }
    private bool CanDraw()
    {
        Vector3Int tilePos = FixCameraPosition(mousePositionOnGrid);
        foreach (Vector3 drawablePos in GridDraw.GetDrawablePosList())//Check if tile can draw or not
        {
            Vector3Int drawableTilePos = tilemap.WorldToCell(drawablePos);

            if (drawableTilePos == tilePos)
            {
                return true;
            }
        }
        return false;
    }
    private bool CheckIfHasWinningTile()
    {
        foreach (Vector3 drawablePos in GridDraw.GetDrawablePosList())//Check if tile can draw or not
        {
            Vector3Int drawableTilePos = tilemap.WorldToCell(drawablePos);

            if (tilemap.GetTile(drawableTilePos) != null && tilemap.GetTile(drawableTilePos).name == winningTile)
            {

                return true;
            }
        }
        return false;
    }
    private void LogObstacleQuantities()
    {
        foreach (var pair in obstacleQuantities)
        {
            string obstacleName = pair.Key;
            int quantity = pair.Value;
            Debug.Log("Obstacle Name: " + obstacleName + ", Quantity: " + quantity);
        }
    }
    #endregion
    void OnDisable()
    {
        DragDropObject.OnDragEnd -= OnDragEndHandler;
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(Vector2.zero, mousePositionOnGrid);
    }

}
