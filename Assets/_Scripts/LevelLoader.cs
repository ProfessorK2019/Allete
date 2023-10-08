using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    private void Start()
    {
        EventManager.OnPlayerMovesRunOut += StartTransition;
        EventManager.OnPlayerWin += StartTransition;
        GameViewUI.OnRestart += StartTransition;
        WinningTrigger.OnNextLevelLoad += StartTransition;
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerMovesRunOut -= StartTransition;
        EventManager.OnPlayerWin -= StartTransition;
        GameViewUI.OnRestart -= StartTransition;
        WinningTrigger.OnNextLevelLoad -= StartTransition;
    }

    private void StartTransition()
    {
        transition.SetTrigger("Start");
    }
}
