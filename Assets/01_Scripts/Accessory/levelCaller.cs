using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelCaller : MonoBehaviour
{
    public void ChangeLevelNumber(int val)
    {
        LevelSelect.instance.AssignLevelValue(val - 1);
    }
}
