using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New TileSO")]
public class TileSO : ScriptableObject
{
    public TileBase tile;
    public string id;
    public Sprite Icon;
}
