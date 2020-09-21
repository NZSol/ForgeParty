using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.InputSystem.InputAction;

public class UIInteraction : MonoBehaviour
{
    EventSystem _event;
    public GameObject pauseFirst, optionsFirst, titleFirst, exitConfFirst, localFirst;

    // Start is called before the first frame update
    void Start()
    {
        _event = gameObject.GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
    }



    public void Input(CallbackContext context)
    {
        InputPress();
    }

    public void Move(CallbackContext context)
    {

    }

    public void Back(CallbackContext context)
    {
        
    }

    void InputPress()
    {
        print("hit");
    }

}
