using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BindToPlayer : MonoBehaviour
{

    public void JoinGame()
    {
        PlayerAdd();
    }

    public void LeaveGame()
    {

    }

    List<GameObject> players = new List<GameObject>();
    void PlayerAdd()
    {
        players = GameObject.FindGameObjectsWithTag("Player").ToList();
        foreach (GameObject player in players)
        {
            player.GetComponent<Interaction>().active = true;
        }
    }



}
