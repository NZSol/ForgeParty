using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{

    public List<GameObject> Weapons = new List<GameObject>();

    public float totalDurable;
    public float totalDamage;
    public float localDamage;
    public float localDurable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void AddToBattle(GameObject obj)
    {
        totalDamage += obj.GetComponent<WeaponVars>().totalDamage;
        Weapons.Add(obj);
    }

    public void RemoveFromBattle(GameObject obj)
    {
        totalDamage -= obj.GetComponent<WeaponVars>().totalDamage;
        Weapons.Remove(obj);
    }
}
