using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] WinLose gameState;

    public int secondsLeft = 10;
    public bool takingAway = false;
    public bool addseconds = false;
    public int timeToAdd = 0;

    [SerializeField] Text timer;
    [SerializeField] Image timerRadialAdjust;
    public float endgameTime;
    float maxTimer;
    float minutes;
    float seconds;
    [SerializeField] Color endColor = Color.red;
    [SerializeField] Color startColor;
    [SerializeField]float threshHold;

    private void Start()
    {
        maxTimer = endgameTime;
        startColor = timerRadialAdjust.color;
        gameState = GetComponent<WinLose>();
    }

    private void Update()
    {
        if (takingAway == false && secondsLeft > 0)
        {

            endgameTimer();
        }

        if( Input.GetKeyDown(KeyCode.Space))
        {
            GiveSeconds(time: timeToAdd);
        }

        if (endgameTime <= threshHold)
        {
            threshHold = endgameTime;
            timer.color = Color.Lerp(endColor, startColor, threshHold / 30);
            timerRadialAdjust.color = Color.Lerp(endColor, startColor, threshHold / 30);
        }
        else
        {
            threshHold = 30;
        }
    }

    void endgameTimer()
    {
        timer.gameObject.SetActive(true);
        if (endgameTime > 0)
        {
            endgameTime = Mathf.Clamp(endgameTime, 0, maxTimer);
            endgameTime -= Time.deltaTime;
            timerRadialAdjust.fillAmount = endgameTime / maxTimer;
        }
        else
        {
            endgameTime = 0;
            gameState.Lose();
        }

        displayTime(endgameTime);
    }


    void displayTime(float display)
    {

        minutes = Mathf.FloorToInt(display / 60);
        seconds = Mathf.FloorToInt(display % 60);

        if (minutes <= 0)
        {
            timer.text = string.Format("{0:00}", seconds);
            timer.fontSize = 90;
        }
        else
        {
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            timer.fontSize = 40;
        }

    }


    public void GiveSeconds(int time)
    {
        endgameTime += time;

    }
}
