using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAssignment : MonoBehaviour
{

    [SerializeField] Material tin;
    [SerializeField] Material copper;
    [SerializeField] Material bronze;

    [SerializeField] Renderer render;
    [SerializeField] Material curMat;

    public Metal.metal myMetal;


    // Start is called before the first frame update
    void Start()
    {
        myMetal = GetComponent<Metal>().myMetal;
        render = gameObject.GetComponentInChildren<Renderer>();

        switch (myMetal)
        {
            case Metal.metal.Bronze:
                curMat = bronze;
                break;

            case Metal.metal.Tin:
                curMat = tin;
                break;

            case Metal.metal.Copper:
                curMat = copper;
                break;
        }

        render.GetComponent<Renderer>().material = curMat;
    }
}
