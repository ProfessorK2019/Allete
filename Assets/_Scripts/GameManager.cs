using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int numberOfSteps = 5;
    private void Awake()
    {
        Instance = this;
    }
}
