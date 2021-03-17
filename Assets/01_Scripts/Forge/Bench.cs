using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : Tool
{
    GameObject myObject;

    [SerializeField] Transform benchmark = null;
    Vector3 baseScale = Vector3.zero;

    public override GameObject GiveItem()
    {
        if (myObject == null)
        {
            return null;
        }
        else
        {
            myObject.transform.localScale = baseScale;
            return myObject;
        }
    }

    public override void TakeItem(GameObject item)
    {
        myObject = Instantiate(item, benchmark);
        baseScale = myObject.transform.localScale;
        switch (item.GetComponent<Item>().tool)
        {
            case Item.Tool.Furnace:
                myObject.transform.localPosition = Vector3.zero;
                myObject.transform.localScale = myObject.transform.localScale * 1.5f;
                break;

            case Item.Tool.Cast:
                myObject.transform.localPosition = new Vector3 (myObject.transform.localPosition.x, myObject.transform.localPosition.y + 1.5f, myObject.transform.localPosition.z);
                myObject.transform.localScale = myObject.transform.localScale * 1.5f + new Vector3(0, 0.6f, 0);
                break;

            case Item.Tool.Anvil:
                myObject.transform.localPosition = Vector3.zero;
                myObject.transform.localScale = myObject.transform.localScale * 1.5f + new Vector3(0.26f, 0.7f, 0.26f);
                break;

            case Item.Tool.Bucket:
                myObject.transform.localPosition = Vector3.zero;
                myObject.transform.localScale = myObject.transform.localScale * 1.5f + new Vector3(0.39f, 0.39f, 0.39f);
                break;
        }
        myObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
