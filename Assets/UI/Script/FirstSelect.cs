using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FirstSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject FirstObject;

    [SerializeField] Image[] insideImg;
    [SerializeField] Image[] borderImg;

    [SerializeField] Sprite imgActive;
    [SerializeField] Sprite ImageInactive;
    [SerializeField] Sprite borderActive;
    [SerializeField] Sprite borderInactive;

    int playerCount = 0;

    private void OnEnable()
    {
        
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
