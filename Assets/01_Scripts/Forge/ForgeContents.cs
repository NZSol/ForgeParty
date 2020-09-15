using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForgeContents : MonoBehaviour
{
        //Check
    bool OutputObj;

        //Ores
    public bool Sn;
    public bool Cu;

        //Metals
    bool CuMet;
    bool SnMet;
    
        //Alloys
    bool BronzeAll;

    //UI
    [SerializeField] Slider _slide;
    float meltTime;
    
    public float temperature;
    public float requiredTemp;
    public int metalForging;

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
        if (Sn && !Cu && temperature >= 10)
        {
            metalForging = 2;
            requiredTemp = 10;
        }
        if (Sn && Cu && temperature >= 40)
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
            case 1:
                meltTime += Time.deltaTime;

                if (meltTime > 5)
                {
                    Cu = false;

                    meltTime = 0;
                    CuMet = true;
                    metalForging = 0;
                    OutputObj = true;
                }

                break;

            case 2:
                meltTime += Time.deltaTime;
                if (meltTime > 5)
                {
                    Sn = false;

                    meltTime = 0;
                    SnMet = true;
                    metalForging = 0;
                    OutputObj = true;
                }

                break;

            case 3:
                meltTime += Time.deltaTime;
                if (meltTime > 5)
                {
                    Cu = false;
                    Sn = false;

                    meltTime = 0;
                    BronzeAll = true;
                    metalForging = 0;
                    OutputObj = true;
                }

                break;

            default:
                if (meltTime > 0)
                {
                    meltTime -= Time.deltaTime / 2;
                }
                break;
        }
    }

}
