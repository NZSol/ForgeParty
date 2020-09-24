using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anvil : MonoBehaviour
{
    public bool Hammering;

    //Weapon Bools
    public bool sword;
    public bool axe;

    //Material Bools
    public bool copper;
    public bool bronze;
    public bool tin;


    bool sharpAxe;
    bool sharpSword;


    float Progress;

    public int material;

    //UI
    [SerializeField] Slider _slide;
    public int _slideMax;

    [SerializeField] GameObject hammeredSwordBrze, hammeredSwordCu, hammeredSwordSn;
    [SerializeField] GameObject hammeredAxeBrze, hammeredAxeCu, hammeredAxeSn;
    [SerializeField] GameObject OutputObj;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Progress += Time.deltaTime;
        Progress = Mathf.Clamp(Progress, 0, _slideMax);

        if (_slide != null)
        {
            _slide.value = Progress;
            if (_slide.value == _slideMax)
            {
                if (axe)
                {
                    axe = false;
                    sharpAxe = true;
                }
                else if (sword)
                {
                    sword = false;
                    sharpSword = true;
                }
            }
        }


        switch (material)
        {
            //Tin
            case 1:
                if (sharpAxe)
                {
                    OutputObj = hammeredAxeSn;
                }
                else if (sharpSword)
                {
                    OutputObj = hammeredSwordSn;
                }
                break;

            case 2:
                if (sharpAxe)
                {
                    OutputObj = hammeredAxeCu;
                }
                else if (sharpSword)
                {
                    OutputObj = hammeredSwordCu;
                }
                break;

            case 3:
                if (sharpAxe)
                {
                    OutputObj = hammeredAxeBrze;
                }
                else if (sharpSword)
                {
                    OutputObj = hammeredSwordBrze;
                }
                break;
        }

    }
}
