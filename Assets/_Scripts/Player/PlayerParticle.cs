using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    [SerializeField] GameObject particleEffect;
    private void Start()
    {
        EventManager.OnPlayerCombine += () => Instantiate(particleEffect, transform.position, Quaternion.identity);
    }
}
