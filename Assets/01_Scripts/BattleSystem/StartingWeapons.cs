using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingWeapons : MonoBehaviour
{

    Weapon.weaponType myWeapon = new Weapon.weaponType();
    Metal.metal myMetal = new Metal.metal();
    [SerializeField] WeaponVars.team myTeam = new WeaponVars.team();

    public int startCount = 0;

    int weapon = 0;
    int metal = 0;

    [SerializeField] GameObject weaponInstance = null;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < startCount; i++)
        {
            weapon = Random.Range((int)Weapon.weaponType.Sword, (int)Weapon.weaponType.Axe + 1);
            if (myTeam == WeaponVars.team.T1)
            {
                metal = Random.Range((int)Metal.metal.Tin, (int)Metal.metal.Bronze);
            }
            else
            {
                metal = Random.Range((int)Metal.metal.Tin, (int)Metal.metal.Bronze);
            }

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

            if (i > 8 && myTeam == WeaponVars.team.T2)
            {
                Destroy(instance);
            }
        }

        
    }

}
