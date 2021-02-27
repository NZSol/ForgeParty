using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public static LevelSelect instance = null;
    public int levelVal;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
                //instance = this;
            }

        }

        //if (SceneManager.GetActiveScene().buildIndex == SceneManager.GetSceneByBuildIndex(0).buildIndex)
        //{
        //    DontDestroyOnLoad(this.gameObject);
        //}
    }

    public void setLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.GetSceneByBuildIndex(2).buildIndex)
        {
            var god = GameObject.FindWithTag("LevelGod");
            god.GetComponent<LevelSet>().SetTargetLevel(levelVal);
        }
    }

    public void AssignLevelValue(int var)
    {
        levelVal = var;
    }
    public void assign01()
    {
        levelVal = 0;
    }
    public void assign02()
    {
        levelVal = 1;
    }
    public void assign03()
    {
        levelVal = 2;
    }
    public void assign04()
    {
        levelVal = 3;
    }
}
