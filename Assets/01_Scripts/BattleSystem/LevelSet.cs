using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSet : MonoBehaviour
{
    [Range(1,4)]
    public int DesiredLvl = 0;
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
    GameObject CurLvl = null;

    [SerializeField]
    Camera cam;
    Vector3 camPos;

    [SerializeField]
    GameObject intSet = null;

    private void Start()
    {
        LevelSelect.instance.setLevel();
    }

    public void Update()
    {
        if (CurLvl == null)
        {
            SetTargetLevel(DesiredLvl - 1);
        }
    }

    public void SetTargetLevel(int i)
    {
        GameObject god = GameObject.FindWithTag("LevelGod");
        curLevel = (levels)i;
        print(curLevel);
        
        switch (curLevel)
        {
            case levels.level1:
                position = new Vector3(75.8f, 4.7f, -30.4f);
                camPos = new Vector3(75.7f, 86.1f, -107.5f);

                CurLvl = Instantiate(Level01, parent: god.transform);

                cam.transform.position = camPos;
                break;

            case levels.level2:
                position = new Vector3(1.9f, 1.8f, 4.4f);
                camPos = new Vector3(75.7f, 86.1f, -107.5f);

                CurLvl = Instantiate(Level02, parent: god.transform);

                CurLvl.transform.localPosition = position;
                cam.transform.position = camPos;
                break;

            case levels.level3:
                position = new Vector3(-66.5f, 0.2f, 6.9f);
                camPos = new Vector3(8.4f, 87.8f, -140.9f);

                CurLvl = Instantiate(Level03, parent: god.transform);

                CurLvl.transform.localPosition = position;
                cam.transform.position = camPos;
                break;

            case levels.level4:
                position = new Vector3(18.4f, 1.8f, 14.7f);
                camPos = new Vector3(91.2f, 91.7f, -114.9f);

                CurLvl = Instantiate(Level04, parent: god.transform);

                CurLvl.transform.localPosition = position;
                cam.transform.position = camPos;
                break;
        }
    }
}
