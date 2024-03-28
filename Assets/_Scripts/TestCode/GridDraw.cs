using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Draw gridmap background for drawing Tile
/// </summary>

public class GridDraw : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tile prefab;
    private static List<Vector3> drawablePosList = new List<Vector3>();
    private void Start()
    {
        DrawGridMap();
    }

    void DrawGridMap()
    {
        float x = 0, y = 0;
        float xOffset = 0.6f, yOffset = 0.35f;

        for (int row = 0; row < width; row++)
        {
            x += xOffset * row;
            y -= yOffset * row;
            for (int col = 0; col < height; col++)
            {
                Vector3 position = new Vector3(x, y, 0);
                Instantiate(prefab, position, Quaternion.identity, transform);
                drawablePosList.Add(position);
                x -= xOffset;
                y -= yOffset;
            }
            x = 0; y = 0;
        }
    }
    public static List<Vector3> GetDrawablePosList() => drawablePosList;
}
