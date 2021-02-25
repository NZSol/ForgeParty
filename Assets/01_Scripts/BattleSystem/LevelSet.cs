using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSet : MonoBehaviour
{
    [SerializeField]
    GameObject Level01 = null;
    [SerializeField]
    GameObject Level02 = null;
    [SerializeField]
    GameObject Level03 = null;
    [SerializeField]
    GameObject Level04 = null;
    enum levels { level1, level2, level3, level4}
    levels curLevel;


    public void SetTargetLevel(int i)
    {
        curLevel = (levels)i;
        switch (curLevel)
        {
            case levels.level1:
                Instantiate(Level01);
                break;
            case levels.level2:
                Instantiate(Level02);
                break;

            case levels.level3:
                Instantiate(Level03);
                break;

            case levels.level4:
                Instantiate(Level04);
                break;
        }
    }
}
