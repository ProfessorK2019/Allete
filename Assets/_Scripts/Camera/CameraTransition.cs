using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CameraTransition : MonoBehaviour
{
    public static event Action<Vector3> OnCameraTransition;
    [SerializeField] private Vector3 direction;
    private static bool isTransitioning = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!isTransitioning)
            {
                isTransitioning = true;
                OnCameraTransition?.Invoke(direction);
                GameManager.Instance.ChangeState(GameManager.State.ReadyToStart);
                StartCoroutine(ResetIsTransitioning());
            }
        }
    }
    private IEnumerator ResetIsTransitioning()
    {
        yield return new WaitForSeconds(0.5f);
        isTransitioning = false;
    }
}
