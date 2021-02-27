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

    [SerializeField] Image[] insideImg = new Image[0];
    [SerializeField] Image[] borderImg = new Image[0];

    [SerializeField] Sprite imgActive = null;
    [SerializeField] Sprite ImageInactive = null;
    [SerializeField] Sprite borderActive = null;
    [SerializeField] Sprite borderInactive = null;


    int playerCount = 0;

    private void OnEnable()
    {
        if (gameObject.tag == "navMenu")
        {
            SetBtn();
        }
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
