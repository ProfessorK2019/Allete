using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameViewUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberOfStepsTxt;
    void Start()
    {
        numberOfStepsTxt.text = LevelManager.GetMovesRemaining().ToString();
    }
    void Update()
    {
        numberOfStepsTxt.text = LevelManager.GetMovesRemaining().ToString();
    }
}
