using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Moves
    [Range(0f, 3f)][SerializeField] private float TimeBeforeRestarting = 0.5f;
    [Range(1, 100)][SerializeField] private int PlayerMovesAllowed;
    [SerializeField] private int currentLevelIndex;
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

        EventManager.OnPlayerMove += UpdatePlayerStep;
        EventManager.OnPlayerBarrier += UpdatePlayerStep;
        EventManager.OnPlayerWin += () => hasPlayerWon = true;

        EventManager.OnPlayerCombine += () => numberOfPlayers = 1;
        EventManager.OnPlayerSplit += () => numberOfPlayers = 2;
    }
    void Start()
    {
        EventManager.OnPlayerWin += EventManager_OnPlayerWin;
    }

    private void EventManager_OnPlayerWin()
    {
        CompleteLevel();
    }

    private void OnEnable()
    {
        RandomMapManager.OnRandomDone += RandomMapManager_OnRandomDone;
    }

    private void OnDisable()
    {
        RandomMapManager.OnRandomDone -= RandomMapManager_OnRandomDone;
    }

    public void CompleteLevel()
    {
        PlayerPrefs.SetInt("Level" + currentLevelIndex, 1);
        PlayerPrefs.SetInt("LastCompletedLevel", currentLevelIndex);
        PlayerPrefs.Save();

    }
    private void RandomMapManager_OnRandomDone(int step)
    {
        PlayerMovesAllowed = step;
        playerMovesRemaining = PlayerMovesAllowed;
    }

    private static void UpdatePlayerStep()
    {
        if (hasPlayerWon) return;

        playerMovesRemaining -= 1 / (float)numberOfPlayers;
        if (playerMovesRemaining > 0) return;
        EventManager.PlayerLose(timeBeforeRestarting, GetSceneName());
    }
    public static int GetMovesRemaining() => Mathf.Max(0, (int)playerMovesRemaining);
    public static void SetMovesRemaining(int moves) => playerMovesRemaining = Mathf.Max(0, moves);
    public static String GetSceneName() => SceneManager.GetActiveScene().name;
    public static float GetTimeBeforeRestart() => timeBeforeRestarting;
    private void OnDestroy()
    {
        EventManager.OnPlayerMove -= UpdatePlayerStep;
        EventManager.OnPlayerBarrier -= UpdatePlayerStep;
    }
}

