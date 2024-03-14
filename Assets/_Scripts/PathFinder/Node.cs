using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int x;
    public int y;
    public bool isWalkable;
    public float gCost;
    public float hCost;
    public Node parent;

    public float fCost { get { return gCost + hCost; } }

    public Node(int x, int y, bool isWalkable)
    {
        this.x = x;
        this.y = y;
        this.isWalkable = isWalkable;
    }
}
