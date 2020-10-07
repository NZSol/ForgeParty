using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponType : MonoBehaviour
{

    public enum weaponType { Blank, Sword, Axe }
    public weaponType myWeapon;

    public weaponType ActiveWeapon()
    {
        return myWeapon;
    }

}
