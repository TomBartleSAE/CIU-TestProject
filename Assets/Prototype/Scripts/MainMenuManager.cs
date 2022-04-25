using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Object levelToLoad;
    
    public void Play()
    {
        SceneManager.LoadScene(levelToLoad.name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}