using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragDropObjectUI : MonoBehaviour
{
    public static DragDropObjectUI Instance; // Singleton instance

    // Các UI elements để hiển thị số lượng
    [SerializeField] private TextMeshProUGUI spikeQuantityText;
    [SerializeField] private TextMeshProUGUI playerQuantityText;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Hàm cập nhật giao diện người dùng
    public void UpdateUI(Dictionary<string, int> obstacleQuantities)
    {
        string spikeId = "Spike";
        string playerId = "Player";
        // Kiểm tra và hiển thị số lượng đối tượng
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
