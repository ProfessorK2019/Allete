using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;
    [SerializeField] private Texture2D blockCursor;
    [SerializeField] private Texture2D defaultCursor;

    private Vector2 cursorHotpot;

    private void Awake()
    {
        Instance = this;
    }
    public void ChangCursor()
    {
        cursorHotpot = new Vector2(blockCursor.width / 2, blockCursor.height / 2);
        Cursor.SetCursor(blockCursor, cursorHotpot, CursorMode.Auto);
    }
    public void ResetCursor()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
