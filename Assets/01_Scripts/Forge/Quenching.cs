using System.Collections;
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

    [SerializeField] Slider _slide = null;


    public float timer = 0;
    public float completionTime = 5;

    [SerializeField] ParticleSystem steam = null;

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
                    break;
                case Weapon.weaponType.Axe:
                    outputPrefab = Axe;
                    break;
            }

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
        _slide.maxValue = completionTime;
        myTeam = WeaponVars.team.T1;
        steam.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputWeapon != Weapon.weaponType.Blank)
        {
            if (timer <= completionTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer -= completionTime;
                outputMet = inputMet;
                outputWeapon = inputWeapon;

                inputWeapon = Weapon.weaponType.Blank;
                inputMet = Metal.metal.Blank;
            }
        }

        _slide.value = timer;
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

            hasContents = false;
            return weaponOut;
        }
    }
}
