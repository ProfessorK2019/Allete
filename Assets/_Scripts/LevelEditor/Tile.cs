using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Manager SingleTile can be drawn
/// </summary>
public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject highlightTile;
    private SpriteRenderer highlightTileSprite;
    private void Start()
    {
        highlightTileSprite = highlightTile.GetComponent<SpriteRenderer>();

        TileDrawer.Instance.OnTileChange += HandleSelectedTileIndexChanged;
    }

    private void HandleSelectedTileIndexChanged(int index) => highlightTileSprite.sprite = LevelManagerMaster.Instance.tiles[index].Icon;

    void OnMouseEnter()
    {
        highlightTile.SetActive(true);
        //CursorManager.Instance.ResetCursor();
    }

    void OnMouseExit()
    {
        highlightTile.SetActive(false);
        // CursorManager.Instance.ChangCursor();
    }

    void OnDisable() => TileDrawer.Instance.OnTileChange -= HandleSelectedTileIndexChanged;

}
