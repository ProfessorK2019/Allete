using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public static class EventManager
{
    //Level Events
    public static event Action OnPlayerMovesRunOut;
    public static event Action OnPlayerWin;
    public static event Action OnPlayerMove;
    public static event Action OnPlayerBarrier;

    public static async void PlayerMovesRunOut(float seconds, String sceneName)
    {
        OnPlayerMovesRunOut?.Invoke();
        await Task.Delay(TimeSpan.FromSeconds(seconds));
        SceneManager.LoadScene(sceneName);
    }
    public static void PlayerMove() => OnPlayerMove?.Invoke();
    public static void PlayerWin() => OnPlayerWin?.Invoke();

    public static void PlayerBarrier() => OnPlayerBarrier?.Invoke();
}
