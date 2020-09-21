using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Animator animPanel;
    bool quitPause;


    private void OnEnable()
    {
        animPanel = gameObject.GetComponent<Animator>();
        quitPause = false;
    }

    private void Update()
    {
        if(quitPause== true)
        {
            animPanel.SetBool("ExitPause", true);

        }

        else
        {
            animPanel.SetBool("ExitPause", false);
        }



    }


    public void ClosePause()
    {
        quitPause = true;

    }


    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
}
