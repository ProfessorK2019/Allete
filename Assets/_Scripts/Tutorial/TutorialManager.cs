using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    [SerializeField] private GameObject destinateTutorial;

    [SerializeField] private GameObject stepTutorial;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Invoke("StartTutorial", 1.25f);
    }
    private void StartTutorial()
    {
        destinateTutorial.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }
    public void TriggerStepTutorial()
    {
        int moveRemain = LevelManager.GetMovesRemaining() + 1;
        LevelManager.SetMovesRemaining(moveRemain);
        stepTutorial.SetActive(true);
        Time.timeScale = 0f;
    }
}
