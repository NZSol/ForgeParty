﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureSlider : MonoBehaviour
{
    [SerializeField] Slider _slide;
    [SerializeField] Image _img;

    public float colorLerp = 0;

    // Start is called before the first frame update
    void Start()
    {
        _slide.maxValue = 6;
        _slide.value = 0;
        _img.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        _slide.value = Mathf.Clamp(_slide.value, 0, _slide.maxValue);
    }
}
