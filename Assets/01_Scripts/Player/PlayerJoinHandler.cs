﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinHandler : MonoBehaviour
{
    [SerializeField]
    private BindToPlayer CurrentPlayerBind;
    [SerializeField]
    GameObject lvlSelect = null;
    public void JoinPlayer(PlayerInput input)
    {
        CurrentPlayerBind.JoinGame(input);
    }

    public void SetPlayerBind(BindToPlayer players)
    {
        CurrentPlayerBind = players;
    }

    public void LeavePlayer(PlayerInput input)
    {
        CurrentPlayerBind.LeaveGame(input);
    }
    public void CancelFunc()
    {
        CurrentPlayerBind.Cancel();
        lvlSelect.SetActive(false);
    }
}
