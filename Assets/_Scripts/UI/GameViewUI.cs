using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;

public class GameViewUI : MonoBehaviour
{   
    public static event Action OnRestart;
    [SerializeField] private TextMeshProUGUI numberOfStepsTxt;

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
        SceneManager.LoadScene(LevelManager.GetSceneName());
    }
}
