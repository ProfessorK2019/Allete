using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public static class EventManager
{
    //Scene Events
    public static event Action OnSceneLoadStart;
    public static event Action OnSceneLoadEnd;

    public static void SceneLoadStart() =>
        OnSceneLoadStart?.Invoke();

    public static void SceneLoadEnd() =>
        OnSceneLoadEnd?.Invoke();

    //Level Events
    public static event Action OnPlayerMovesRunOut;
    public static event Action OnPlayerWin;

    public static async void PlayerMovesRunOut(float seconds, String sceneName)
    {
        OnPlayerMovesRunOut?.Invoke();
        await Task.Delay(TimeSpan.FromSeconds(seconds));
        SceneManager.LoadScene(sceneName);
    }

    public static void PlayerWin() => OnPlayerWin?.Invoke();

    public static event Action OnPlayerBarrier;

    public static void PlayerBarrier() =>
        OnPlayerBarrier?.Invoke();
}
