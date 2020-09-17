using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeOverTime : MonoBehaviour
{
    public float totaltime;

    [SerializeField] [Range(0f, 1f)] float lerpTime;

    float startVal;
    float endVal;
    Vector3 ScaleVal;
    private void Start()
    {
        startVal = transform.localScale.y;
        ScaleVal = transform.localScale;
        endVal = -844.2933f;
    }

    // Update is called once per frame
    void Update()
    {
       

     

        if (lerpTime < totaltime)
        {
            float LerpVal = Mathf.Lerp(startVal, endVal, lerpTime / totaltime);
            transform.localScale = new Vector3(ScaleVal.x, LerpVal, ScaleVal.z);
            lerpTime += Time.deltaTime;
        }

    }
}
