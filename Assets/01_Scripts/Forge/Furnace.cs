using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Furnace : Tool
{
    public float timer = 0;
    public float maxTimer = 5;
    public float temperature = 0;

    public Queue<Metal.metal> inputs = new Queue<Metal.metal>();
    public Metal.metal activeMetal = new Metal.metal();
    public Metal.metal outputMet = new Metal.metal();

    public float meltingPoint = 0;

    [SerializeField] GameObject crucible = null;

    public List<Metal.metal> displayQueue = new List<Metal.metal>();

    [SerializeField] ParticleSystem smoke = null;
    bool playSmoke = false, canActivate = true;


    // Start is called before the first frame update
    void Start()
    {
        activeMetal = Metal.metal.Blank;
        smoke.Stop();
    }


    public override void TakeItem(GameObject item)
    {
        inputs.Enqueue(item.GetComponent<Metal>().myMetal);
        activeMetal = item.GetComponent<Metal>().myMetal;
    }


    // Update is called once per frame
    void Update()
    {
        //if charging bool in attached component is true, start increasing temperature
        if (activeMetal != Metal.metal.Blank)
        {

            if (outputMet == Metal.metal.Blank && inputs.Count > 0)
            {
                activeMetal = inputs.Dequeue();
                checkContents();
                foreach (Metal.metal metal in inputs)
                {
                    displayQueue.Add(metal);
                }
            }

            if (temperature >= meltingPoint)
            {
                timer += Time.deltaTime;
            }
        }
        if (timer < maxTimer && timer > 0 && canActivate)
        {
            playSmoke = true;
            canActivate = false;
        }

        if (timer >= maxTimer)
        {
            outputMet = activeMetal;
            outputPrefab = crucible;
            activeMetal = Metal.metal.Blank;

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

    void checkContents()
    {
        //check metal type
        if (activeMetal == Metal.metal.Copper)
        {
            meltingPoint = 6;
        }
        else if (activeMetal == Metal.metal.Tin)
        {
            meltingPoint = 4;
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
            var outputCrucible = Instantiate(outputPrefab);
            outputCrucible.GetComponent<Metal>().myMetal = outputMet;
            outputMet = Metal.metal.Blank;
            return outputCrucible;
        }
    }
}
