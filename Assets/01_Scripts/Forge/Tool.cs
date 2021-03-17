using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    [SerializeField]
    protected GameObject outputPrefab;
    public bool charging;
    public enum curTool { Ore, Furnace, Cast, Anvil, Bucket, Bench, Bin}
    public curTool tool;

    public bool hasContents = false;
    public bool completed = false;
    public float timer = 0;
    public float completionTime = 5;

    public abstract void TakeItem(GameObject item);

    public abstract GameObject GiveItem();

    public curTool currentTool()
    {
        return tool;
    }



}
