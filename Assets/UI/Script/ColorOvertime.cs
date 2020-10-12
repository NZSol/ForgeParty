using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorOvertime : MonoBehaviour
{
    public float totaltime;
    public Color StartColor;
    public Color EndColor;
    public Canvas WeaponRequest;
    public Slider _slide;
    [SerializeField] Image _img;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponRequest.enabled==true)
        {
            _img.color = Color.Lerp(StartColor, EndColor, _slide.value / totaltime);

            totaltime = gameObject.GetComponentInParent<NpcRequest>().timerMax;
        }
    }


    
}


//This controls the color of the slider over time , allowing us to comunicate to the player that good and bad things 
