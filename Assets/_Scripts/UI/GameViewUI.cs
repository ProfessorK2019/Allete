using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;

public class GameViewUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberOfStepsTxt;
    [SerializeField] private Image image;
    public static event Action OnRestart;

    private void Start()
    {   
        numberOfStepsTxt.text = LevelManager.GetMovesRemaining().ToString();
    }

    private void Update()
    {
        numberOfStepsTxt.text = LevelManager.GetMovesRemaining().ToString();
    }
    public async void ResetLevel()
    {   
        OnRestart?.Invoke();
        await Task.Delay(TimeSpan.FromSeconds(LevelManager.GetTimeBeforeRestart()));
        // AsyncUtilities.Invoke(this, OnRestart, LevelManager.GetTimeBeforeRestart());
        SceneManager.LoadScene(LevelManager.GetSceneName());
    }
}