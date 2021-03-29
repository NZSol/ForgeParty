using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class EnableDisableJoin : MonoBehaviour
{
    [SerializeField] EventSystem system;

    private void OnEnable()
    {
        system.GetComponent<PlayerInputManager>().DisableJoining();
    }

    private void OnDisable()
    {
        if (gameObject.tag != "Tutorial")
        {
            system.GetComponent<PlayerInputManager>().EnableJoining();
        }
    }
}
