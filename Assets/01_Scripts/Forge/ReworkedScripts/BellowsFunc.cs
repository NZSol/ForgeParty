using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BellowsFunc : MonoBehaviour
{
    timerScript timer;

    Slider _slide;
    public float temperature;
    Furnace furnace;

    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.GetComponent<timerScript>();
        furnace = GetComponentInParent<Furnace>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.charge)
        {
            temperature += Time.deltaTime;
        }
        else
        {
            if (temperature > 0)
            {
                temperature -= Time.deltaTime / 3;
            }
        }

        furnace.temperature = temperature;
        _slide.value = temperature;
    }
}
