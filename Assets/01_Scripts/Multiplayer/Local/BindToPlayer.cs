using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class BindToPlayer : MonoBehaviour
{
    [SerializeField] GameObject[] spawns;
    public List<GameObject> players = new List<GameObject>();

    public int playerCounter = 0;

    private void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Spawns");
    }
    public void JoinGame(PlayerInput input)
    {

        PlayerAdd(input);
        ++playerCounter;

        DontDestroyOnLoad(input.gameObject);

    }

    

    public void LeaveGame(PlayerInput input)
    {
        Destroy(input.gameObject);
    }

    void PlayerAdd(PlayerInput input)
    {
        var mostRecentPlayer = input.gameObject;

        mostRecentPlayer.transform.position = spawns[playerCounter].transform.position;

        players.Add(GameObject.FindWithTag("Player"));

    }



}
