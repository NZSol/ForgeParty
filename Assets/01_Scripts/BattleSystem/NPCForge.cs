using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCForge : MonoBehaviour
{

    Weapon.weaponType myWeapon;
    Metal.metal myMetal;
    WeaponVars.team myTeam;

    [SerializeField]
    GameObject weapon;

    GameObject weaponInstance;


    public float timer;
    public float startTimer = 10;
    public float randomTime;

    private void Start()
    {
        myTeam = WeaponVars.team.T2;
    }

    void SetWeapon()
    {
        weaponInstance = weapon;
        myWeapon = Weapon.weaponType.Sword;
        myMetal = Metal.metal.Copper;
    }


    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer += startTimer + Random.Range(-5, 5);
            SetWeapon();
            
            var instance = Instantiate(weapon, gameObject.transform);
            instance.GetComponent<Metal>().myMetal = myMetal;
            instance.GetComponent<Weapon>().myWeapon = myWeapon;
            instance.GetComponent<WeaponVars>().setVar();
            instance.GetComponent<WeaponVars>().myTeam = myTeam;
            //instance.SetActive(false);
        }
    }

    void RandomizeTimer(float i)
    {
        randomTime = (startTimer += Random.Range(-5, 5));
        startTimer = 10;
    }




}
