using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem winningExplosion;
    [SerializeField] private ParticleSystem winningParticle;
    [SerializeField] private string nextLevel;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EventManager.PlayerWin();

            Destroy(winningParticle);
            winningExplosion.Play();
            StartCoroutine(LoadNextLevel());
        }
    }
    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nextLevel);
    }

}
