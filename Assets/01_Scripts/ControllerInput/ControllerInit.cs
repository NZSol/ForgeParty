using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerInit : MonoBehaviour
{
    [SerializeField] string AButton;
    [SerializeField] Players player;
    List<Players> playerList = new List<Players>();
    HashSet<Players> playerSet = new HashSet<Players>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(AButton))
        {
            playerSet.Add(player);
        }
    }
}
