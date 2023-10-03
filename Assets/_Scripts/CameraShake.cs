using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    public float duration = 0.5f;
    public float strength = 1.0f;
    public static CameraShake Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void Shake()
    {
        cameraTransform.DOShakePosition(duration, strength);
    }
}
