using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;


public class BigPlayerController : Player
{
    private void Start()
    {
         InitStartPosition();
    }
    private void InitStartPosition()
    => transform.position = GridManager.Instance.GetGridCenterPosition(transform.position);
    protected override void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.TryGetComponent<Spike>(out Spike spike))
        {   
            EventManager.PlayerSplit();
            GameManager.Instance.ChangeState(GameManager.State.ReadyToStart);
            SpawnParticle();
            spike.CreateSmallPlayers();
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 rayDirection = targetPosition - transform.position;
        Gizmos.DrawLine(rayCastStartPoint.transform.position, transform.position + rayDirection);
    }
}
