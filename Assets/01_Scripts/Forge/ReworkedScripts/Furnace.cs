using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : Tool
{
    ObjectStore storedObjs;

    float timer;
    public float temperature;

    [SerializeField] GameObject bellows;
    BellowsFunc bellowsFunction;

    Queue<Metal> inputs = new Queue<Metal>();
    Metal curMet;
    Metal output;

    float meltingPoint;

    [SerializeField] GameObject crucible;
    


    // Start is called before the first frame update
    void Start()
    {
        bellowsFunction = bellows.GetComponent<BellowsFunc>();
        storedObjs = gameObject.GetComponent<ObjectStore>();
    }

    // Update is called once per frame
    void Update()
    {

        if (timer <= 0)
        {
            output = curMet;
            storedObjs.outputObj = crucible;
            curMet = null;
            timer = 5;
        }

        //if charging bool in attached component is true, start increasing temperature
        if (temperature >= meltingPoint && storedObjs.outputObj == null)
        {
            timer -= Time.deltaTime;
        }
        if (curMet == null && inputs.Count > 0)
        {
            checkContents();
        }
    }

    void checkContents()
    {
        curMet = inputs.Dequeue();

        //check metal type
        if (curMet.GetComponent<Metal>().metal == Metal.metals.Copper)
        {
            meltingPoint = 10;
        }
        else if (curMet.GetComponent<Metal>().metal == Metal.metals.Tin)
        {
            meltingPoint = 5;
        }
    }

    public override void TakeItem(GameObject item)
    {
        inputs.Enqueue(item.GetComponent<Metal>());
    }

    public override GameObject GiveItem()
    {
        var outputCrucible = Instantiate(outputPrefab);
        outputCrucible.GetComponent<Metal>().metal = output.metal;
        storedObjs.outputObj = null;
        return outputCrucible;
    }
}
