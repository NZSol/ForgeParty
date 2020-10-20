using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingWeapons : MonoBehaviour
{

    Weapon.weaponType myWeapon;
    Metal.metal myMetal;
    [SerializeField] WeaponVars.team myTeam;

    public int startCount;

    int weapon;
    int metal;

    [SerializeField] GameObject weaponInstance;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < startCount; i++)
        {
            weapon = Random.Range((int)Weapon.weaponType.Sword, (int)Weapon.weaponType.Axe + 1);
            metal = Random.Range((int)Metal.metal.Tin, (int)Metal.metal.Bronze + 1);

            myMetal = (Metal.metal)metal;
            myWeapon = (Weapon.weaponType)weapon;

            var instance = Instantiate(weaponInstance, gameObject.transform);

            instance.GetComponent<Metal>().myMetal = myMetal;
            instance.GetComponent<Weapon>().myWeapon = myWeapon;
            instance.GetComponent<WeaponVars>().myTeam = myTeam;
            if (myTeam == WeaponVars.team.T1)
            {
                instance.GetComponent<WeaponVars>().inFight = true;
            }

        }
    }

}
