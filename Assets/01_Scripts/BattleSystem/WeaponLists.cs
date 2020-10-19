using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponLists : MonoBehaviour
{

    public List<WeaponVars> Team1 = new List<WeaponVars>();
    public List<WeaponVars> Team2 = new List<WeaponVars>();

    float timer;
    public float timerMax = 5;

    WeaponVars.team myTeam;

    [SerializeField] Slider _slide;
    float slideVal;
    int multiplier;


    // Start is called before the first frame update
    void Start()
    {
        _slide.value = 0;
        print(slideVal + " Start");
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        slideVal = Mathf.Clamp(slideVal, _slide.minValue, _slide.maxValue);
        _slide.value = slideVal;
        print(slideVal + " Update");

        if (timer <= 0)
        {
            if (Team1.Count <= Team2.Count)
            {
                for (int i = 0; i < Team1.Count; i++)
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
            else if (Team2.Count <= Team1.Count)
            {
                for (int i = 0; i < Team2.Count; i++)
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

        if (Team1.Count < Team2.Count)
        {
            slideVal -= (Time.deltaTime / multiplier);
        }
        else if (Team1.Count > Team2.Count)
        {
            slideVal += ((Time.deltaTime * 1.5f) / multiplier);
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
