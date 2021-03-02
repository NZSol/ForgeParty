using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SceneManagement;

public class PlayerThroughput : MonoBehaviour
{
    [SerializeField] GameObject myPlayer = null;

    Movement moveScript;

    Interact interactScript;
    CharSelect CharSelectScript;

    [SerializeField] Scene curScene;
    [SerializeField] Scene titleScene;

    StartPos position;

    [SerializeField] GameObject eventSystem = null;

    PlayerInput input;
    bool active = false;

    GameObject PlayerChar = null;
    public bool spawned = false;

    private void Start()
    {
        StartCoroutine(DelayedStart());
        spawned = false;
    }

    IEnumerator DelayedStart()
    {
        print("running");
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
        moveScript = PlayerChar.GetComponent<Movement>();
        interactScript = PlayerChar.GetComponent<Interact>();
        interactScript.active = true;
        CharSelectScript = PlayerChar.GetComponent<CharSelect>();
        position.Positioning(PlayerChar);
    }

    public void readMove (CallbackContext context)
    {
        if (moveScript != null)
        {
            moveScript.stick = context.ReadValue<Vector2>();
            moveScript.Move(context);
        }
    }

    public void readDash(CallbackContext context)
    {
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

        if (curScene.buildIndex == titleScene.buildIndex)
        {
            if (eventSystem == null)
            {
                eventSystem = GameObject.FindWithTag("Event");
            }
            eventSystem.GetComponent<PlayerJoinHandler>().LeavePlayer(input);
            eventSystem.GetComponent<PlayerJoinHandler>().CancelFunc();
        }
    }

    public void readHold(CallbackContext context)
    {
        if (interactScript != null)
        {
            interactScript.InteractHold(context);
        }
    }


    public void Rush(CallbackContext context)
    {
        
        if (context.started && active)
        {
            active = false;
            if (CharSelectScript != null)
            {
                CharSelectScript.ChangeChar();
            }
        }
        if (context.canceled)
        {
            active = true;
        }
    }

    public void SetInput(PlayerInput input)
    {
        this.input = input;
    }
}
