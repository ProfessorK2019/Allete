using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameViewUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberOfStepsTxt;
    [SerializeField] private Image image;

    private void Start()
    {   
        numberOfStepsTxt.text = LevelManager.GetMovesRemaining().ToString();
    }

    private void Update()
    {
        numberOfStepsTxt.text = LevelManager.GetMovesRemaining().ToString();
    }
    public void ResetLevel()
    {
        SceneManager.LoadScene(LevelManager.GetSceneName());
    }
}
