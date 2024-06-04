using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLevelUI : MonoBehaviour
{
    public static event Action OnNewLevelSave;
    [SerializeField] private TMP_InputField name_IF;
    [SerializeField] private TMP_InputField step_IF;

    public void SaveLevel()
    {
        if (name_IF.text == "") name_IF.text = "UnknownLevel";
        if (step_IF.text == "") step_IF.text = "99";

        int step = int.Parse(step_IF.text);
        LevelManagerMaster.Instance.SaveLevel(name_IF.text, step);
        OnNewLevelSave?.Invoke();
    }
}
