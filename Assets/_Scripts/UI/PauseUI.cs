using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public static event Action OnReturnMenu;
    [SerializeField] private float popupDuration = 0.5f;
    [SerializeField] private Ease popupEase = Ease.OutBack;
    private bool isPopupVisible = false;

    private void Start()
    {
        // Start with the scale set to zero
        transform.localScale = Vector3.zero;
    }
    public void TogglePopup()
    {
        if (isPopupVisible)
        {
            HidePopup();
        }
        else
        {
            ShowPopup();
        }
    }
    public void ShowPopup()
    {
        isPopupVisible = true;
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), popupDuration).SetEase(popupEase);
    }

    public void HidePopup()
    {
        isPopupVisible = false;
        transform.DOScale(Vector3.zero, popupDuration).SetEase(popupEase);
    }
    public async void ReturnMainMenu()
    {
        OnReturnMenu?.Invoke();
        await Task.Delay(TimeSpan.FromSeconds(LevelManager.GetTimeBeforeRestart()));
        Loader.Load("MainMenu");
    }
}
