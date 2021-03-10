using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerThroughput : MonoBehaviour
{
    public bool canAct = false;

    public GameObject myPlayer = null;
    public CharSelect.skin mySkin = 0;

    Movement moveScript;

    Interact interactScript;
    public CharSelect CharSelectScript;

    [SerializeField] Scene curScene;
    [SerializeField] Scene titleScene;

    StartPos position;

    [SerializeField] GameObject eventSystem = null;

    PlayerInput input;
    bool active = true;

    GameObject PlayerChar = null;
    public bool spawned = false;

    public int playerIndex = 0;
    public bool ready = false;

    private void Start()
    {
        StartCoroutine(DelayedStart());
        spawned = false;
        CharSelectScript = GetComponent<CharSelect>();
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.5f);
        curScene = SceneManager.GetActiveScene();
        titleScene = SceneManager.GetSceneByBuildIndex(0);
        DontDestroyOnLoad(this.gameObject);

        if (curScene.buildIndex == titleScene.buildIndex)
        {
            eventSystem = GameObject.FindWithTag("Event");

        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.GetSceneByBuildIndex(0).buildIndex)
        {
            if (ready && !LevelSelect.instance.GetPlayerReady(playerIndex))
            {
                LevelSelect.instance.SetPlayerReady(playerIndex);
            }
            if (!ready && LevelSelect.instance.GetPlayerReady(playerIndex))
            {
                LevelSelect.instance.SetPlayerUnready(playerIndex);
            }
        }

        if (PlayerChar == null)
        {
            if (SceneManager.GetActiveScene().buildIndex == SceneManager.GetSceneByBuildIndex(2).buildIndex && !spawned)
            {
                spawned = true;
                StartCoroutine(DelayedUpdate());
            }
        }
    }
    IEnumerator DelayedUpdate()
    {
        yield return new WaitForSeconds(0.5f);
        curScene = SceneManager.GetActiveScene();
        position = GameObject.FindWithTag("LevelGod").GetComponent<StartPos>();

        PlayerJoin();
    }

    public void PlayerJoin()
    {
        SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
        eventSystem = GameObject.FindWithTag("Event");
        PlayerChar = Instantiate(myPlayer);
        PlayerChar.GetComponent<CharSelect>().mySkin = mySkin;
        moveScript = PlayerChar.GetComponent<Movement>();
        interactScript = PlayerChar.GetComponent<Interact>();
        interactScript.active = true;
        position.Positioning(PlayerChar);
    }

    public void readMove (CallbackContext context)
    {
        if (curScene.buildIndex == SceneManager.GetSceneByBuildIndex(0).buildIndex)
        {
            if (!ready)
            {
                if (context.ReadValue<Vector2>().x == 1 && active)
                {
                    active = false;
                    CharSelectScript.ChangeCharRight();
                    print("hitting Right");
                }
                else if (context.ReadValue<Vector2>().x == -1 && active)
                {
                    active = false;
                    CharSelectScript.ChangeCharLeft();
                    print("hitting Left");
                }
            }
            if (context.canceled)
            {
                print("cancelled");
                active = true;
            }
        }

        if (moveScript != null)
        {
            moveScript.stick = context.ReadValue<Vector2>();
            moveScript.Move(context);
        }
    }

    public void readDash(CallbackContext context)
    {
        if (curScene.buildIndex == SceneManager.GetSceneByBuildIndex(0).buildIndex)
        {
            if (context.started && active)
            {
                active = false;
                if (canAct)
                {
                    ready = true;
                }

            }
            if (context.canceled)
            {
                active = true;
            }
        }

        if (moveScript != null)
        {
            moveScript.Dash(context);
        }
    }

    public void readPress(CallbackContext context)
    {
        if (interactScript != null)
        {
            interactScript.InteractPress(context);
        }

        if (context.started && active)
        {
            if (curScene.buildIndex == titleScene.buildIndex)
            {
                active = false;
                if (ready)
                {
                    ready = false;
                    print("ready");
                    StartCoroutine(DeselectBtn());
                }
                else
                {
                    print("unready");
                    if (eventSystem == null)
                    {
                        eventSystem = GameObject.FindWithTag("Event");
                    }
                    eventSystem.GetComponent<PlayerJoinHandler>().LeavePlayer(input);
                    eventSystem.GetComponent<PlayerJoinHandler>().CancelFunc();
                }
            }
        }
        if (context.canceled)
        {
            active = true;
        }
    }

    IEnumerator DeselectBtn()
    {
        yield return new WaitForSeconds(0.05f);
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
    }

    public void readHold(CallbackContext context)
    {
        if (interactScript != null)
        {
            interactScript.InteractHold(context);
        }
    }


    public void SetInput(PlayerInput input)
    {
        this.input = input;
    }
}
