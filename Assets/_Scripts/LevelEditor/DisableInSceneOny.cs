using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableInSceneOny : MonoBehaviour
{
    private string targetSceneName = "LevelEditor";
    private BigPlayerController playerScript;

    private void Start()
    {      
        playerScript = GetComponent<BigPlayerController>();
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == targetSceneName)
        {   
            playerScript.enabled = false;
        }
    }
}
