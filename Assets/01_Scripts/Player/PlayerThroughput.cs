using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SceneManagement;

public class PlayerThroughput : MonoBehaviour
{
    [SerializeField] GameObject myPlayer;

    Movement moveScript;

    Interact interactScript;

    [SerializeField] Scene curScene;
    [SerializeField] Scene titleScene;


    private void Awake()
    {
        curScene = SceneManager.GetActiveScene();
        titleScene = SceneManager.GetSceneAt(0);
        DontDestroyOnLoad(this.gameObject);
        if (titleScene != null)
        {
            if (curScene.name != titleScene.name)
            {
                PlayerJoin();
            }
        }
    }

    private void Start()
    {
        //titleScene = SceneManager.GetActiveScene();

        //if (curScene.name != titleScene.name)
        //{
        //    PlayerJoin();
        //}

    }


    public void PlayerJoin()
    {
        var PlayerChar = Instantiate(myPlayer);
        moveScript = PlayerChar.GetComponent<Movement>();
        interactScript = PlayerChar.GetComponent<Interact>();
        interactScript.active = true;
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
        var npcArray = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npc in npcArray)
        {
            npc.GetComponent<NpcRequest>().timer = npc.GetComponent<NpcRequest>().timerMax;
        }
    }
}
