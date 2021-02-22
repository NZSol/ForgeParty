using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anvil : Tool
{
    public float timer = 0;
    public float completionTime = 5;

    //OUTPUT ENUM VALUES
    public Metal.metal outputMet = new Metal.metal();
    public Weapon.weaponType outputWeapon = new Weapon.weaponType();
    //INPUT ENUM VALUES
    public Metal.metal inputMet = new Metal.metal();
    public Weapon.weaponType inputWeapon = new Weapon.weaponType();

    [SerializeField] GameObject Sword = null;
    [SerializeField] GameObject Axe = null;

    [SerializeField] Slider _slide = null;

    public ParticleSystem spark;
    public override void TakeItem(GameObject item)
    {
        inputMet = item.GetComponent<Metal>().myMetal;
        inputWeapon = item.GetComponent<Weapon>().myWeapon;


        switch (inputWeapon)
        {
            case Weapon.weaponType.Blank:
                outputPrefab = null;
                break;
            case Weapon.weaponType.Sword:
                outputPrefab = Sword;
                break;
            case Weapon.weaponType.Axe:
                outputPrefab = Axe;
                break;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //assign all enums blank in editor runtime

        if (!UnityEditor.EditorApplication.isPlaying)
        {
            inputWeapon = Weapon.weaponType.Blank;
            inputMet = Metal.metal.Blank;

        }
        spark.Stop();
        outputMet = inputMet;
        outputWeapon = inputWeapon;
        _slide.maxValue = completionTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (charging)
        {
            if (inputWeapon != Weapon.weaponType.Blank)
            {
                timer += Time.deltaTime;
            }

            if (timer >= completionTime)
            {
                outputMet = inputMet;
                outputWeapon = inputWeapon;

                inputMet = Metal.metal.Blank;
                inputWeapon = Weapon.weaponType.Blank;
                timer -= completionTime;
                charging = false;
            }
        }

        _slide.value = timer;
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

                outputMet = Metal.metal.Blank;
                outputWeapon = Weapon.weaponType.Blank;

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
