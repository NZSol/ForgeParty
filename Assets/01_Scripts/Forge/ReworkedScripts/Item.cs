using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public enum Tool{Furnace, Cast, Anvil, Bucket}
    public Tool tool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Tool desiredTool()
    {
        return tool;
    }

}
