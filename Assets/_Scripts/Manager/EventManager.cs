using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public static class EventManager
{
    //Level Events
    public static event Action OnPlayerLose;
    public static event Action OnPlayerWin;
    public static event Action OnPlayerMove;
    public static event Action OnPlayerBarrier;
    public static event Action OnPlayerCombine;
    public static event Action OnPlayerSplit;

    public static async void PlayerLose(float seconds, String sceneName)
    {
        OnPlayerLose?.Invoke();
        GameManager.Instance.ChangeState(GameManager.State.GameOver);//change state so that player cant move before transition
        await Task.Delay(TimeSpan.FromSeconds(seconds));//wait some delay for transition
        SceneManager.LoadScene(sceneName);
    }
    public static void PlayerMove() => OnPlayerMove?.Invoke();
    public static void PlayerWin() => OnPlayerWin?.Invoke();
    public static void PlayerBarrier() => OnPlayerBarrier?.Invoke();
    public static void PlayerCombine() => OnPlayerCombine?.Invoke();
    public static void PlayerSplit() => OnPlayerSplit?.Invoke();
}
