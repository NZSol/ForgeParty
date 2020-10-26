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
    [SerializeField]
    private PlayerJoinHandler join;

    public int playerCounter = 0;

    [SerializeField] GameObject Events;
    
    Scene curScene;
    Scene titleScene;

    [SerializeField] GameObject[] InputObjs;



    private void Start()
    {
    }

    private void OnEnable()
    {
        join.SetPlayerBind(this);
        titleScene = SceneManager.GetActiveScene();
        if (titleScene == SceneManager.GetActiveScene())
        {
            Events.GetComponent<PlayerInputManager>().EnableJoining();
            GetComponent<FirstSelect>().deselect();
            InputObjs = GameObject.FindGameObjectsWithTag("Button");
            print("active");
            print((titleScene == SceneManager.GetActiveScene()) + " Active Scene");
        }
    }

    private void OnDisable()
    {
        Events.GetComponent<PlayerInputManager>().DisableJoining();
        foreach (GameObject obj in players)
        {
            Destroy(obj);
        }
    }
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
        players.Add(input.gameObject);
        ++playerCounter;
        DontDestroyOnLoad(input.gameObject);
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneAt(0))
        {
            for (int i = 0; i < playerCounter; i++)
            {

            } 
        }

    }
    private void Update()
    {
        if (players.Count == InputObjs.Length)
        {
            gameObject.GetComponent<FirstSelect>().SetBtn();
        }

        print(players.Count + "    " + InputObjs.Length);
    }

    public void LeaveGame(PlayerInput input)
    {
        Destroy(input.gameObject);
    }


}
