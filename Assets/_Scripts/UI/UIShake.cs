using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class UIShake : MonoBehaviour
{
    [SerializeField] private Transform Transform;

    //Animation
    [Range(0f, 5f)][SerializeField] private float ShakeStrength;
    [Range(0f, 3f)][SerializeField] private float AnimationTime;
    private void Start()
    {
        EventManager.OnPlayerLose += OnPlayerMove_Shake;
    }
    
    private void OnPlayerMove_Shake()
    {
        Transform.DOShakePosition(AnimationTime, ShakeStrength);
        Transform.DOShakeRotation(AnimationTime, ShakeStrength);
    }
}
