using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Furnace : Tool
{
    [SerializeField] float maxTimer = 5;
    [SerializeField] float temperature = 0;

    public Queue<Metal.metal> inputs = new Queue<Metal.metal>();
    public Metal.metal activeMetal = new Metal.metal();
    public Metal.metal secondMetal = new Metal.metal();
    public Metal.metal outputMet = new Metal.metal();

    public float meltingPoint = 0;

    [SerializeField] GameObject crucible = null;

    public List<Metal.metal> displayQueue = new List<Metal.metal>();

    [SerializeField] ParticleSystem smoke = null;
    bool playSmoke = false, canActivate = true;

    //Fire Particle
    [SerializeField] ParticleSystem fire = null;
    Vector3 startSize = new Vector3(0, 0, 0);
    Vector3 endSize = new Vector3(1.5f, 1.5f, 1.5f);

    //Temperature Management
    [SerializeField] Slider _slide = null;
    [SerializeField] float coolingMultiplier = 1;

    //Input Metals
    [SerializeField] Image met1 = null;
    [SerializeField] Image met2 = null;

    //Timer Assets
    [SerializeField] Image timeToReadyGuage = null;
    [SerializeField] Image check = null;
    [SerializeField] Image clock = null;
    [SerializeField] Image borderVal = null;
    public bool rangeCheck = false;
    bool displayCheck = false;

    //Colours for Metals
    [SerializeField] Color copperCol;
    [SerializeField] Color tinCol;
    [SerializeField] Color bronzeCol;
    [SerializeField] Color defaultCol;

    // Start is called before the first frame update
    void Start()
    {
        activeMetal = Metal.metal.Blank;
        smoke.Stop();
        endSize = fire.gameObject.transform.localScale;
        fire.transform.localScale = startSize;
        _slide.maxValue = 10;
    }

    public override void TakeItem(GameObject item)
    {
        inputs.Enqueue(item.GetComponent<Metal>().myMetal);

        if (activeMetal == Metal.metal.Blank)
        {
            if (secondMetal == Metal.metal.Blank)
            {
                activeMetal = inputs.Dequeue();
            }
            else
            {
                activeMetal = secondMetal;
            }
            checkContents();
        }
        else
        {
            if (secondMetal == Metal.metal.Blank)
            {
                secondMetal = inputs.Dequeue();
            }
        }
        SetColours();

        if (secondMetal != Metal.metal.Blank)
        {
            checkAlloy();
        }
        displayQueue = inputs.ToList();
    }
    


    // Update is called once per frame
    void Update()
    {
        if (activeMetal != Metal.metal.Blank || secondMetal != Metal.metal.Blank)
        {
            hasContents = true;
        }
        else
        {
            hasContents = false;
        }
        if (activeMetal != Metal.metal.Blank)
        {
            displayCheck = true;
        }
        else
        {
            displayCheck = false;
        }

        if (displayCheck || rangeCheck)
        {
            timeToReadyGuage.gameObject.SetActive(true);
        }
        else
        {
            timeToReadyGuage.gameObject.SetActive(false);
        }

        _slide.value = temperature;
        fire.transform.localScale = Vector3.Lerp(startSize, endSize, (temperature / _slide.maxValue));

        if (activeMetal != Metal.metal.Blank)
        {
            if (temperature >= meltingPoint && outputMet == Metal.metal.Blank)
            {
                timer += Time.deltaTime;
                borderVal.fillAmount = timer / maxTimer;
                check.enabled = false;
                clock.enabled = true;
            }
        }
        if (timer < maxTimer && timer > 0 && canActivate)
        {
            playSmoke = true;
            canActivate = false;
        }

        if (timer >= maxTimer && outputMet == Metal.metal.Blank)
        {
            clock.enabled = false;
            check.enabled = true;

            outputMet = activeMetal;
            outputPrefab = crucible;

            timer = 0;
            smoke.Stop();
            canActivate = true;
        }

        if (playSmoke == true)
        {
            smoke.Play();
            playSmoke = false;
        }
    }

    void SetColours()
    {
        switch (activeMetal)
        {
            case Metal.metal.Copper:
                met1.color = copperCol;
                break;
            case Metal.metal.Tin:
                met1.color = tinCol;
                break;
            case Metal.metal.Bronze:
                met1.color = bronzeCol;
                break;
            case Metal.metal.Blank:
                met1.color = defaultCol;
                break;
        }
        switch (secondMetal)
        {
            case Metal.metal.Copper:
                met2.color = copperCol;
                break;
            case Metal.metal.Tin:
                met2.color = tinCol;
                break;
            case Metal.metal.Bronze:
                met2.color = bronzeCol;
                break;
            case Metal.metal.Blank:
                met2.color = defaultCol;
                break;

        }
    }
    private void FixedUpdate()
    {
        //if charging bool in attached component is true, start increasing temperature
        if (charging)
        {
            if (temperature < _slide.maxValue)
            {
                temperature += Time.deltaTime * 4;
            }
        }
        else
        {
            if (temperature > 0)
            {
                temperature -= Time.deltaTime / coolingMultiplier;
            }
        }
    }

    void checkContents()
    {
        //check metal type
        switch (activeMetal)
        {
            case Metal.metal.Copper:
                meltingPoint = 6;
                break;
            case Metal.metal.Tin:
                meltingPoint = 4;
                break;
            case Metal.metal.Bronze:
                meltingPoint = 8;
                break;
        }
    }

    void checkAlloy()
    {
        if(secondMetal != activeMetal)
        {
            switch (activeMetal)
            {
                case Metal.metal.Copper:
                    switch (secondMetal)
                    {
                        case Metal.metal.Tin:
                            activeMetal = Metal.metal.Bronze;
                            secondMetal = Metal.metal.Blank;
                            break;
                    }

                    break;

                case Metal.metal.Tin:
                    switch (secondMetal)
                    {
                        case Metal.metal.Copper:
                            activeMetal = Metal.metal.Bronze;
                            secondMetal = Metal.metal.Blank;
                            break;
                    }
                    break;
            }
            checkContents();
            SetColours();
        }
    }


    public override GameObject GiveItem()
    {
        if (outputMet == Metal.metal.Blank)
        {
            return null;
        }
        else
        {
            timer = 0;
            borderVal.fillAmount = 0;
            check.enabled = false;
            clock.enabled = true;

            activeMetal = secondMetal;
            if (inputs.Count > 0)
            {
                secondMetal = inputs.Dequeue();
            }
            else
            {
                secondMetal = Metal.metal.Blank;
            }
            checkAlloy();
            SetColours();


            var outputCrucible = Instantiate(outputPrefab);
            outputCrucible.GetComponent<Metal>().myMetal = outputMet;
            outputMet = Metal.metal.Blank;
            displayQueue = inputs.ToList();
            return outputCrucible;


        }
    }
}
