using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomMenuUI : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void TriggerAnimator()
    {
        anim.SetTrigger("Fade");
    }
}
