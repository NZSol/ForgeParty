using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorOvertime : MonoBehaviour
{
    public float totaltime;
    SpriteRenderer spriterenderer;
    [SerializeField] [Range(0f, 10f)] float lerpTime;
    public Color StartColor;
    public Color EndColor;
    public Canvas WeaponRequest;
    [SerializeField] Slider _slide;
    [SerializeField] Image _img;

    private void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        _slide.maxValue = totaltime;
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponRequest.enabled==true)
        {
            _img.color = Color.Lerp(StartColor, EndColor, _slide.value / totaltime);

            lerpTime += Time.deltaTime;

            _slide.value = lerpTime;


            lerpTime = Mathf.Clamp(lerpTime, 0, totaltime);
        }

        if(WeaponRequest.enabled==false)
        {
            _slide.value = 0;
        }
    }


    
}


//This controls the color of the slider over time , allowing us to comunicate to the player that good and bad things 
