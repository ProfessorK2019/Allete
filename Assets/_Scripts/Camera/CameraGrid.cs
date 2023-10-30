using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using System;
using UnityEngine.InputSystem;

public class CameraGrid : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Vector3Int targetCell;
    private Vector3 startingPosition;
    private void Start()
    {
        startingPosition = transform.position;
        CameraTransition.OnCameraTransition += CameraGrid_OnCameraTransition;
    }

    private void CameraGrid_OnCameraTransition(Vector3 direction)
    {
        if (direction.x > 0)
        {
            MoveToTargetTile();
        }
        else
        {
            ReturnPreviousTile();
        }
    }

    public void MoveToTargetTile()
    {
        // Lấy vị trí Grid Position tương ứng
        Vector3 targetPosition = tilemap.GetCellCenterWorld(targetCell);

        transform.DOMove(targetPosition, .5f);
    }
    public void ReturnPreviousTile()
    {
        transform.DOMove(startingPosition, .5f);
    }
}
