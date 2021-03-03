using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FirstSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject FirstObject = null;

    [SerializeField] GameObject lastBtn = null;
    [SerializeField] EventSystem system = null;

    private void OnEnable()
    {
        if (gameObject.tag == "navMenu")
        {
            print("nav");
        }
        if (system == null)
        {
            system = GameObject.FindWithTag("Event").GetComponent<EventSystem>();
        }
        if (gameObject.tag == "navMenu")
        {
            SetBtn();
        }
        
    }

    public void setLastBtn(GameObject obj)
    {
        //    lastBtn = obj;
        //    FirstObject = lastBtn;
        //    system.firstSelectedGameObject = lastBtn;
    }

    public void SetBtn()
    {
        StartCoroutine(highlight());
    }
    public void deselect()
    {
        StartCoroutine(DeselectBtn());
    }

    IEnumerator DeselectBtn()
    {
        yield return new WaitForSeconds(1f);
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
    }

    IEnumerator highlight()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
        EventSystem.current.SetSelectedGameObject(FirstObject);
    }


}
