﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BellowsFunc : Tool
{
    
    [SerializeField] Slider _slide = null;

    public float localTemp = 0;
    public float coolingMult = 0;

    [SerializeField] ParticleSystem fire;

    Vector3 startSize = new Vector3(0,0,0);
    Vector3 endSize = new Vector3(1.5f, 1.5f, 1.5f);

    // Start is called before the first frame update
    void Start()
    {
        _slide.maxValue = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (charging)
        {
            if (localTemp < _slide.maxValue)
            {
                localTemp += Time.deltaTime;
            }


        }
        else
        {
            if (localTemp > 0)
            {
                localTemp -= Time.deltaTime / coolingMult;
            }
        }

        _slide.value = localTemp;


        fire.transform.localScale = Vector3.Lerp(startSize, endSize, (localTemp / 10));
    }

    public override void TakeItem(GameObject item)
    {
    }

    public override GameObject GiveItem()
    {
        return null;
    }
}
