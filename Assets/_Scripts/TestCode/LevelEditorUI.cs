using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorUI : MonoBehaviour
{
    [SerializeField] private Image currentTile;

    void Start()
    {
        TileDrawer.Instance.OnTileChange += HandleSelectedTileIndexChanged;
    }

    private void HandleSelectedTileIndexChanged(int index)
    {
        Debug.Log(LevelManagerMaster.Instance.tiles[index].name);
        currentTile.sprite = LevelManagerMaster.Instance.tiles[index].Icon;
    }
    void OnDisable() => TileDrawer.Instance.OnTileChange -= HandleSelectedTileIndexChanged;
}
