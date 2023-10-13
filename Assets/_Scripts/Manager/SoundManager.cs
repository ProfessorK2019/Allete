using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioRefSO audioRefSO;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        EventManager.OnPlayerMove += EventManager_OnPlayerMove;
        EventManager.OnPlayerBarrier += EventManager_OnPlayerBarrier;

        EventManager.OnPlayerLose += EventManager_OnPlayerLose;
        EventManager.OnPlayerWin += EventManager_OnPlayerWin;

        EventManager.OnPlayerCombine += EventManager_OnPlayerCombine;
        EventManager.OnPlayerSplit += EventManager_OnPlayerSplit;

        GameViewUI.OnRestart += GameViewUI_OnRestart;
    }

    private void PlaySound(AudioClip audioClip, float volume = 1f)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    private void EventManager_OnPlayerWin() => PlaySound(audioRefSO.playerWin);
    private void EventManager_OnPlayerLose() => PlaySound(audioRefSO.playerLose);

    private void EventManager_OnPlayerSplit() => PlaySound(audioRefSO.playerCombine);
    private void EventManager_OnPlayerCombine() => PlaySound(audioRefSO.playerSplit);

    private void EventManager_OnPlayerBarrier() => PlaySound(audioRefSO.barrier);
    private void EventManager_OnPlayerMove() => PlaySound(audioRefSO.move);
    
    private void GameViewUI_OnRestart() => PlaySound(audioRefSO.buttonClick);

    void OnDestroy()
    {
        EventManager.OnPlayerMove -= EventManager_OnPlayerMove;
        EventManager.OnPlayerBarrier -= EventManager_OnPlayerBarrier;

        EventManager.OnPlayerLose -= EventManager_OnPlayerLose;
        EventManager.OnPlayerWin -= EventManager_OnPlayerWin;

        EventManager.OnPlayerCombine -= EventManager_OnPlayerCombine;
        EventManager.OnPlayerSplit -= EventManager_OnPlayerSplit;
    }
}
