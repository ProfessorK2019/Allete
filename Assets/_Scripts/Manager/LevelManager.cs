using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{   
    //Moves
    [Range(0f, 3f)][SerializeField] private float TimeBeforeRestarting = 0.5f;
    [Range(1, 100)][SerializeField] private int PlayerMovesAllowed;
    private static float timeBeforeRestarting;
    private static float playerMovesRemaining;
    //Players
    private static int numberOfPlayers = 1;
    private static bool hasPlayerWon = false;


    private void Awake()
    {
        timeBeforeRestarting = TimeBeforeRestarting;
        playerMovesRemaining = PlayerMovesAllowed;
        numberOfPlayers = 1;
        hasPlayerWon = false;
        EventManager.OnPlayerWin += () => hasPlayerWon = true;
    }
    public static int GetMovesRemaining() =>
        Mathf.Max(0, (int)playerMovesRemaining);

    public static void PlayerMove()
    {
        playerMovesRemaining -= 1 / (float)numberOfPlayers;
        if (playerMovesRemaining > 0 || hasPlayerWon)
            return;
        else PlayerLose();
    }
    public static void PlayerLose()
    {
        EventManager.PlayerMovesRunOut(timeBeforeRestarting, GetSceneName());
    }
    public static void PlayerSplit()
    {
        numberOfPlayers = 2;
    }
    public static void PlayerCombine()
    {
        numberOfPlayers = 1;
    }
    public static String GetSceneName() => SceneManager.GetActiveScene().name;
}

