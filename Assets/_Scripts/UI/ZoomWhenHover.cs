using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomWhenHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Assignables
    private RectTransform recTransform;
    //Animation
    [Range(0f, 3f)][SerializeField] private float LargerScale = 1.25f;
    [Range(0f, 1f)][SerializeField] private float AnimationTime = 0.01f;

    private void Awake() =>
        recTransform = GetComponent<RectTransform>();

    public void OnPointerEnter(PointerEventData _eventData) =>
        recTransform.DOScale(Vector3.one * LargerScale, AnimationTime);

    public void OnPointerExit(PointerEventData _eventData) =>
        recTransform.DOScale(Vector3.one, AnimationTime);
}
