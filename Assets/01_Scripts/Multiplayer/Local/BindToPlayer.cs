using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BindToPlayer : MonoBehaviour
{
    [SerializeField] GameObject[] spawns;
    public List<GameObject> players = new List<GameObject>();

    public int playerCounter = 0;

    [SerializeField] Image[] imgArray;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Spawns") != null)
        {
            spawns = GameObject.FindGameObjectsWithTag("Spawns");
        }
        else
        {
            Debug.Log("Spawns Null");
        }


        print("waking");
    }

    public void JoinGame(PlayerInput input)
    {

        //PlayerAdd(input);
        ++playerCounter;
        DontDestroyOnLoad(input.gameObject);
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneAt(0))
        {
            

            for (int i = 0; i < playerCounter; i++)
            {

            } 
        }

    }

    

    public void LeaveGame(PlayerInput input)
    {
        Destroy(input.gameObject);
    }

    //void PlayerAdd(PlayerInput input)
    //{
    //    var mostRecentPlayer = input.gameObject;

    //    if (spawns.Length != 0)
    //    {
    //        mostRecentPlayer.transform.position = spawns[playerCounter].transform.position;
    //    }

    //    players.Add(GameObject.FindWithTag("Player"));

    //}



}
