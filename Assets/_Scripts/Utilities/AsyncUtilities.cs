using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class AsyncUtilities
{
    public static async void Invoke(this Component _caller, Action _action, float _seconds)
    {
        await Task.Delay(TimeSpan.FromSeconds(_seconds));
        _action?.Invoke();
    }
}
