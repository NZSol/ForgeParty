using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : Tool
{
    public override GameObject GiveItem()
    {
        return null;
    }

    public override void TakeItem(GameObject item)
    {
        Destroy(item);
    }

}
