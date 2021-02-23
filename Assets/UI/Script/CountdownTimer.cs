using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{

    public GameObject textDisplay;
    public int secondsLeft = 10;
    public bool takingAway = false;
    public bool addseconds = false;



    private void Start()
    {
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
    }
    private void Update()
    {
        if (takingAway == false && secondsLeft > 0)
        {

            StartCoroutine(TimerTake());

        }
       if( Input.GetKeyDown(KeyCode.Space))
        {
            GiveSeconds();
        }
        
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if (secondsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;
        }
        else
        {
            textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
            
        }
        takingAway = false;
    }


    public void GiveSeconds()
    {
        addseconds = true;
        secondsLeft += 10;
        addseconds = false;

    }
}
