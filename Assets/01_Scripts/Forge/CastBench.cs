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
    [SerializeField] Image progressMeter = null;
    [SerializeField] GameObject uiObj= null;
    [SerializeField] Image tick = null;
    [SerializeField] Image clock = null;

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
        progressMeter.fillAmount = timer / maxTimer;
        input = Metal.metal.Blank;
        outputMet = Metal.metal.Blank;
        weapon = gameObject.GetComponent<Weapon>().myWeapon;
        CastInner.SetActive(false);
        render.material = moldInnerMat;
        uiObj.SetActive(false);
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

        if ((rangeCheck || hasContents) && !uiObj.activeSelf)
        {
            uiObj.SetActive(true);
        }
        else if (!rangeCheck && !hasContents && uiObj.activeSelf)
        {
            uiObj.SetActive(false);
        }

        if (timer >= maxTimer && outputMet == Metal.metal.Blank)
        {
            timer = maxTimer;
            outputMet = input;
            input = Metal.metal.Blank;
            clock.enabled = false;
            tick.enabled = true;
        }

        progressMeter.fillAmount = timer/maxTimer;
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
            timer = 0;

            clock.enabled = true;
            tick.enabled = false;
            CastInner.SetActive(false);
            hasContents = false;
            return outputWeapon;
        }
    }

}
