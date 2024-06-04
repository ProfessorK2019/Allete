using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem winningExplosion;
    [SerializeField] private ParticleSystem winningParticle;
    [SerializeField] private string nextSceneName;
    public static event Action OnNextLevelLoad;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !GameManager.Instance.IsLosing())
        {

            Destroy(winningParticle);
            winningExplosion.Play();

            if (SceneManager.GetActiveScene().name == "Level 1-7")
            {
                MapUnlockNotification.Instance.Invoke("TriggerMapUnlockNotification", 2f);
            }
            else
            {
                EventManager.PlayerWin();
                LoadNextLevel();
            }
        }
    }
    public async void LoadNextLevel()
    {
        OnNextLevelLoad?.Invoke();
        await Task.Delay(TimeSpan.FromSeconds(LevelManager.GetTimeBeforeRestart()));

        SceneManager.LoadScene(nextSceneName);
    }

}
