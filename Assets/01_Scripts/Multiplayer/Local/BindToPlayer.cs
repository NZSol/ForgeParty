using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class BindToPlayer : MonoBehaviour
{
    [SerializeField] GameObject[] spawns = null;
    public List<GameObject> players = new List<GameObject>();
    [SerializeField]
    private PlayerJoinHandler join = null;


    public GameObject Events = null;

    Scene curScene;
    Scene titleScene;

    [SerializeField] GameObject[] InputObjs = new GameObject[0];

    bool primed = true;
    [SerializeField] GameObject homeScreen = null;

    InputSystemUIInputModule uiInput = null;


    private void OnEnable()
    {

        titleScene = SceneManager.GetSceneByBuildIndex(0);
        curScene = SceneManager.GetActiveScene();


        if (curScene != SceneManager.GetActiveScene())
        {
            curScene = SceneManager.GetActiveScene();
        }


        if (curScene == titleScene)
        {
            join.SetPlayerBind(this);
            Events.GetComponent<PlayerInputManager>().EnableJoining();
            GetComponent<FirstSelect>().deselect();
            foreach (GameObject obj in players)
            {
                Destroy(obj);
            }

            players.Clear();
        }
    }

    private void OnDisable()
    {
        if (curScene != titleScene)
        {
            Events.GetComponent<PlayerInputManager>().DisableJoining();
        }
    }

    private void Awake()
    {
        uiInput = Events.GetComponent<InputSystemUIInputModule>();
        if (GameObject.FindGameObjectsWithTag("Spawns") != null)
        {
            spawns = GameObject.FindGameObjectsWithTag("Spawns");
        }
        else
        {
            Debug.Log("Spawns Null");
        }
    }

    public void JoinGame(PlayerInput input)
    {
        players.Add(input.gameObject);
        input.gameObject.GetComponent<PlayerThroughput>().SetInput(input);
        DontDestroyOnLoad(input.gameObject);
    }

    private void Update()
    {

        if (SceneManager.GetActiveScene() == titleScene)
        {
            if (players.Count >= 1)
            {
                Debug.Log(players.Count + "    " + InputObjs.Length);
            }
            if (players.Count == InputObjs.Length)
            {
                gameObject.GetComponent<FirstSelect>().SetBtn();
            }
        }
    }


    public void Cancel()
    {
        if (curScene == titleScene && gameObject.tag == "CurScene")
        {
            homeScreen.SetActive(true);
            Events.GetComponent<PlayerInputManager>().DisableJoining();
            Events.GetComponent<InputSystemUIInputModule>().enabled = false;
            Events.GetComponent<InputSystemUIInputModule>().enabled = true;
            gameObject.SetActive(false);
        }
    }

    public void LeaveGame(PlayerInput input)
    {
        players.Remove(input.gameObject);
        Destroy(input.gameObject);
    }


}
