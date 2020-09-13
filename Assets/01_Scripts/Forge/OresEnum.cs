using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OresEnum : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] enum OreFlags {Copper, Tin};



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    OreFlags assignment(OreFlags ore)
    {
        if (ore == OreFlags.Copper)
        {
            ore = OreFlags.Copper;
        }
        else if (ore == OreFlags.Tin)
        {
            ore = OreFlags.Tin;
        }

        return ore;
    }
}
