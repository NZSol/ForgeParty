using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public static LevelSelect instance = null;
    public int levelVal;

    [SerializeField] GameObject TutoQuestionPanel = null;

    public bool allPlayersReady = false;
    bool player1Ready = false, player2Ready = false, player3Ready = false, player4Ready = false;
    bool player1Exists = false, player2Exists = false, player3Exists = false, player4Exists = false;

    public bool setPrefFalse = false;

    public bool settingsDisable = false;

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

    private void Start()
    {
        if (setPrefFalse)
        {
            PlayerPrefs.SetInt("SeenTutorial", 0);
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
    public GameMode.gameMode myMode;
    public void SetGameMode(int mode)
    {
        myMode = (GameMode.gameMode)mode;
    }

    public GameMode.gameMode GetGameMode()
    {
        return myMode;
    }

    public void AssignLevelValue(int var)
    {
        levelVal = var;
    }

    public int maxPlayers = 0;
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
                break;
            case 2:
                player2Ready = true;
                break;
            case 3:
                player3Ready = true;
                break;
            case 4:
                player4Ready = true;
                break;
        }
        setAllReady();
    }

    public void SetPlayerUnready(int i)
    {
        switch (i)
        {
            case 1:
                player1Ready = false;
                break;
            case 2:
                player2Ready = false;
                break;
            case 3:
                player3Ready = false;
                break;
            case 4:
                player4Ready = false;
                break;
            case 5:
                player1Ready = false;
                player2Ready = false;
                player3Ready = false;
                player4Ready = false;
                break;
        }
        allPlayersReady = false;
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
                else if (player1Ready && player2Ready && player3Ready && !player4Exists)
                {
                    allPlayersReady = true;
                }
                break;
        }
    }

    public void SetExistance(int i)
    {
        switch (i)
        {
            case 1:
                player1Exists = true;
                break;
            case 2:
                player2Exists = true;
                break;
            case 3:
                player3Exists = true;
                break;
            case 4:
                player4Exists = true;
                break;
            case 5:
                player1Exists = false;
                player2Exists = false;
                player3Exists = false;
                player4Exists = false;
                break;
        }
    }


    public void SetPlayerPrefTutorial()
    {
        PlayerPrefs.SetInt("SeenTutorial", 1);
    }

    public void CheckPlayerPrefTutorial()
    {
        if (PlayerPrefs.GetInt("SeenTutorial") == 0)
        {
            TutoQuestionPanel.SetActive(true);
            SetPlayerPrefTutorial();
        }
    }

    public void playersReset()
    {
        print("HitTarget");
        SetExistance(5);
        SetPlayerReady(5);
        allPlayersReady = false;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != SceneManager.GetSceneByBuildIndex(2).buildIndex)
        {
            if (Time.timeScale != 1)
            {
                Time.timeScale = 1;
            }
        }
    }

}
