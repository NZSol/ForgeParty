using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceTemperature : MonoBehaviour
{
    public float MaxTemp = 100;

    public float temp;

    public Bellows _bellowScript;
    public ForgeContents fc;

    [SerializeField] [Range(0f, 1f)] float lerpTime;

    float startVal;
    float endVal;
    Vector3 ScaleVal;
    private void Start()
    {
        startVal = transform.localScale.y;
        ScaleVal = transform.localScale;
        endVal = 2.87f;
        

    }

    // Update is called once per frame
    void Update()
    {

     //   MaxTemp = fc.maxTemp;
     

    //    if (lerpTime < MaxTemp && fc.present == true)
        {
            float LerpVal = Mathf.Lerp(startVal, endVal, _bellowScript.Temperature / MaxTemp);
            transform.localScale = new Vector3(ScaleVal.x, LerpVal, ScaleVal.z);
            lerpTime += Time.deltaTime;
        }

    }
}
