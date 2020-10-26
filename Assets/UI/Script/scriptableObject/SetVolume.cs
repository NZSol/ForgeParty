using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    private static SetVolume instance;
    public static SetVolume Instance { get { return Instance; } }
    void Awake()
    {
        instance = this;
    }


    [SerializeField] private Settings settings;

    public Settings Settings { get { return settings; } }

   
    
}
