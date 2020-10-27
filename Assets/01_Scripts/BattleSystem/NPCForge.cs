﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCForge : MonoBehaviour
{

    Weapon.weaponType myWeapon = new Weapon.weaponType();
    Metal.metal myMetal = new Metal.metal();
    WeaponVars.team myTeam = new WeaponVars.team();

    int weaponWant = 0;
    int matWant = 0;

    [SerializeField]
    GameObject weapon = null;


    public float timer = 0;
    public float startTimer = 10;
    public float randomTime = 0;

    private void Start()
    {
        myTeam = WeaponVars.team.T2;
        timer = startTimer;
    }

    void SetWeapon()
    {
        matWant = Random.Range((int)Metal.metal.Tin, (int)Metal.metal.Bronze + 1);
        weaponWant = Random.Range((int)Weapon.weaponType.Sword, (int)Weapon.weaponType.Axe + 1);

        myMetal = (Metal.metal)matWant;
        myWeapon = (Weapon.weaponType)weaponWant;
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
}
