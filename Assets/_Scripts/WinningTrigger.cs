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
    public static event Action OnNextLevelLoad;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !GameManager.Instance.IsLosing())
        {
            EventManager.PlayerWin();

            Destroy(winningParticle);
            winningExplosion.Play();
            LoadNextLevel();
        }
    }
    public async void LoadNextLevel()
    {
        OnNextLevelLoad?.Invoke();
        await Task.Delay(TimeSpan.FromSeconds(LevelManager.GetTimeBeforeRestart()));

        Scene currentScene = SceneManager.GetActiveScene();
        int sceneIndex = currentScene.buildIndex;

        SceneManager.LoadScene(sceneIndex + 1);
    }

}
