using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CameraTransition : MonoBehaviour
{
    public static event Action<Vector3> OnCameraTransition;
    [SerializeField] private Vector3 direction;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            OnCameraTransition?.Invoke(direction);
            GameManager.Instance.ChangeState(GameManager.State.ReadyToStart);
        }
    }
}
