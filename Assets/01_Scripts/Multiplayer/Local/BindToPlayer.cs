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

    [SerializeField] List<GameObject> InputObjs = new List<GameObject>();

    bool primed = true;
    [SerializeField] GameObject homeScreen = null;

    [SerializeField]
    GameObject[] JoinInstructions = null;

    [SerializeField] GameObject charIconParent = null;
    [SerializeField] GameObject charSelectIcon = null;

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
            InputObjs.Clear();
        }
    }

    private void OnDisable()
    {
        if (curScene != titleScene)
        {
            disableJoin();
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
    }

    public void JoinGame(PlayerInput input)
    {
        players.Add(input.gameObject);
        var i = players.Count;
        input.gameObject.GetComponent<PlayerThroughput>().SetInput(input);
        input.gameObject.GetComponent<PlayerThroughput>().playerIndex = i;
        DontDestroyOnLoad(input.gameObject);
        input.gameObject.GetComponent<PlayerThroughput>().canAct = true;
        var character = Instantiate(charSelectIcon, charIconParent.transform);
        input.gameObject.GetComponent<CharSelect>().AssignSelect(character.GetComponent<PlayerSelectSetup>());
        InputObjs.Add(character);
        EnableButton();
    }

    private void Update()
    {

        if (SceneManager.GetActiveScene() == titleScene)
        {
            if (LevelSelect.instance.allPlayersReady)
            {
                gameObject.GetComponent<FirstSelect>().SetBtn();
            }
        }
    }


    void EnableButton()
    {
        for (int i = 0; i < players.Count; i++)
        {
            InputObjs[i].SetActive(true);
            if (players.Count == InputObjs.Count)
            {
                foreach (GameObject obj in JoinInstructions)
                {
                    obj.SetActive(false);
                }
            }
        }    
    }

    void resetPanel()
    {
        foreach (GameObject obj in InputObjs)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in JoinInstructions)
        {
            obj.SetActive(true);
        }
    }

    public void disableJoin()
    {
        Events.GetComponent<PlayerInputManager>().DisableJoining();
    }


    public void Cancel()
    {
        if (curScene == titleScene && gameObject.tag == "CurScene")
        {
            resetPanel();
            homeScreen.SetActive(true);
            Events.GetComponent<PlayerInputManager>().DisableJoining();
            Events.GetComponent<InputSystemUIInputModule>().enabled = false;
            Events.GetComponent<InputSystemUIInputModule>().enabled = true;
            gameObject.SetActive(false);
            GameObject[] players = GameObject.FindGameObjectsWithTag("Container");
            foreach (GameObject player in players)
            {
                Destroy(player);
            }
        }
    }

    public void LeaveGame(PlayerInput input)
    {
        players.Remove(input.gameObject);
        Destroy(input.gameObject);
    }


}
