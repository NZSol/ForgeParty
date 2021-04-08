using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{

    public bool pauseNow = false;
    [SerializeField] GameObject pauseUI = null;
    [SerializeField] GameObject settingsUI = null;

    public void Pause()
    {
        pauseNow = !pauseNow;
        if (pauseNow)
        {
            PauseMenu();
        }
        else
        {
            Unpause();
        }
    }

    void PauseMenu()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }

    void Unpause()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        settingsUI.SetActive(false);
    }

    
    public void disableSettings()
    {
        LevelSelect.instance.settingsDisable = true;
    }
}
