using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowInSceneOnly : MonoBehaviour
{
    private string targetSceneName = "LevelEditor";

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != targetSceneName)
        {   
            gameObject.SetActive(false);
        }
    }
}