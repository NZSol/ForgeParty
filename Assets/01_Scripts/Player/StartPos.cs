using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour
{
    public GameObject[] playerArray;
    public GameObject[] spawns;

    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Spawns");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Positioning(GameObject obj)
    {
        playerArray = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < playerArray.Length; i++)
        {
            playerArray[i].transform.position = spawns[i].transform.position;
        }
    }
}
