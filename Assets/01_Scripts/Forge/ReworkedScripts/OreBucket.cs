using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreBucket : Tool
{
    [SerializeField] GameObject[] Ore;
    public Metal.metal Output;

    public override GameObject GiveItem()
    {
        outputPrefab = Ore[Random.Range(0, 2)];

        var outputOre = Instantiate(outputPrefab);
        
        outputOre.GetComponent<Metal>().myMetal = Output;

        return outputOre;
    }



    public override void TakeItem(GameObject item)
    {
    }

    void Start()
    {
        Output = gameObject.GetComponent<Metal>().myMetal;
    }
}
