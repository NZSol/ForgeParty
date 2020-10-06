using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponType : MonoBehaviour
{

    public enum weaponType { Blank, Sword, Axe }
    public weaponType weapon;

    public weaponType ActiveWeapon()
    {
        return weapon;
    }

}
