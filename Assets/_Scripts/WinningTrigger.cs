using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem winningExplosion;
    [SerializeField] private ParticleSystem winningParticle;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("win");
            Destroy(winningParticle);
            winningExplosion.Play();
        }
    }
}
