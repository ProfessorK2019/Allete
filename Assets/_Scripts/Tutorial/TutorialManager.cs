using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    [SerializeField] private GameObject controlTutorial;
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
        controlTutorial.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }
    public void TriggerDestinationTutorial()
    {
        destinateTutorial.SetActive(true);
    }
    public void TriggerStepTutorial()
    {
        int moveRemain = LevelManager.GetMovesRemaining() + 1;
        LevelManager.SetMovesRemaining(moveRemain);
        stepTutorial.SetActive(true);
        Time.timeScale = 0f;
    }
}
