using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCheck : MonoBehaviour
{
    void Start()
    {
        LevelSelect.instance.playersReset();
        print("Hit");
    }
}

