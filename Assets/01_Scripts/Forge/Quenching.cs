﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quenching : Tool
{

    public Weapon.weaponType inputWeapon = new Weapon.weaponType();
    public Metal.metal inputMet = new Metal.metal();

    public Weapon.weaponType outputWeapon = new Weapon.weaponType();
    public Metal.metal outputMet = new Metal.metal();

    WeaponVars.team myTeam = new WeaponVars.team();

    [SerializeField] GameObject Sword = null;
    [SerializeField] GameObject Axe = null;

    [SerializeField] Image progressMeter = null;
    [SerializeField] GameObject uiObj = null;
    [SerializeField] Image clock = null;
    [SerializeField] Image tick = null;


    [SerializeField] ParticleSystem steam = null;

    //Feedback Asset
    //weapons
    [SerializeField] GameObject axeModel = null;
    [SerializeField] GameObject swordModel = null;
    //Renderers
    [SerializeField] Renderer render = null;
    [SerializeField] Material mat = null;
    //Colours
    [SerializeField] Color bronzeCol;
    [SerializeField] Color copperCol;
    [SerializeField] Color tinCol;

    public override void TakeItem(GameObject item)
    {

        if (!hasContents)
        {
            hasContents = true;
            inputWeapon = item.GetComponent<Weapon>().myWeapon;
            inputMet = item.GetComponent<Metal>().myMetal;


            //ASSIGN OUTPUT PREFAB
            switch (inputWeapon)
            {
                case Weapon.weaponType.Blank:
                    outputPrefab = null;
                    break;
                case Weapon.weaponType.Sword:
                    outputPrefab = Sword;
                    render = swordModel.GetComponent<Renderer>();
                    swordModel.SetActive(true);
                    break;
                case Weapon.weaponType.Axe:
                    outputPrefab = Axe;
                    render = axeModel.GetComponent<Renderer>();
                    axeModel.SetActive(true);
                    break;
            }

            mat = render.material;

            var desiredColor = new Color();
            switch (inputMet)
            {
                case Metal.metal.Bronze:
                    desiredColor = bronzeCol;
                    break;
                case Metal.metal.Copper:
                    desiredColor = copperCol;
                    break;
                case Metal.metal.Tin:
                    desiredColor = tinCol;
                    break;
            }
            mat.SetColor("colourTo", desiredColor);
            steam.Play();
        }
        else
        {
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myTeam = WeaponVars.team.T1;
        steam.Stop();

        completionTime = 3;
        axeModel.SetActive(false);
        swordModel.SetActive(false);
        tick.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputWeapon != Weapon.weaponType.Blank)
        {
            if (timer < completionTime)
            {
                timer += Time.deltaTime;

                tick.enabled = false;
                clock.enabled = true;
            }
            else if (timer >= completionTime && outputMet == Metal.metal.Blank)
            {
                timer = completionTime;
                outputMet = inputMet;
                outputWeapon = inputWeapon;

                inputWeapon = Weapon.weaponType.Blank;
                inputMet = Metal.metal.Blank;
                tick.enabled = true;
                clock.enabled = false;
            }
        }

        if ((hasContents || rangeCheck) && !uiObj.activeSelf)
        {
            uiObj.SetActive(true);
        }
        else if (!hasContents && !rangeCheck && uiObj.activeSelf)
        {
            uiObj.SetActive(false);
        }

        progressMeter.fillAmount = timer/completionTime;
    }

    public override GameObject GiveItem()
    {
        //IF WEAPON != NULL, ALLOW COLLECTION
        if (outputWeapon == Weapon.weaponType.Blank)
        {
            return null;
        }
        else
        {
            //ASSIGN METAL + WEAPON TYPE
            var weaponOut = Instantiate(outputPrefab);
            weaponOut.GetComponent<Metal>().myMetal = outputMet;
            weaponOut.GetComponent<Weapon>().myWeapon = outputWeapon;
            weaponOut.GetComponent<Weapon>().completed = true;
            weaponOut.GetComponent<WeaponVars>().myTeam = myTeam;

            outputMet = Metal.metal.Blank;
            outputWeapon = Weapon.weaponType.Blank;
            weaponOut.GetComponent<Weapon>().completed = true;

            axeModel.SetActive(false);
            swordModel.SetActive(false);
            hasContents = false;
            timer = 0;
            clock.enabled = true;
            tick.enabled = false;
            return weaponOut;
        }
    }
}
