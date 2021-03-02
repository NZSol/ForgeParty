using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFrameRate : MonoBehaviour
{
    public enum frameRate { Low, Low_Med, Med_High, High, Unlimited}
    public frameRate frames = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        switch (frames)
        {
            case frameRate.Low:
                Application.targetFrameRate = 19;
                break;
            case frameRate.Low_Med:
                Application.targetFrameRate = 30;
                break;
            case frameRate.Med_High:
                Application.targetFrameRate = 45;
                break;
            case frameRate.High:
                Application.targetFrameRate = 60;
                break;
            case frameRate.Unlimited:
                Application.targetFrameRate = -1;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
