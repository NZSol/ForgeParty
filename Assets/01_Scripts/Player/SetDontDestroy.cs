using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDontDestroy : MonoBehaviour
{
    GameObject[] players = null;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Container");
    }

    public void DontDestroy()
    {
         foreach(GameObject chara in players)
        {
            DontDestroyOnLoad(chara);
            chara.GetComponent<PlayerThroughput>().spawned = false;
        }
    }
}
