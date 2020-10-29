using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSwaper : MonoBehaviour
{




    // attach this to loading scene.
    public void Load()
    {
        SceneManager.LoadScene(1);

    }

   

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);

    }





    public void HeadOut()
    {
        Application.Quit();
    }


}
