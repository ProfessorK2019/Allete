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
    private TileBase currentTile => LevelManagerMaster.Instance.tiles[selectedTileIndex].tile;
    private Dictionary<string, int> obstacleQuantities = new Dictionary<string, int>();
    private ObstacleSO currentObstacle;
    private Vector3Int mousePositionOnGrid;
    private int selectedTileIndex;

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

        // LogObstacleQuantities(); Use For Logging Dict
    }
    private void OnDragEndHandler(string obstacleName)
    {
        foreach (ObstacleSO obstacle in LevelManagerMaster.Instance.obstacles)
        {
            if (obstacleName == obstacle.id)
            {
                currentObstacle = obstacle;
                DrawObstacle();
                UpdateObstacleQuantity(obstacle.id, 1);
                DragDropObjectUI.Instance.UpdateUI(obstacleQuantities);
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
            Instantiate(currentObstacle.obstacle, worldPos, Quaternion.identity);
        }

    }
    private void DeleteObstacle()
    {
        GameObject obstacleToDelete = LevelManagerMaster.Instance.GetObstacleAtPosition(mousePositionOnGrid);

        if (obstacleToDelete != null)
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
    private bool IsOccupiedPosition()
    {
        GameObject existingObject = LevelManagerMaster.Instance.GetObstacleAtPosition(mousePositionOnGrid);
        return existingObject != null;
    }
    #endregion
    #region Tiles
    private void DrawTile()
    {
        if (CanDraw()) tilemap.SetTile(mousePositionOnGrid, currentTile);
    }
    private void DeleteTile()
    {
        tilemap.SetTile(mousePositionOnGrid, null);
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
