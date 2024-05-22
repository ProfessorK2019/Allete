using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapManager : MonoBehaviour
{
    public static event Action<int> OnRandomDone;
    [SerializeField] private Transform baseMap;
    [SerializeField] private GameObject[] mapAList;
    [SerializeField] private GameObject[] mapBList;
    private void Start()
    {
        SpawnRandomMaps();
    }
    public void SpawnRandomMaps()
    {
        ClearOldMaps();
        
        GridManager.Instance.UpdateTileMaps();

        GameObject randomMap1 = mapAList[UnityEngine.Random.Range(0, mapAList.Length)];
        MapData map1Data = randomMap1.GetComponent<MapData>();
        Instantiate(randomMap1, baseMap);

        GameObject randomMap2 = mapBList[UnityEngine.Random.Range(0, mapBList.Length)];
        MapData map2Data = randomMap2.GetComponent<MapData>();
        Instantiate(randomMap2, baseMap);

        int baseMapStep = map1Data.GetNumberOfStep() + map2Data.GetNumberOfStep();
        OnRandomDone?.Invoke(baseMapStep);

        GridManager.Instance.UpdateTileMaps();
    }
    private void ClearOldMaps()
    {
        foreach (Transform child in baseMap)
        {
            Destroy(child.gameObject);
        }
    }
}
