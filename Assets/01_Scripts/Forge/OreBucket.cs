﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreBucket : Tool
{
    [SerializeField] GameObject[] Ore = new GameObject[0];
    public Metal.metal Output = new Metal.metal();
    int randomValMin = 1, randomValMax = 101, val = 0;



    public override GameObject GiveItem()
    {
        val = Random.Range(randomValMin, randomValMax);

        if (val <= 50)
        {
            outputPrefab = Ore[0];
        }
        else
        {
            outputPrefab = Ore[1];
        }

        var outputOre = Instantiate(outputPrefab);
        
        outputOre.GetComponent<Metal>().myMetal = Output;

        return outputOre;
    }



    public override void TakeItem(GameObject item)
    {
    }

    void Start()
    {
        Output = GetComponent<Metal>().myMetal;
        
    }
}
