using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Casting : MonoBehaviour
{
    public bool axe;
    public bool sword;

    public Slider _slide;

    public bool Bronze;
    public bool Tin;
    public bool Copper;

    
    public float coolDownTimer;
    public float readyTimer;

    [SerializeField] GameObject bronzeObj;
    [SerializeField] GameObject copperObj;
    [SerializeField] GameObject tinObj;

    public GameObject outputObj;

    private void Start()
    {
        if (axe)
        {
            _slide = GameObject.Find("Canvas/AxeCast").GetComponent<Slider>();
        }
        if (sword)
        {
            _slide = GameObject.Find("Canvas/SwordCast").GetComponent<Slider>();
        }
    }

    void Update()
    {
        if (Copper)
        {
            coolDownTimer += Time.deltaTime;
        }
        if (Tin)
        {
            coolDownTimer += Time.deltaTime;
        }
        if (Bronze)
        {
            coolDownTimer += Time.deltaTime;
        }

        _slide.value = coolDownTimer;

        if (coolDownTimer > readyTimer)
        {
            if (Bronze)
            {
                outputObj = bronzeObj;
                Bronze = false;
            }
            if (Tin)
            {
                outputObj = tinObj;
                Tin = false;
            }
            if (Copper)
            {
                outputObj = copperObj;
                Copper = false;
            }

            coolDownTimer = 0;
        }
    }
}
