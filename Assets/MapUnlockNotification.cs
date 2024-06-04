using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MapUnlockNotification : MonoBehaviour
{
    public static MapUnlockNotification Instance;
    private void Awake()
    {
        Instance = this;
        transform.localScale = Vector3.zero;

    }
    public void TriggerMapUnlockNotification()
    {
        transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).SetEase(Ease.OutBack);
    }
    public void LoadMap2()
    {
        EventManager.PlayerWin();
        Loader.Load("Level 2-1");
    }
}
