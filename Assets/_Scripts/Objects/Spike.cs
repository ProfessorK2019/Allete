using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spike : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPlayerPos;
    [SerializeField] private GameObject smallPlayerPrefab;
    public void CreateSmallPlayers()
    {
        
        GameObject smallPlayer1 = Instantiate(smallPlayerPrefab, transform.position, Quaternion.identity);
        GameObject smallPlayer2 = Instantiate(smallPlayerPrefab, transform.position, Quaternion.identity);

        //dotween JumpAnimation
        smallPlayer1.transform.DOJump(GridManager.Instance.GetGridCenterPosition(spawnPlayerPos[0].position), 0.3f, 1, 0.3f)
                .SetEase(Ease.OutCubic);
        smallPlayer2.transform.DOJump(GridManager.Instance.GetGridCenterPosition(spawnPlayerPos[1].position), 0.3f, 1, 0.3f)
                .SetEase(Ease.OutCubic);
    }
}
