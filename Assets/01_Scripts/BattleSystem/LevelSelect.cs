using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public int levelVal = 0;

    public void moveToNoDestroy()
    {
    }

    private void Start()
    {
        var curScene = SceneManager.GetActiveScene();
        if (curScene == SceneManager.GetSceneByBuildIndex(0))
        {
            DontDestroyOnLoad(this.gameObject);
        }
        if(curScene == SceneManager.GetSceneByBuildIndex(1))
        {
            var god = GameObject.FindWithTag("LevelGod");

            god.GetComponent<LevelSet>().SetTargetLevel(levelVal);
            SceneManager.MoveGameObjectToScene(god, curScene);
            
        }
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
