using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public static LevelSelect instance = null;
    public int levelVal;


    public bool allPlayersReady = false;
    bool player1Ready = false, player2Ready = false, player3Ready = false, player4Ready = false;

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
            }
        }

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

    int maxPlayers = 0;
    public void AssignPlayerCount (int i)
    {
        maxPlayers = i;
    }

    public void SetPlayerReady(int i)
    {
        switch (i)
        {
            case 1:
                player1Ready = true;
                print("P1Ready");
                break;
            case 2:
                player2Ready = true;
                print("P2Ready");
                break;
            case 3:
                player3Ready = true;
                print("P3Ready");
                break;
            case 4:
                player4Ready = true;
                print("P4Ready");
                break;
        }
        setAllReady();
    }

    public bool GetPlayerReady(int i)
    {
        var boolVal = false;
        switch (i)
        {
            case 1:
                boolVal = player1Ready;
                break;
            case 2:
                boolVal = player2Ready;
                break;
            case 3:
                boolVal = player3Ready;
                break;
            case 4:
                boolVal = player4Ready;
                break;
        }
        return boolVal;
    }

    public void setAllReady()
    {
        switch (maxPlayers)
        {
            case 1:
                if (player1Ready)
                {
                    allPlayersReady = true;
                }
                break;

            case 2:
                if (player1Ready && player2Ready)
                {
                    allPlayersReady = true;
                }
                break;


            case 4:
                if (player1Ready && player2Ready && player3Ready && player4Ready)
                {
                    allPlayersReady = true;
                }
                break;
        }
        print("Are they all ready? " + allPlayersReady);
    }




}
