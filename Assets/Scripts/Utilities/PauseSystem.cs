using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    // It is much more cumbersome to create something like an interface to handle pausing
    // I.e. having to set the pause behaviour for every active object in the game
    // To allow objects to continue moving while paused (e.g. menu animations), use unscaled time
    
    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
}
