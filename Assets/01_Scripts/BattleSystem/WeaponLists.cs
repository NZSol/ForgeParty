using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLists : MonoBehaviour
{

    public List<WeaponVars> Team1 = new List<WeaponVars>();
    public List<WeaponVars> Team2 = new List<WeaponVars>();

    float timer;
    public float timerMax = 5;

    WeaponVars.team myTeam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        timer -= Time.deltaTime;


        if (timer <= 0)
        {
            if (Team1.Count <= Team2.Count)
            {
                for (int i = 0; i < Team1.Count; i++)
                {
                    Team2[i].gameObject.GetComponent<WeaponVars>().durability -= Team1[i].gameObject.GetComponent<WeaponVars>().totalDamage;
                    Team1[i].gameObject.GetComponent<WeaponVars>().durability -= Team2[i].gameObject.GetComponent<WeaponVars>().totalDamage;
                }
            }
            else if (Team2.Count <= Team1.Count)
            {
                for (int i = 0; i < Team2.Count; i++)
                {
                    Team2[i].gameObject.GetComponent<WeaponVars>().durability -= Team1[i].gameObject.GetComponent<WeaponVars>().totalDamage;
                    Team1[i].gameObject.GetComponent<WeaponVars>().durability -= Team2[i].gameObject.GetComponent<WeaponVars>().totalDamage;
                }
            }
            timer += timerMax;
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
