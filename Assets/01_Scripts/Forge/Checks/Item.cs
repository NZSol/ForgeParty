using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public enum Tool{Furnace, Cast, Anvil, Bucket}
    public Tool tool;

    public Tool desiredTool()
    {
        return tool;
    }

}
