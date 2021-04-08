using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    public enum gameMode { TimeAttack, Survival};
    public gameMode myMode;

    [SerializeField] CountdownTimer timeAttack = null;
    [SerializeField] StockMode Survival = null;

    public bool SetManual = false;

    private void Start()
    {
        if (LevelSelect.instance != null)
        {
            myMode = LevelSelect.instance.GetGameMode();
        }
        onStart(myMode);
    }

    private void onStart(gameMode mode)
    {

        switch (mode)
        {
            case gameMode.TimeAttack:
                timeAttack.enabled = true;
                break;
            case gameMode.Survival:
                Survival.enabled = true;
                break;
        }
    }
}
