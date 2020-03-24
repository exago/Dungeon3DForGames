using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    private static float _volume = 0.80f;
    public static float Volume {  get { return _volume; } set { _volume = value; } }
}
