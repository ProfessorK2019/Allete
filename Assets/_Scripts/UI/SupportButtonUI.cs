using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupportButtonUI : MonoBehaviour
{
    public event Action OnMoveLeftButtonPressed;
    public event Action OnMoveRightButtonPressed;
    public event Action OnZoomOutButtonPressed;

    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button zoomOutButton;
    private void Awake()
    {
        moveLeftButton.onClick.AddListener(() =>
        {
            OnMoveLeftButtonPressed?.Invoke();
        });
        moveRightButton.onClick.AddListener(() =>
        {
            OnMoveRightButtonPressed?.Invoke();
        });
        zoomOutButton.onClick.AddListener(() =>
        {
            OnZoomOutButtonPressed?.Invoke();
        });
    }

}
