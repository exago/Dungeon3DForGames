using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeBehaviour 
{
    public static void StartSlowMotion(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public static void EndSlowMotion()
    {
        Time.timeScale = 1;
    }
}
