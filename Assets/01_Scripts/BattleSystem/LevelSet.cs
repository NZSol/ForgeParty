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

    Vector3 position = new Vector3();


    public void SetTargetLevel(int i)
    {
        GameObject god = GameObject.FindWithTag("LevelGod");
        curLevel = (levels)i;
        print(curLevel);
        
        switch (curLevel)
        {
            case levels.level1:
                position = new Vector3(75.8f, 4.7f, -30.4f);
                //Instantiate(Level01, position: position, rotation: Quaternion.Euler(Vector3.zero));
                var level1 = Instantiate(Level01, parent: god.transform);

                break;
            case levels.level2:
                position = new Vector3(1.9f, 1.8f, 19f);
                //Instantiate(Level02, position: position, rotation: Quaternion.Euler(Vector3.zero));
                var level2 = Instantiate(Level02, parent: god.transform);
                level2.transform.localPosition = position;
                break;

            case levels.level3:
                var level3 = Instantiate(Level03, parent: god.transform);
                break;

            case levels.level4:
                var level4 = Instantiate(Level04, parent: god.transform);
                break;
        }
    }
}
