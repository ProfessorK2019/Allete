using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomTest : MonoBehaviour
{   
    [Header("Map Properties")]
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private int mapWidth = 10;
    [SerializeField] private int mapHeight = 10;
    [SerializeField] private int minTilesBetweenTwoPosition = 3;//Decide distance between two Pos

    [Header("Tile Settings")]
    [SerializeField] private TileBase groundTile;
    [SerializeField] private TileBase endTile;
    [SerializeField] private TileBase pathTile;
    [SerializeField] private TileBase spikeTile;

    [Header("SpawnObject")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject spikePrefab;
    [SerializeField] private GameObject finishParticlePrefab;
    
    private List<Vector3> edgePositionList = new List<Vector3>();
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 spikePosition;

    private List<Node> nodes = new List<Node>();
    private List<Node> spikeToEndPath = new List<Node>();
    private List<Node> spikeToEndPath2 = new List<Node>();

    private List<Node> spikeToStartPath = new List<Node>();


    void Start()
    {
        GenerateMap();

        GenerateEdgePositionList();
        GenerateSpikePosition();
        GenerateStartPosition();
        GenerateEndPosition();

        spikeToEndPath = GeneratePath(spikePosition, endPosition);
        DrawPath(spikeToEndPath);
        spikeToEndPath2 = GeneratePath(spikePosition, endPosition);
        DrawPath(spikeToEndPath2);
        spikeToStartPath = GeneratePath(spikePosition, startPosition);
        DrawPath(spikeToStartPath);

        SpawnObject(playerPrefab, startPosition);
        SpawnObject(spikePrefab, spikePosition);
        SpawnObject(finishParticlePrefab, endPosition);

    }
    private Node FindNodeAtPosition(Vector3 position)
    {
        return nodes.Find(node => Mathf.FloorToInt(node.x) == Mathf.FloorToInt(position.x) && Mathf.FloorToInt(node.y) == Mathf.FloorToInt(position.y));
    }

    private void DrawPath(List<Node> path)
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            Node currentNode = path[i];
            Vector3Int cellPosition = new Vector3Int(currentNode.x, currentNode.y, 0);
            tilemap.SetTile(cellPosition, pathTile);
        }
    }
    private void SpawnObject(GameObject objPrefab, Vector3 postion)
    {   
        Instantiate(objPrefab,tilemap.GetCellCenterWorld(Vector3Int.FloorToInt(postion)), Quaternion.identity);
    }
    #region Generate Function
    private void GenerateMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                // All tiles are considered walkable
                bool isWalkable = true;

                nodes.Add(new Node(x, y, isWalkable));
            }
        }
    }
    private void GenerateEdgePositionList()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                bool isEdgeTile = x == 0 || y == 0 || x == mapWidth - 1 || y == mapHeight - 1;

                if (isEdgeTile)
                {
                    edgePositionList.Add(new Vector3(x, y, 0));
                }
            }
        }
    }

    private void GenerateSpikePosition()
    {
        spikePosition = GetRandomPosition();
        for (int i = edgePositionList.Count - 1; i >= 0; i--)
        {
            Vector3 position = edgePositionList[i];

            if (position == spikePosition)
            {
                edgePositionList.RemoveAt(i);
                break;
            }
        }
        tilemap.SetTile(Vector3Int.FloorToInt(spikePosition), spikeTile);

    }

    private void GenerateStartPosition()
    {
        startPosition = GetRandomPositionFromEdge();
        tilemap.SetTile(Vector3Int.FloorToInt(startPosition), groundTile);
    }
    private void GenerateEndPosition()
    {
        endPosition = GenerateValidEndPosition();

        tilemap.SetTile(Vector3Int.FloorToInt(endPosition), endTile);
    }

    private Vector3 GenerateValidEndPosition()
    {
        while (GetDistanceOnGrid(spikePosition, endPosition) < minTilesBetweenTwoPosition ||
               GetDistanceOnGrid(startPosition, endPosition) < minTilesBetweenTwoPosition)
        {
            endPosition = GetRandomPositionFromEdge();
        }

        return endPosition;
    }
    private List<Node> GeneratePath(Vector3 pos1, Vector3 pos2)
    {
        Node startNode = FindNodeAtPosition(pos1);
        Node endNode = FindNodeAtPosition(pos2);

        return AStar.FindRandomPath(startNode, endNode, nodes, mapWidth, mapHeight);
    }
    #endregion
    #region Get Function
    private int GetDistanceOnGrid(Vector3 position1, Vector3 position2)
    {

        int deltaX = Mathf.RoundToInt(position2.x - position1.x);
        int deltaY = Mathf.RoundToInt(position2.y - position1.y);

        int distance = Mathf.Abs(deltaX) + Mathf.Abs(deltaY);

        return distance;
    }


    private Vector3 GetRandomPosition()
    {
        Vector3 randomPositon = new Vector3(Random.Range(0, mapHeight - 1), Random.Range(0, mapWidth - 1));
        return randomPositon;

    }
    private Vector3 GetRandomPositionFromEdge()
    {

        int randomIndex = Random.Range(0, edgePositionList.Count);

        Vector3 randomEdgePosition = edgePositionList[randomIndex];

        return randomEdgePosition;
    }
    public Vector3 GetStartPosition()
    {
        return startPosition;
    }
    #endregion
}
