using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerThroughput : MonoBehaviour
{
    [SerializeField] GameObject myPlayer;

    Movement moveScript;

    Interact interactScript;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        PlayerJoin();
    }


    public void PlayerJoin()
    {
        var PlayerChar = Instantiate(myPlayer);
        PlayerChar.transform.position = transform.position;
        moveScript = PlayerChar.GetComponent<Movement>();
        interactScript = PlayerChar.GetComponent<Interact>();
        interactScript.active = true;
    }

    public void readMove (CallbackContext context)
    {
        moveScript.stick = context.ReadValue<Vector2>();
        moveScript.Move(context);
    }

    public void readDash(CallbackContext context)
    {
        moveScript.Dash(context);
    }

    public void readPress(CallbackContext context)
    {
        interactScript.InteractPress(context);
    }

    public void readHold(CallbackContext context)
    {
        interactScript.InteractHold(context);
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
