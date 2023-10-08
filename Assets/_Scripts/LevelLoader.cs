using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]private Animator transition;
    private void Start()
    {
        EventManager.OnPlayerMovesRunOut += OnPlayerMovesRunOut_Transition;
    }

    private void OnPlayerMovesRunOut_Transition()
    {
       transition.SetTrigger("Start");
    }
    void OnDestroy()
    {
        EventManager.OnPlayerMovesRunOut -= OnPlayerMovesRunOut_Transition;
    }
}
