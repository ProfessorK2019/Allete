using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputAction input;
    private void Awake()
    {
        input = new PlayerInputAction();
        input.Player.Enable();
    }
    public bool Up() => input.Player.Up.triggered;
    public bool Down() => input.Player.Down.triggered;
    public bool Left() => input.Player.Left.triggered;
    public bool Right() => input.Player.Right.triggered;

}
