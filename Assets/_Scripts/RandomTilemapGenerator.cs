using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomTilemapGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase groundTile;
    [SerializeField] private TileBase endTile;
    [SerializeField] private TileBase pathTile;
    [SerializeField] private TileBase spikeTile;

    [SerializeField] private int mapWidth = 10;
    [SerializeField] private int mapHeight = 10;
    [SerializeField] private int minTilesBetweenTwoPosition = 3;

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
        InitializeEdgePositionList();
        GenerateSpikePosition();
        GenerateStartPosition();
        GenerateEndPosition();
        spikeToEndPath = GeneratePath(spikePosition,endPosition);
        DrawPath(spikeToEndPath);
        spikeToEndPath2 = GeneratePath(spikePosition,endPosition);
        DrawPath(spikeToEndPath);
        spikeToStartPath = GeneratePath(spikePosition,startPosition);
        DrawPath(spikeToStartPath);
    }

    private void InitializeEdgePositionList()
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

        nodes.Clear();
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                bool isWalkable = (tile != endTile);

                nodes.Add(new Node(x, y, isWalkable));
            }
        }
    }

    private List<Node> GeneratePath(Vector3 pos1, Vector3 pos2)
    {
        Node startNode = FindNodeAtPosition(pos1);
        Node endNode = FindNodeAtPosition(pos2);

        return AStar.FindRandomPath(startNode, endNode, nodes, mapWidth, mapHeight);
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

    private void GenerateSpikePosition()
    {
        spikePosition = GetRandomPosition();
        for (int i = edgePositionList.Count - 1; i >= 0; i--)
        {
            Vector3 position = edgePositionList[i];

            if (position == spikePosition)
            {
                edgePositionList.RemoveAt(i);
                break; // Assuming you want to remove only the first occurrence
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
        while (Vector3Int.Distance(Vector3Int.FloorToInt(spikePosition), Vector3Int.FloorToInt(endPosition)) < minTilesBetweenTwoPosition ||
               Vector3Int.Distance(Vector3Int.FloorToInt(startPosition), Vector3Int.FloorToInt(endPosition)) < minTilesBetweenTwoPosition)
        {
            endPosition = GetRandomPositionFromEdge();
        }

        return endPosition; 
    }
    private Vector3 GetRandomPosition()
    {
        Vector3 randomPositon = new Vector3(Random.Range(0, mapHeight - 1), Random.Range(0, mapWidth - 1));
        Debug.Log(randomPositon);
        return randomPositon;

    }
    private Vector3 GetRandomPositionFromEdge()
    {
        if (edgePositionList.Count > 0)
        {
            int randomIndex = Random.Range(0, edgePositionList.Count);
            Vector3 randomPosition = edgePositionList[randomIndex];
            edgePositionList.RemoveAt(randomIndex);
            return randomPosition;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
