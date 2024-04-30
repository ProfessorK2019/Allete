using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class LevelManagerMaster : MonoBehaviour
{
    public static LevelManagerMaster Instance;
    private void Awake()
    {
        //set up the instance
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public List<TileSO> tiles = new List<TileSO>();
    public List<ObstacleSO> obstacles = new List<ObstacleSO>();
    public Tilemap tilemap;
    [SerializeField] private GameObject winningParticlePrefab;

    private void Update()
    {
        //save level when pressing Ctrl + A
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.A)) Savelevel();
        //load level when pressing Ctrl + L
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L)) LoadLevel();
    }

    private void Savelevel()
    {
        //get the bounds of the tilemap
        BoundsInt bounds = tilemap.cellBounds;

        //create a new leveldata
        LevelData levelData = new LevelData();

        //loop trougth the bounds of the tilemap
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);

                GameObject currentObstacle = GetObstacleAtPosition(new Vector3Int(x, y, 0));

                if (currentObstacle != null)
                {
                    string currentObjectName = currentObstacle.name.Replace("(Clone)", "");
                    //save spawnPointPos
                    if (currentObjectName == "SpawnPoint1" || currentObjectName == "SpawnPoint2")
                    {
                        levelData.spawnPointPos.Add(currentObstacle.transform.localPosition);
                    }
                    //save obstacle pos and id
                    ObstacleSO tempObstacle = obstacles.Find(o => o.id == currentObjectName);
                    if (tempObstacle != null)
                    {
                        levelData.obstacles.Add(tempObstacle.id);
                        levelData.obstaclePos.Add(pos);

                    }

                }
                //get the tile on the position
                TileBase currentTile = tilemap.GetTile(new Vector3Int(x, y, 0));
                TileSO tempTile = tiles.Find(t => t.tile == currentTile);
                //if there's a TileSO associated with the tile
                if (tempTile != null)
                {
                    //add the values to the leveldata
                    levelData.tiles.Add(tempTile.id);
                    levelData.tilePos.Add(pos);
                }
            }
        }

        //save the data as a json
        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(Application.dataPath + "/testLevel.json", json);

        //debug
        Debug.Log("Level was saved");
    }

    private void LoadLevel()
    {
        //load the json file to a leveldata
        string json = File.ReadAllText(Application.dataPath + "/testLevel.json");
        LevelData data = JsonUtility.FromJson<LevelData>(json);

        //clear the tilemap
        tilemap.ClearAllTiles();

        //place the tiles
        for (int i = 0; i < data.tiles.Count; i++)
        {
            tilemap.SetTile(new Vector3Int(data.tilePos[i].x, data.tilePos[i].y, 0), tiles.Find(t => t.id == data.tiles[i]).tile);
            if (data.tiles[i] == "WinningTile")
            {
                Vector3Int winningTilePos = new Vector3Int(data.tilePos[i].x, data.tilePos[i].y, 0);
                Instantiate(winningParticlePrefab, tilemap.GetCellCenterWorld(winningTilePos), Quaternion.identity);
            }
        }
        //place objects
        for (int i = 0; i < data.obstacles.Count; i++)
        {
            Vector3Int obstaclePos = new Vector3Int(data.obstaclePos[i].x, data.obstaclePos[i].y, 0);
            GameObject obstaclePrefab = obstacles.Find(o => o.id == data.obstacles[i]).obstacle;

            GameObject obstalce = Instantiate(obstaclePrefab, tilemap.GetCellCenterWorld(obstaclePos), Quaternion.identity);
            //place SpawnPointPos
            Debug.Log(obstalce.name);
            if (obstalce.name == "Spike(Clone)")
            {

                Transform spawnPoint1 = obstalce.transform.GetChild(0);
                Transform spawnPoint2 = obstalce.transform.GetChild(1);

                spawnPoint1.localPosition = data.spawnPointPos[0];
                spawnPoint2.localPosition = data.spawnPointPos[1];

            }
        }




        Debug.Log("Level was loaded");
    }

    public GameObject GetObstacleAtPosition(Vector3Int pos)
    {
        // Get pos on tilemap
        Vector3 worldPos = tilemap.GetCellCenterWorld(pos);

        // get obstaclePos on tilemap
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit.collider != null && !hit.collider.gameObject.CompareTag("UnDetectedable"))
        {
            return hit.collider.gameObject;
        }
        return null;
    }
    public ObstacleSO GetObstacleSOByID(string obstacleID)
    {
        foreach (ObstacleSO obstacle in obstacles)
        {
            if (obstacle.id == obstacleID)
            {
                return obstacle;
            }
        }
        return null;
    }
}


public class LevelData
{
    public List<string> tiles = new List<string>();
    public List<string> obstacles = new List<string>();
    public List<Vector2Int> tilePos = new List<Vector2Int>();
    public List<Vector2Int> obstaclePos = new List<Vector2Int>();
    public List<Vector3> spawnPointPos = new List<Vector3>();

}