using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    [SerializeField] private float dragSpeed = 10;
    private Vector3 dragOffset;
    private Camera cam;
    private Transform parentTransform;
    private BoxCollider2D col;
    private Vector3 initPosition;

    void Awake()
    {
        cam = Camera.main;
        parentTransform = transform.parent;
        col = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        initPosition = transform.position;
    }
    void OnMouseDown()
    {
        dragOffset = transform.position - GetMousePos();
    }

    void OnMouseDrag()
    {
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + dragOffset, dragSpeed * Time.deltaTime);
    }

    void OnMouseUp()
    {
        col.enabled = false;
        Vector3Int parentGridPos = GridManager.Instance.GetGridPosition(parentTransform.position);

        Vector3Int mouseGridPos = GridManager.Instance.GetGridPosition(GetMousePos());
        mouseGridPos.z = 0;

        foreach (Vector3Int validPos in CheckIfHasTileSurround(parentGridPos))
        {
            if (validPos == mouseGridPos && !LevelManagerMaster.Instance.GetObstacleAtPosition(mouseGridPos))// can place at validPos
            {
                Vector3 mouseCenterGridPos = GridManager.Instance.GetGridCenterPosition(GetMousePos());
                ResetZPosition(ref mouseCenterGridPos);

                transform.position = mouseCenterGridPos;
                initPosition = transform.position;//change initPos to mousePos

                break;
            }
            else transform.position = initPosition;//comeback to initPos if cant place 
        }
        col.enabled = true; 
    }

    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        ResetZPosition(ref mousePos);
        return mousePos;
    }
    private List<Vector3Int> CheckIfHasTileSurround(Vector3Int pos)
    {
        Vector3Int[] directions = { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };
        List<Vector3Int> validPos = new List<Vector3Int>();
        foreach (Vector3Int direction in directions)
        {
            Vector3Int targetPos = pos + direction;

            if (GridManager.Instance.GetTileMap().GetTile(targetPos) != null)
            {

                validPos.Add(targetPos);

            }
        }
        return validPos;
    }
    void ResetZPosition(ref Vector3 pos) => pos.z = 0;
}
