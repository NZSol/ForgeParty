using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject FirstObject;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(FirstObject);
    }
}
