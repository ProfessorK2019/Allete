using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerateEditor : EditorWindow
{
    private Tilemap selectedTilemap;

    [MenuItem("Tools/Map Generator")]
    public static void ShowWindow()
    {
        GetWindow<MapGenerateEditor>("Map Generator");
    }

    void OnGUI()
    {
        GUILayout.Label("Map Generator", EditorStyles.boldLabel);

        // Chọn Tilemap
        selectedTilemap = EditorGUILayout.ObjectField("Tilemap:", selectedTilemap, typeof(Tilemap), true) as Tilemap;

        // Kiểm tra xem Tilemap có được chọn không
        if (selectedTilemap.GetTilesBlock(selectedTilemap.cellBounds) != null)
        {
            GUILayout.Label("Tiles in Tilemap:", EditorStyles.boldLabel);
            foreach (TileBase tile in selectedTilemap.GetTilesBlock(selectedTilemap.cellBounds))
            {
                if (tile != null)
                {
                    GUILayout.Label(tile.name);
                }
                else
                {
                    GUILayout.Label("Null Tile");
                }
            }
        }
        else
        {
            GUILayout.Label("No Tilemap selected.", EditorStyles.boldLabel);
        }
    }
}
