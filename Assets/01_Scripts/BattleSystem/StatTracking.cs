using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatTracking : MonoBehaviour
{
    public static StatTracking instance = null;

    [SerializeField]
    Text weaponsText = null;
    int deliveredWeapons = 0;

    [SerializeField]
    Text timeText = null;
    int timeSurvived = 0;
    float curTime = 0;

    [SerializeField]
    Text pointsText = null;
    int pointsEarned = 0;

    float minutes = 0;
    float seconds = 0;

    [SerializeField]
    GameObject screen = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
        }
    }
    bool count = true;
    bool set = false;

    private void Update()
    {
        curTime = Time.time;

        if (screen.activeSelf)
        {
            count = false;
            if (!set)
            {
                set = true;
                timeSurvived = (int)curTime;
                setTextVars();
            }
        }
    }

    void setTextVars()
    {
        setWeaponsText();
        setTimeText(timeSurvived);
        setPointsText();
    }

    ///Set Text variables
    void setWeaponsText()
    {
        weaponsText.text = "Weapons delivered : " + deliveredWeapons;
    }
    void setTimeText(float display)
    {
        minutes = Mathf.FloorToInt(display / 60);
        seconds = Mathf.FloorToInt(display % 60);

        timeText.text = "Time Survived : " + string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText.fontSize = 40;
    }
    void setPointsText()
    {
        pointsText.text = "Points earned : " + pointsEarned;
    }


    ///Set Values

    public void addWeapon(int i)
    {
        deliveredWeapons = deliveredWeapons + i;
    }

    public void addToPoint(int i)
    {
        pointsEarned = pointsEarned + i;
    }
}
