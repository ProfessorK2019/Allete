using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private enum State
    {
        ReadyToStart,
        Playing,
    }
    private State state;
    private float readyToStartTimer = 2f;
    private void Awake()
    {
        Instance = this;
        state = State.ReadyToStart;
    }
    private void Update()
    {
        switch (state)
        {
            case State.ReadyToStart:
                readyToStartTimer -= Time.deltaTime;
                if (readyToStartTimer <= 0)
                {
                    state = State.Playing;
                }
                break;
        }
    }
    public bool IsPlaying() => state == State.Playing;
}
