using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private Animator transition;
    private void Start()
    {
        EventManager.OnPlayerLose += StartTransition;
        EventManager.OnPlayerWin += StartTransition;

        GameViewUI.OnRestart += StartTransition;
        PauseUI.OnReturnMenu += StartTransition;

        WinningTrigger.OnNextLevelLoad += StartTransition;
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerLose -= StartTransition;
        EventManager.OnPlayerWin -= StartTransition;
        
        GameViewUI.OnRestart -= StartTransition;
        PauseUI.OnReturnMenu -= StartTransition;

        WinningTrigger.OnNextLevelLoad -= StartTransition;
    }

    private void StartTransition()
    {
        transition.SetTrigger("Start");
    }
}
