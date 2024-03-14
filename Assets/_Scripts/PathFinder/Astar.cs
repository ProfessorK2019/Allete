using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AStar
{
    public static List<Node> FindRandomPath(Node startNode, Node endNode, List<Node> nodes, int width, int height)
    {
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[Random.Range(0, openSet.Count)];

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }

            List<Node> neighbors = GetNeighbors(currentNode, nodes, width, height);

            foreach (Node neighbor in neighbors)
            {
                if (!neighbor.isWalkable || closedSet.Contains(neighbor))
                {
                    continue;
                }

                float newCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);

                if (newCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, endNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }

    private static List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    private static float GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.x - nodeB.x);
        int dstY = Mathf.Abs(nodeA.y - nodeB.y);

        return dstX + dstY + Random.Range(0.0f, 1.0f);
    }

    private static List<Node> GetNeighbors(Node node, List<Node> nodes, int width, int height)
    {
        List<Node> neighbors = new List<Node>();

        int[] xOffsets = { 0, 0, 1, -1 };
        int[] yOffsets = { 1, -1, 0, 0 };

        for (int i = 0; i < xOffsets.Length; i++)
        {
            int checkX = node.x + xOffsets[i];
            int checkY = node.y + yOffsets[i];

            if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
            {
                Node neighbor = nodes.Find(n => n.x == checkX && n.y == checkY);
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }
}
