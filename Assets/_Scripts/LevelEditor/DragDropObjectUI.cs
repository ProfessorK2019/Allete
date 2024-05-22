using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragDropObjectUI : MonoBehaviour
{
    public static DragDropObjectUI Instance; // Singleton instance

    [SerializeField] private TextMeshProUGUI spikeQuantityText;
    [SerializeField] private TextMeshProUGUI playerQuantityText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateUI(Dictionary<string, int> obstacleQuantities)
    {
        string spikeId = "Spike";
        string playerId = "Player";
        UpdateText(obstacleQuantities, spikeId, spikeQuantityText);
        UpdateText(obstacleQuantities, playerId, playerQuantityText);
    }
    private void UpdateText(Dictionary<string, int> obstacleQuantities, string id, TextMeshProUGUI textToUpdate)
    {
        if (obstacleQuantities.ContainsKey(id))
        {
            textToUpdate.text = obstacleQuantities[id].ToString() + "/" + LevelManagerMaster.Instance.GetObstacleSOByID(id).quantity;

            if (obstacleQuantities[id] == LevelManagerMaster.Instance.GetObstacleSOByID(id).quantity)
            {
                textToUpdate.color = Color.red;
            }
            else textToUpdate.color = Color.white;
        }
    }
}
