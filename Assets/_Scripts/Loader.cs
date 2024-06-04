using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        LoadingScene,
    }
    private static string targetScene;
    public static void Load(string scene)
    {
        targetScene = scene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());

    }
    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene);
    }
}
