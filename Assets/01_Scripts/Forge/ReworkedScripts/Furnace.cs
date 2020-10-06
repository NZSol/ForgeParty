using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Furnace : Tool
{
    ObjectStore storedObjs;

    public float timer = 5;
    public float temperature;

    [SerializeField] GameObject bellows;
    BellowsFunc bellowsFunction;

    public Queue<Metal.metal> inputs = new Queue<Metal.metal>();
    public Metal.metal activeMetal;
    public Metal.metal outputMet;

    public float meltingPoint;

    [SerializeField] GameObject crucible;

    public List<Metal.metal> displayQueue = new List<Metal.metal>();

    // Start is called before the first frame update
    void Start()
    {
        bellowsFunction = bellows.GetComponent<BellowsFunc>();
        storedObjs = gameObject.GetComponent<ObjectStore>();
        activeMetal = Metal.metal.Blank;
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
            if (temperature >= meltingPoint)
            {
                timer -= Time.deltaTime;
            }

            if (outputMet == Metal.metal.Blank && inputs.Count > 0)
            {
                activeMetal = inputs.Dequeue();
                checkContents();
                foreach (Metal.metal metal in inputs)
                {
                    displayQueue.Add(metal);
                }

            }
        }


        if (timer <= 0)
        {
            outputMet = activeMetal;
            outputPrefab = crucible;
            activeMetal = Metal.metal.Blank;
            timer = 5;
        }

    }

    void checkContents()
    {

        //check metal type
        if (activeMetal == Metal.metal.Copper)
        {
            meltingPoint = 10;
        }
        else if (activeMetal == Metal.metal.Tin)
        {
            meltingPoint = 5;
        }
    }


    public override GameObject GiveItem()
    {
        var outputCrucible = Instantiate(outputPrefab);
        outputCrucible.GetComponent<Metal>().myMetal = outputMet;
        outputMet = Metal.metal.Blank;
        return outputCrucible;
    }
}
