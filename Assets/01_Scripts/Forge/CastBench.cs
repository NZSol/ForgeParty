using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastBench : Tool
{
    //Metals
    public Weapon.weaponType weapon = new Weapon.weaponType();
    public Metal.metal outputMet = new Metal.metal();
    public Metal.metal input = new Metal.metal();

    //UI Slider
    [SerializeField] Slider _slide = null;

    //Timer Values
    public float maxTimer = 5;

    //Inserts
    [SerializeField] GameObject CastInner = null;
    [SerializeField] Renderer render = null;
    [SerializeField] Material moldInnerMat = null;
    //Insert End Colours
    [SerializeField] Color bronzeCol;
    [SerializeField] Color copperCol;
    [SerializeField] Color tinCol;
    [SerializeField] Color startCol;

    public override void TakeItem(GameObject item)
    {
        if (!hasContents)
        {
            input = item.GetComponent<Metal>().myMetal;
            hasContents = true;
            CastInner.SetActive(true);
            moldInnerMat.SetColor("colourTo", startCol);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _slide.maxValue = timer;
        input = Metal.metal.Blank;
        outputMet = Metal.metal.Blank;
        weapon = gameObject.GetComponent<Weapon>().myWeapon;
        _slide.maxValue = maxTimer;
        CastInner.SetActive(false);
        render.material = moldInnerMat;
    }

    // Update is called once per frame
    void Update()
    {
        if (input != Metal.metal.Blank)
        {
            var endCol = new Color();
            timer += Time.deltaTime;
            switch (input)
            {
                case Metal.metal.Bronze:
                    endCol = bronzeCol;
                    break;
                case Metal.metal.Copper:
                    endCol = copperCol;
                    break;

                case Metal.metal.Tin:
                    endCol = tinCol;
                    break;
            }
            if (timer < maxTimer)
            {
                moldInnerMat.SetColor("colourTo", Color.Lerp(startCol, endCol, timer/maxTimer));
            }

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

            CastInner.SetActive(false);
            hasContents = false;
            return outputWeapon;
        }
    }

}
