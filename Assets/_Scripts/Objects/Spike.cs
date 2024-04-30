using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spike : MonoBehaviour
{
    private Transform[] spawnPlayerPos = new Transform[2];
    [SerializeField] private GameObject smallPlayerPrefab;
    private void Start()
    {
        spawnPlayerPos[0] = transform.Find("SpawnPoint1");
        spawnPlayerPos[1] = transform.Find("SpawnPoint2");

        spawnPlayerPos[0].transform.position = GridManager.Instance.GetGridCenterPosition(spawnPlayerPos[0].position);
        spawnPlayerPos[1].transform.position = GridManager.Instance.GetGridCenterPosition(spawnPlayerPos[1].position);

        SetZPosition(spawnPlayerPos[0]);
        SetZPosition(spawnPlayerPos[1]);

    }
    public void CreateSmallPlayers()
    {

        GameObject smallPlayer1 = Instantiate(smallPlayerPrefab, transform.position, Quaternion.identity);
        GameObject smallPlayer2 = Instantiate(smallPlayerPrefab, transform.position, Quaternion.identity);

        //dotween JumpAnimation
        smallPlayer1.transform.DOJump(spawnPlayerPos[0].position, 0.3f, 1, 0.3f)
                .SetEase(Ease.OutCubic);
        smallPlayer2.transform.DOJump(spawnPlayerPos[1].position, 0.3f, 1, 0.3f)
                .SetEase(Ease.OutCubic);
    }
    void SetZPosition(Transform transform)
    {
        Vector3 newPosition = transform.position;
        newPosition.z = 0;
        transform.position = newPosition;
    }
}
