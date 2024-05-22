using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackgroundColor : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Color newColor;
    public void ChangeColor()
    {
        cam.backgroundColor = newColor;
    }
}
