using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anvil : Tool
{
    public bool canAnimate = false;

    //OUTPUT ENUM VALUES
    public Metal.metal outputMet = new Metal.metal();
    public Weapon.weaponType outputWeapon = new Weapon.weaponType();
    //INPUT ENUM VALUES
    public Metal.metal inputMet = new Metal.metal();
    public Weapon.weaponType inputWeapon = new Weapon.weaponType();

    [SerializeField] GameObject Sword = null;
    [SerializeField] GameObject Axe = null;

    [SerializeField] Image progressMeter = null;
    [SerializeField] GameObject uiMeter = null;
    [SerializeField] Image tick = null;
    [SerializeField] Image clock = null;

    public ParticleSystem spark;

    //WeaponDisplay
    [SerializeField] GameObject axeModel = null;
    [SerializeField] GameObject swordModel = null;
    [SerializeField] Renderer render = null;
    [SerializeField] Material mat = null;
    //Colours
    [SerializeField] Color bronzeCol;
    [SerializeField] Color copperCol;
    [SerializeField] Color tinCol;

    public override void TakeItem(GameObject item)
    {
        if (!hasContents)
        {
            timer = 0;

            hasContents = true;
            canAnimate = true;
            inputMet = item.GetComponent<Metal>().myMetal;
            inputWeapon = item.GetComponent<Weapon>().myWeapon;
            

            switch (inputWeapon)
            {
                case Weapon.weaponType.Blank:
                    outputPrefab = null;
                    break;
                case Weapon.weaponType.Sword:
                    outputPrefab = Sword;
                    render = swordModel.GetComponent<Renderer>();
                    swordModel.SetActive(true);
                    break;
                case Weapon.weaponType.Axe:
                    outputPrefab = Axe;
                    render = axeModel.GetComponent<Renderer>();
                    axeModel.SetActive(true);
                    break;
            }
            mat = render.material;
            var desiredColor = new Color();
            switch (inputMet)
            {
                case Metal.metal.Bronze:
                    desiredColor = bronzeCol;
                    break;
                case Metal.metal.Copper:
                    desiredColor = copperCol;
                    break;
                case Metal.metal.Tin:
                    desiredColor = tinCol;
                    break;
            }
            mat.SetColor("colourTo", desiredColor);
        }
    }
    public void IncreaseTimer(float val)
    {
        if (timer < completionTime)
        {
            timer += val;
            if (timer >= completionTime)
            {
                hasContents = false;
                outputMet = inputMet;
                outputWeapon = inputWeapon;

            }
        }
        print(timer + "  Timer");
        print(completionTime + "  Complete");
        
    }

    // Start is called before the first frame update
    void Start()
    {
        uiMeter.SetActive(false);
        tick.enabled = false;
        clock.enabled = true;

        inputWeapon = Weapon.weaponType.Blank;
        inputMet = Metal.metal.Blank;

        spark.Stop();
        outputMet = inputMet;
        outputWeapon = inputWeapon;
        axeModel.SetActive(false);
        swordModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        progressMeter.fillAmount = timer/completionTime;

        if (rangeCheck || hasContents)
        {
            uiMeter.SetActive(true);
        }
        else if (uiMeter.activeSelf && !rangeCheck && (!hasContents && !completed))
        {
            uiMeter.SetActive(false);
        }
        if (timer >= completionTime)
        {
            clock.enabled = false;
            tick.enabled = true;
            completed = true;
        }
        else
        {
            tick.enabled = false;
            clock.enabled = true;
        }
    }


    public override GameObject GiveItem()
    {
        if (outputPrefab != null)
        {

            if (outputMet == Metal.metal.Blank)
            {
                return null;
            }
            else
            {
                var weaponOut = Instantiate(outputPrefab);
                weaponOut.GetComponent<Metal>().myMetal = outputMet;
                weaponOut.GetComponent<Weapon>().myWeapon = outputWeapon;
                timer = 0;

                outputMet = Metal.metal.Blank;
                outputWeapon = Weapon.weaponType.Blank;
                inputMet = Metal.metal.Blank;
                inputWeapon = Weapon.weaponType.Blank;
                completed = false;

                swordModel.SetActive(false);
                axeModel.SetActive(false);
                timer -= completionTime;
                clock.enabled = true;
                tick.enabled = false;
                return weaponOut;
            }
        }
        else
        {
            return null;
        }
    }
    public void SparkPlay()
    {
        spark.gameObject.SetActive(true);
    }

}
