using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreBucket : Tool
{
    [SerializeField] GameObject[] Ore;
    public bool tin, copper;
    public Metal Output;

    public override GameObject GiveItem()
    {

        var outputOre = Instantiate(outputPrefab);
        if (tin)
        {
            outputOre.GetComponent<Metal>().metal = Metal.metals.Tin;
        }
        if (copper)
        {
            outputOre.GetComponent<Metal>().metal = Metal.metals.Copper;
        }

        return outputOre;
    }



    public override void TakeItem(GameObject item)
    {
    }

}
