using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    [SerializeField] private int numberOfStep;
    public int GetNumberOfStep() => numberOfStep;
}
