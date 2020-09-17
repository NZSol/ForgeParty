using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOvertime : MonoBehaviour
{
    public float totaltime;
    SpriteRenderer spriterenderer;
    [SerializeField] [Range(0f, 1f)] float lerpTime;
    public Color StartColor;
    public Color EndColor;

 

    private void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriterenderer.color = Color.Lerp(StartColor, EndColor, lerpTime/totaltime);

        lerpTime += Time.deltaTime;
        

       
    }
}
