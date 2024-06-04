using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    private int numberOfLevelsRequired = 7;
    [SerializeField] private TextMeshProUGUI map1Progress;
    [SerializeField] private TextMeshProUGUI map2Progress;
    [SerializeField] private GameObject locked;

    private void Start()
    {
        SetProgressText();
    }
    private void SetProgressText()
    {
        int lastCompletedLevel = PlayerPrefs.GetInt("LastCompletedLevel", 0);
        if (lastCompletedLevel < 8)
        {
            map1Progress.text = lastCompletedLevel.ToString() + "/7";
        }
        else if (lastCompletedLevel >= 8)
        {
            int progressToSet = lastCompletedLevel - 7;
            map1Progress.text = "7/7";
            map2Progress.text = progressToSet.ToString() + "/7";
        }
    }
    public void CheckProgress()
    {
        int completedLevels = 0;

        for (int i = 1; i <= numberOfLevelsRequired; i++)
        {
            if (PlayerPrefs.HasKey("Level" + i) && PlayerPrefs.GetInt("Level" + i) == 1)
            {
                completedLevels++;
            }
        }

        if (completedLevels >= numberOfLevelsRequired)
        {
            locked.SetActive(false);
        }
        else
        {
            //Player hasnt finish first 7 level
        }
    }
    public void LoadMap2()
    {
        Loader.Load("Level 2-1");
    }
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}
