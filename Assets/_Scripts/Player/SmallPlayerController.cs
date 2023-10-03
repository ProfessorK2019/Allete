using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SmallPlayerController : Player
{
    [SerializeField] private GameObject bigPlayerPrefab;

    protected override void OnTriggerEnter2D(Collider2D other)
    {

        if (other.TryGetComponent<SmallPlayerController>(out SmallPlayerController smallPlayerController))
        {
            jumpTween.Kill();
            Destroy(gameObject);
            if (gameObject.GetInstanceID() > smallPlayerController.gameObject.GetInstanceID())
            {
                transform.position = GridManager.Instance.GetGridCenterPosition(transform.position);
                Instantiate(bigPlayerPrefab, transform.position, Quaternion.identity);
                LevelManager.PlayerCombine();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 rayDirection = targetPosition - transform.position;
        Gizmos.DrawLine(rayCastStartPoint.transform.position, transform.position + rayDirection);
    }
}
