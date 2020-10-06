using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    [SerializeField]
    protected GameObject outputPrefab;


    public abstract void TakeItem(GameObject item);
    public abstract GameObject GiveItem();
}
