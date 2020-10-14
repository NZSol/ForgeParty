using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    [SerializeField]
    protected GameObject outputPrefab;
    public bool charging;
    public enum curTool { Ore, Furnace, Cast, Anvil, Bucket}
    public curTool tool;



    public abstract void TakeItem(GameObject item);
    //public abstract void Use();
    public abstract GameObject GiveItem();

    public curTool currentTool()
    {
        return tool;
    }



}
