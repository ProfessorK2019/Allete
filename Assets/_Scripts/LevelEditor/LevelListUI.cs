using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelListUI : MonoBehaviour
{
    public static event Action OnStartCustomLevel;
    [SerializeField] private Transform container;
    [SerializeField] private Transform templatePrefab;
    [SerializeField] private GameObject gameViewUI;
    private bool isShowing = false;
    private void Start()
    {
        Hide();
        LoadLevelButtons();

        SaveLevelUI.OnNewLevelSave += SaveLevelUI_OnNewLevelSave;
    }

    private void SaveLevelUI_OnNewLevelSave()
    {
        LoadLevelButtons();
    }

    private void LoadLevelButtons()
    {
        List<string> savedLevels = GetSavedLevelFiles();

        foreach (string levelName in savedLevels)
        {
            Transform template = Instantiate(templatePrefab, container);
            template.GetComponentInChildren<TextMeshProUGUI>().text = levelName;
            template.GetComponent<Button>().onClick.AddListener(() => OnCustomLevelButtonLoad(levelName));

            Button deleteButton = template.transform.Find("DeleteButton").GetComponent<Button>();
            deleteButton.onClick.AddListener(() => OnDeleteButtonClicked(levelName, template));
        }
    }
    private List<string> GetSavedLevelFiles()
    {
        string path = Application.dataPath + "/Levels";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string[] files = Directory.GetFiles(path, "*.json");
        List<string> fileNames = new List<string>();
        foreach (string file in files)
        {
            fileNames.Add(Path.GetFileNameWithoutExtension(file));
        }
        return fileNames;
    }
    private void OnDeleteButtonClicked(string levelName, Transform template)
    {
        string path = Application.dataPath + "/Levels/" + levelName + ".json";
        if (File.Exists(path))
        {
            File.Delete(path);
            template.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Level file not found: " + path);
        }
    }
    private async void OnCustomLevelButtonLoad(string levelName)
    {
        OnStartCustomLevel?.Invoke();
        await Task.Delay(TimeSpan.FromSeconds(1f));
        gameViewUI.SetActive(true);
        LevelManagerMaster.Instance.LoadLevel(levelName);
        gameObject.SetActive(false);
    }
    public void ToggleUI()
    {
        if (!isShowing)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        gameObject.SetActive(true);
        isShowing = true;
    }
    private void Hide()
    {
        gameObject.SetActive(false);
        isShowing = false;
    }
    void OnDestroy()
    {
        SaveLevelUI.OnNewLevelSave -= SaveLevelUI_OnNewLevelSave;
    }
}
