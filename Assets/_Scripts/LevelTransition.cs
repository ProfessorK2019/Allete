using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private Animator transition;
    private void Start()
    {
        EventManager.OnPlayerLose += StartTransition;
        EventManager.OnPlayerWin += StartTransition;

        GameViewUI.OnRestart += StartTransition;
        WinningTrigger.OnNextLevelLoad += StartTransition;
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerLose -= StartTransition;
        EventManager.OnPlayerWin -= StartTransition;
        
        GameViewUI.OnRestart -= StartTransition;
        WinningTrigger.OnNextLevelLoad -= StartTransition;
    }

    private void StartTransition()
    {
        transition.SetTrigger("Start");
    }
}
