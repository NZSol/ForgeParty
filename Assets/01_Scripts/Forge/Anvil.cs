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
    public GameObject outputObj;

    public string MatName;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((axe || sword) && Hammering)
        {
            Progress += Time.deltaTime;
        }

        Progress = Mathf.Clamp(Progress, 0, _slideMax);

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
            Progress = 0;
            RunSwitch();
        }


    }

    void RunSwitch()
    {
        switch (material)
        {
            //Tin
            case 1:
                if (sharpAxe)
                {
                    outputObj = hammeredAxeSn;
                }
                else if (sharpSword)
                {
                    outputObj = hammeredSwordSn;
                }
                sharpAxe = false;
                sharpSword = false;
                axe = false;
                sword = false;

                break;

            case 2:
                if (sharpAxe)
                {
                    outputObj = hammeredAxeCu;
                }
                else if (sharpSword)
                {
                    outputObj = hammeredSwordCu;
                }
                sharpAxe = false;
                sharpSword = false;
                axe = false;
                sword = false;

                break;

            case 3:
                if (sharpAxe)
                {
                    outputObj = hammeredAxeBrze;
                }
                else if (sharpSword)
                {
                    outputObj = hammeredSwordBrze;
                }
                sharpAxe = false;
                sharpSword = false;
                axe = false;
                sword = false;

                break;

            default:
                break;
        }
    }
}
