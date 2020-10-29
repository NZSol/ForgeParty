using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponLists : MonoBehaviour
{

    public List<WeaponVars> Team1 = new List<WeaponVars>();
    public List<WeaponVars> Team2 = new List<WeaponVars>();

    float timer = 0;
    public float timerMax = 5;

    WeaponVars.team myTeam = new WeaponVars.team();

    [SerializeField] Slider _slide = null;
    public float slideVal = 0;
    int multiplier = 1;

    public int combatWidth = 0;
    [SerializeField] Text t1Count;
    [SerializeField] Text t2Count;

    // Start is called before the first frame update
    void Start()
    {
        _slide.value = 0;
        multiplier = 1;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        slideVal = Mathf.Clamp(slideVal, _slide.minValue, _slide.maxValue);
        _slide.value = slideVal;

        t1Count.text = "" + Team1.Count;
        t2Count.text = "" + Team2.Count;

        if (timer <= 0)
        {
            if (Team1.Count <= Team2.Count && Team1.Count > 0)
            {
                for (int i = 0; i < combatWidth; i++)
                {
                    Team2[i].gameObject.GetComponent<WeaponVars>().durability -= Team1[i].gameObject.GetComponent<WeaponVars>().totalDamage;
                    Team1[i].gameObject.GetComponent<WeaponVars>().durability -= Team2[i].gameObject.GetComponent<WeaponVars>().totalDamage;
                }

                //Assign Multiplier
                if (Team1.Count <= 1)
                {
                    multiplier = 1;
                }
                else
                {
                    multiplier = Team1.Count;
                }
            }
            else if (Team2.Count <= Team1.Count && Team2.Count > 0)
            {

                for (int i = 0; i < combatWidth; i++)
                {
                    Team2[i].gameObject.GetComponent<WeaponVars>().durability -= Team1[i].gameObject.GetComponent<WeaponVars>().totalDamage;
                    Team1[i].gameObject.GetComponent<WeaponVars>().durability -= Team2[i].gameObject.GetComponent<WeaponVars>().totalDamage;
                }
                //Assign Multiplier
                if (Team2.Count <= 1)
                {
                    multiplier = 1;
                }
                else
                {
                    multiplier = Team2.Count;
                }
            }
            timer += timerMax;

        }


        if (Team1.Count < combatWidth || Team2.Count < combatWidth)
        {
            if (Team1.Count < Team2.Count)
            {
                slideVal -= (Time.deltaTime / (Team1.Count + 1));
            }
            else if (Team1.Count > Team2.Count)
            {
                slideVal += ((Time.deltaTime * 1.5f) / (Team2.Count + 1));
            }
        }
        else
        {
            if (Team1.Count < Team2.Count)
            {
                slideVal -= Time.deltaTime / 6;
            }
            else if (Team1.Count > Team2.Count)
            {
                slideVal += Time.deltaTime / 6;
            }
        }
    }

    public void AddToList (GameObject obj)
    {
        myTeam = obj.GetComponent<WeaponVars>().myTeam;
        switch (myTeam)
        {
            case WeaponVars.team.T1:
                Team1.Add(obj.GetComponent<WeaponVars>());
                break;

            case WeaponVars.team.T2:
                Team2.Add(obj.GetComponent<WeaponVars>());
                break;
        }
    }

    public void RemoveFromList(GameObject obj)
    {
        myTeam = obj.GetComponent<WeaponVars>().myTeam;
        switch (myTeam)
        {
            case WeaponVars.team.T1:
                Team1.Remove(obj.GetComponent<WeaponVars>());
                break;

            case WeaponVars.team.T2:
                Team2.Remove(obj.GetComponent<WeaponVars>());
                break;
        }
        Destroy(obj);
    }

}
