﻿using System.Collections;
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

    [SerializeField] Scene curScene;
    [SerializeField] Scene titleScene;

    StartPos position;

    [SerializeField] GameObject eventSystem = null;

    PlayerInput input;
    private void Awake()
    {
        print("running awake");
        curScene = SceneManager.GetActiveScene();
        titleScene = SceneManager.GetSceneByBuildIndex(0);
        DontDestroyOnLoad(this.gameObject);

        if (curScene.buildIndex == titleScene.buildIndex)
        {
            eventSystem = GameObject.FindWithTag("Event");
        }
    }

    void Update()
    {
        if (curScene.name != SceneManager.GetActiveScene().name)
        {
            curScene = SceneManager.GetActiveScene();
            position = GameObject.FindWithTag("LevelGod").GetComponent<StartPos>();
            PlayerJoin();
        }
    }


    public void PlayerJoin()
    {
        var PlayerChar = Instantiate(myPlayer);
        moveScript = PlayerChar.GetComponent<Movement>();
        interactScript = PlayerChar.GetComponent<Interact>();
        interactScript.active = true;
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
            eventSystem.GetComponent<PlayerJoinHandler>().LeavePlayer(input);
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

    public void SetInput(PlayerInput input)
    {
        this.input = input;
    }
}
