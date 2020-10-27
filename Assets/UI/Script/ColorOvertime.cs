using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorOvertime : MonoBehaviour
{
    public float totaltime = 0;
    public Color StartColor = new Color();
    public Color EndColor = new Color();
    public Canvas WeaponRequest = null;
    public Slider _slide = null;
    [SerializeField] Image _img = null;

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
