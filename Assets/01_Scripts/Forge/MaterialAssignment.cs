using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAssignment : MonoBehaviour
{

    [SerializeField] Material tin = null;
    [SerializeField] Material copper = null;
    [SerializeField] Material bronze = null;

    [SerializeField] Renderer render = null;
    [SerializeField] Material curMat = null;

    public Metal.metal myMetal = new Metal.metal();


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
