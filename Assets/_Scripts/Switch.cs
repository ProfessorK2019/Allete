using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering.Universal;
using System;

public class Switch : MonoBehaviour
{   
    public event Action OnSwitchPress;
    public event Action OnSwitchRelease;
    private Light2D growLight;
    [SerializeField] private Tilemap tilemap; // Gán Tilemap từ Inspector hoặc thông qua mã
    [SerializeField] private TileBase tileToSet; // Gán sprite tile từ Inspector hoặc thông qua mã

    [SerializeField] private Vector3Int positionToSet;

    void Start()
    {
        growLight = GetComponentInChildren<Light2D>();
        growLight.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<ITriggerable>(out ITriggerable ITriggerable))
        {   
            OnSwitchPress?.Invoke();
            growLight.gameObject.SetActive(true);
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<ITriggerable>(out ITriggerable ITriggerable))
        {
            OnSwitchRelease?.Invoke();
            growLight.gameObject.SetActive(false);
            
        }
    }
}
