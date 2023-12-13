using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum State
    {
        ReadyToStart,
        Playing,
        GameOver
    }
    private State state;
    private float readyToStartTimer = 1f;
    private float currentTimer;
    private void Awake()
    {
        Instance = this;
        state = State.ReadyToStart;
        currentTimer = readyToStartTimer;
    }
    private void Update()
    {
        switch (state)
        {
            case State.ReadyToStart:
                currentTimer -= Time.deltaTime;
                if (currentTimer <= 0)
                {
                    ChangeState(State.Playing);
                }
                break;
        }
    }
    public bool IsPlaying() => state == State.Playing;
    public void ChangeState(State newState)
    {
        currentTimer = readyToStartTimer;
        state = newState;
    }
    public bool IsLosing() => state == State.GameOver;
}
