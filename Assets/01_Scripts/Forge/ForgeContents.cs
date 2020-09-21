using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForgeContents : MonoBehaviour
{
        //Check
    bool OutputObj;

        //Ores
    [NonSerialized] public bool Sn;
    [NonSerialized] public bool Cu;

        //Metals
    [NonSerialized] public bool CuMet;
    [NonSerialized] public bool SnMet;

        //Alloys
    [NonSerialized] public bool BronzeAll;

        //Output Objects
    [SerializeField] GameObject CuMetal;
    [SerializeField] GameObject SnMetal;
    [SerializeField] GameObject BrnzMetal;

    //UI
    [SerializeField] Slider _slide;
    float meltTime;
    
    public float temperature;
    public float requiredTemp;
    public int metalForging;
    public GameObject instanceObj;


    public List<bool> metals = new List<bool>();

    private void Update()
    {
        if (!OutputObj)
        {

        }


        if (Cu && !Sn && temperature >= 25)
        {
            metalForging = 1;
            requiredTemp = 25;
        }
        if (Sn && !Cu && !CuMet && temperature >= 10)
        {
            metalForging = 2;
            requiredTemp = 10;
        }
        if (Sn && Cu && temperature >= 40 || SnMet && Cu && temperature >= 40 || Sn && CuMet && temperature >= 40)
        {
            metalForging = 3;
            requiredTemp = 40;
        }

        if (temperature < requiredTemp)
        {
            metalForging = 0;
        }

        _slide.value = meltTime;

        switch (metalForging)
        {
                //Copper Smelting
            case 1:
                meltTime += Time.deltaTime;

                if (meltTime > 5)
                {
                    Cu = false;

                    meltTime = 0;
                    CuMet = true;
                    metalForging = 0;
                    instanceObj = CuMetal;
                    OutputObj = true;
                }

                break;

                //Tin Smelting
            case 2:
                meltTime += Time.deltaTime;
                if (meltTime > 5)
                {
                    Sn = false;

                    meltTime = 0;
                    SnMet = true;
                    metalForging = 0;
                    instanceObj = SnMetal;
                    OutputObj = true;
                }

                break;

                //Bronze Alloying
            case 3:
                meltTime += Time.deltaTime;
                if (meltTime > 5)
                {
                    Cu = false;
                    Sn = false;
                    CuMet = false;
                    SnMet = false;

                    meltTime = 0;
                    BronzeAll = true;
                    metalForging = 0;
                    instanceObj = BrnzMetal;
                    OutputObj = true;
                }

                break;

                //Too cold/Do nothing
            default:
                if (meltTime > 0)
                {
                    meltTime -= Time.deltaTime / 2;
                }
                break;
        }
    }


    void listRemove()
    {
        for (int i = 0; i<metals.Count; i++)
        {
            metals.Remove(metals[i]);
        }
    }

    void reset()
    {
 
    }
}
