using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastBench : Tool
{
    public Weapon.weaponType weapon;
    public Metal.metal outputMet;
    public Metal.metal input;

    [SerializeField] Slider _slide;

    public float timer = 0;
    public float maxTimer = 5;

    public override void TakeItem(GameObject item)
    {
        input = item.GetComponent<Metal>().myMetal;
    }


    // Start is called before the first frame update
    void Start()
    {
        _slide.maxValue = timer;

        input = Metal.metal.Blank;
        outputMet = Metal.metal.Blank;
        weapon = gameObject.GetComponent<Weapon>().myWeapon;
        _slide.maxValue = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (input != Metal.metal.Blank)
        {
            timer += Time.deltaTime;
        }

        if (timer >= maxTimer)
        {
            timer = 0;
            outputMet = input;
            input = Metal.metal.Blank;
        }
        _slide.value = timer;
    }


    public override GameObject GiveItem()
    {if (outputMet == Metal.metal.Blank)
        {
            return null;
        }
        else
        {
            var outputWeapon = Instantiate(outputPrefab);
            outputWeapon.GetComponent<Metal>().myMetal = outputMet;
            outputWeapon.GetComponent<Weapon>().myWeapon = weapon;

            outputMet = Metal.metal.Blank;

            return outputWeapon;
        }
    }

}
