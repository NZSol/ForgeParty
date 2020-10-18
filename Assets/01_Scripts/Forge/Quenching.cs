using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quenching : Tool
{

    public Weapon.weaponType inputWeapon;
    public Metal.metal inputMet;

    public Weapon.weaponType outputWeapon;
    public Metal.metal outputMet;

    WeaponVars.team myTeam;

    [SerializeField] GameObject Sword;
    [SerializeField] GameObject Axe;

    [SerializeField] Slider _slide;


    public float timer;
    public float completionTime = 5;
    

    public override void TakeItem(GameObject item)
    {
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
    }

    // Start is called before the first frame update
    void Start()
    {
        _slide.maxValue = completionTime;
        myTeam = WeaponVars.team.T1;
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

            return weaponOut;
        }
    }
}
