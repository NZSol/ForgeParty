using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVars : MonoBehaviour
{
    public float durability;
    public float damage;
    public float damageMult;

    Weapon.weaponType myWeapon;
    Metal.metal myMetal;

    bool valsAssigned;

    public void setVar()
    {
        myMetal = GetComponent<Metal>().myMetal;
        myWeapon = GetComponent<Weapon>().myWeapon;

        //Assign Damage and Durability based on weapon material
        switch (myMetal)
        {
            case Metal.metal.Bronze:
                durability = 60;
                damage = 10;
                break;

            case Metal.metal.Copper:
                durability = 30;
                damage = 3;
                break;

            case Metal.metal.Tin:
                durability = 20;
                damage = 6;
                break;
        }

        //Assign Multiplier based on weapon type
        switch (myWeapon)
        {
            case Weapon.weaponType.Sword:
                damageMult = 1.25f;
                break;

            case Weapon.weaponType.Axe:
                damageMult = 1.5f;
                break;
        }
    }

    private void Update()
    {
        if (valsAssigned)
        {
            if (durability <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
