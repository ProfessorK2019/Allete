using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using System;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    private Camera _camera;
    private Vector3 targetPosition;
    private Vector3 startingPosition;
    private Vector3 centerPostionBetween2Room;
    private bool isTransitioning = false;
    private Transform cameraLeftTrigger;
    private Transform cameraRightTrigger;
    private Tilemap tilemap;
    [SerializeField] private Grid gridMap;
    [SerializeField] private Vector3Int targetCell;
    [Header("ZOOM SETTINGS")]
    [SerializeField][Range(.1f, 5f)] private float zoomOutOrthoSize;
    [SerializeField][Range(.1f, 5f)] private float zoomInOrthoSize;
    [SerializeField][Range(.1f, 1f)] private float zoomDuration;
    [SerializeField] SupportButtonUI supportButtonUI;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _camera = GetComponent<Camera>();
        cameraLeftTrigger = transform?.GetChild(0);
        cameraRightTrigger = transform?.GetChild(1);
        tilemap = gridMap.GetComponentInChildren<Tilemap>();

        //init required Position
        startingPosition = transform.position;
        targetPosition = tilemap.GetCellCenterWorld(targetCell);
        centerPostionBetween2Room = CalculateCenterPositionBetween2Room();

        CameraTransition.OnCameraTransition += CameraController_OnCameraTransition;

        supportButtonUI.OnMoveLeftButtonPressed += GameViewUI_OnMoveLeftButtonPressed;
        supportButtonUI.OnMoveRightButtonPressed += GameViewUI_OnMoveRightButtonPressed;
        supportButtonUI.OnZoomOutButtonPressed += GameViewUI_OnZoomOutButtonPressed;

    }
    private void GameViewUI_OnMoveLeftButtonPressed() => ZoomIn(startingPosition);
    private void GameViewUI_OnMoveRightButtonPressed() => ZoomIn(targetPosition);
    private void GameViewUI_OnZoomOutButtonPressed() => ZoomOut();

    private void CameraController_OnCameraTransition(Vector3 direction)
    {
        if (!isTransitioning)
        {
            //if direction > 0 zoomPos = targetPos else zoomPos = startingPos
            Vector3 zoomPosition = (direction.x > 0) ? targetPosition : startingPosition;
            ZoomIn(zoomPosition);
        }
    }
    private void MoveTo(Vector3 targetPosition) => transform.DOMove(targetPosition, .5f);
    public Vector3 CalculateCenterPositionBetween2Room()
    {
        return new Vector3((startingPosition.x + targetPosition.x) / 2,
                            (startingPosition.y + targetPosition.y) / 2,
                            transform.position.z);
    }
    public void ZoomIn(Vector3 targetPosition)
    {
        HandleZoom();

        var moveTween = transform.DOMove(targetPosition, zoomDuration).SetEase(Ease.InOutExpo);
        var orthoSizeTween = _camera.DOOrthoSize(zoomInOrthoSize, zoomDuration).SetEase(Ease.InOutExpo);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(moveTween).Join(orthoSizeTween);
        sequence.OnComplete(() =>
        {
            isTransitioning = false;
            ReActiveTriggers();
        });
    }
    public void ZoomOut()
    {
        HandleZoom();

        var moveTween = transform.DOMove(centerPostionBetween2Room, zoomDuration).SetEase(Ease.InOutExpo);
        var orthoSizeTween = _camera.DOOrthoSize(zoomOutOrthoSize, zoomDuration).SetEase(Ease.InOutExpo);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(moveTween).Join(orthoSizeTween);
        sequence.OnComplete(() => isTransitioning = false);
    }
    private void HandleZoom()
    {
        isTransitioning = true;
        cameraLeftTrigger.gameObject.SetActive(false);
        cameraRightTrigger.gameObject.SetActive(false);
    }
    private void ReActiveTriggers()
    {
        cameraLeftTrigger.gameObject.SetActive(true);
        cameraRightTrigger.gameObject.SetActive(true);
    }
    public Vector3 SetTargetPosition(Vector3 targetPosition)
    {
        Vector3Int gridPos = gridMap.WorldToCell(targetPosition);
        return tilemap.GetCellCenterWorld(gridPos);
    }
    void OnDestroy()
    {
        CameraTransition.OnCameraTransition -= CameraController_OnCameraTransition;
        
        supportButtonUI.OnMoveLeftButtonPressed -= GameViewUI_OnMoveLeftButtonPressed;
        supportButtonUI.OnMoveRightButtonPressed -= GameViewUI_OnMoveRightButtonPressed;
        supportButtonUI.OnZoomOutButtonPressed -= GameViewUI_OnZoomOutButtonPressed;
    }
}
