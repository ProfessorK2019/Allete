using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ObstacleSO")]
public class ObstacleSO : ScriptableObject
{
    public GameObject obstacle;
    public string id;
    public int quantity = 2;
}
