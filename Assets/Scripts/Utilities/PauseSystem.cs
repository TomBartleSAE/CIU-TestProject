using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should probably be a static class
public class PauseSystem : MonoBehaviour
{
    // It is much more cumbersome to create something like an interface to handle pausing
    // I.e. having to set the pause behaviour for every active object in the game
    
    // To allow objects to continue moving while paused (e.g. menu animations), use unscaled time
    // To allow audio to play when paused, use AudioSource.ignoreListenerPause=true (e.g. in Start)

    public event Action GamePausedEvent;
    public event Action GameResumedEvent;
    
    public void Pause()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        GamePausedEvent?.Invoke();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        GameResumedEvent?.Invoke();
    }
}
