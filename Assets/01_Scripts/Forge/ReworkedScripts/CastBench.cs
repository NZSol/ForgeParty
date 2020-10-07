﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastBench : Tool
{
    public Weapon.weaponType weapon;
    public Metal.metal outputMet;
    public Metal.metal input;

    public float timer = 5;

    public override void TakeItem(GameObject item)
    {
        input = item.GetComponent<Metal>().myMetal;
    }


    // Start is called before the first frame update
    void Start()
    {
        input = Metal.metal.Blank;
        outputMet = Metal.metal.Blank;
        weapon = gameObject.GetComponent<Weapon>().myWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        if (input != Metal.metal.Blank)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            timer = 5;
            outputMet = input;
            input = Metal.metal.Blank;
        }
    }


    public override GameObject GiveItem()
    {if (outputMet == Metal.metal.Blank)
        {
            return null;
        }
        else
        {
            var outputWeapon = Instantiate(outputPrefab);
            outputWeapon.GetComponent<Metal>().myMetal = outputMet;
            outputWeapon.GetComponent<Weapon>().myWeapon = weapon;

            outputMet = Metal.metal.Blank;

            return outputWeapon;
        }
    }

}
