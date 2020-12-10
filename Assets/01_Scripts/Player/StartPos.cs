using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour
{
    public GameObject[] playerArray = new GameObject[0];
    public GameObject[] spawns = new GameObject[0];
    public Color[] colors = new Color[0];

    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Spawns");
    }



    public void Positioning(GameObject obj)
    {
        playerArray = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < playerArray.Length; i++)
        {
            playerArray[i].transform.position = spawns[i].transform.position;
            playerArray[i].transform.position = new Vector3(playerArray[i].transform.position.x, 0, playerArray[i].transform.position.z);

            playerArray[0].gameObject.GetComponent<Outline>().OutlineColor = colors[0];
        }
    }
}
