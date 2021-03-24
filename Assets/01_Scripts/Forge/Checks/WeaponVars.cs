using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVars : MonoBehaviour
{

    public enum team { T1, T2 };
    public team myTeam = new team();

    public float durability = 0;
    public float damage = 0;
    public float damageMult = 0;
    public float totalDamage = 0;

    public int timeVal;

    Weapon.weaponType myWeapon = new Weapon.weaponType();
    Metal.metal myMetal = new Metal.metal();

    bool valsAssigned = false;
    bool active = true;
    public bool inFight = false;


    private void Start()
    {
        if (durability == 0)
        {
            setVar();
        }
    }

    public void setVar()
    {
        myMetal = GetComponent<Metal>().myMetal;
        myWeapon = GetComponent<Weapon>().myWeapon;

        //Assign Damage and Durability based on weapon material
        switch (myMetal)
        {
            case Metal.metal.Bronze:
                ///durability = 60;
                ///damage = 10;
                timeVal = 12;
                break;

            case Metal.metal.Copper:
                ///durability = 30;
                ///damage = 3;
                timeVal = 8;
                break;

            case Metal.metal.Tin:
                ///durability = 20;
                ///damage = 6;
                timeVal = 4;
                break;
        }

        //Assign Multiplier based on weapon type
        ///switch (myWeapon)
        ///{
        ///    case Weapon.weaponType.Sword:
        ///        damageMult = 1.25f;
        ///        break;

        ///    case Weapon.weaponType.Axe:
        ///        damageMult = 1.5f;
        ///        break;
        ///}
        ///totalDamage = damage * damageMult;
        valsAssigned = true;
    }


    ///private void Update()
    ///{
    ///    if (transform.parent != null && (transform.parent.name == "TeamForgeList" || transform.parent.name == "NPCForgeList"))
    ///    {
    ///        switch (myTeam)
    ///        {
    ///            case team.T1:
    ///                if (inFight && active)
    ///                {
    ///                    GetComponentInParent<WeaponLists>().AddToList(gameObject);
    ///                    active = false;
    ///                }
    ///                break;

    ///            case team.T2:
    ///                if (valsAssigned && active)
    ///                {
    ///                    GetComponentInParent<WeaponLists>().AddToList(gameObject);
    ///                    active = false;
    ///                }
    ///                break;
    ///        }

    ///        if (durability <= 0 && valsAssigned)
    ///        {
    ///            GetComponentInParent<WeaponLists>().RemoveFromList(gameObject);
    ///        }
    ///    }

    ///}

}
