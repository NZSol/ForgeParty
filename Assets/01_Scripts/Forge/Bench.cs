using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : Tool
{
    GameObject myObject;

    [SerializeField] Vector3 parentPos = Vector3.zero;

    public override GameObject GiveItem()
    {
        if (myObject == null)
        {
            return null;
        }
        else
        {
            return myObject;
        }
    }

    public override void TakeItem(GameObject item)
    {
        myObject = Instantiate(item);
    }
}
