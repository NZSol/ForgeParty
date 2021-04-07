using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UINavPath : MonoBehaviour
{
    [SerializeField] EventSystem Event = null;
    [SerializeField] Scrollbar scroller = null;
    [SerializeField] GameObject contents = null;
    public Toggle[] ToggleObjs = null;
    public List<GameObject> contentChildren = new List<GameObject>();

    Navigation scrollerNav = new Navigation();

    void Start()
    {
        scroller = gameObject.GetComponentInChildren<Scrollbar>();

        //Get all children of object and assign object variable to contentChildren list
        ToggleObjs = contents.GetComponentsInChildren<Toggle>();
        foreach (Toggle obj in ToggleObjs)
        {
            contentChildren.Add(obj.gameObject);
        }


        //Create Navigation directory
        Navigation toggleNav = new Navigation();
        toggleNav.mode = Navigation.Mode.Explicit;


        //Set buttons for directory
        toggleNav.selectOnRight = scroller;
        for (int i = 0; i < contentChildren.Count; i++)
        {
            if (i > 0)
            {
                toggleNav.selectOnUp = contentChildren[i-1].GetComponent<Toggle>();
            }
            if (i < contentChildren.Count - 1)
            {
                toggleNav.selectOnDown = contentChildren[i + 1].GetComponent<Toggle>();
            }
            contentChildren[i].GetComponent<Toggle>().navigation = toggleNav;
        }

        scrollerNav.selectOnLeft = contentChildren[PlayerPrefs.GetInt("resolution")].GetComponent<Toggle>();
        toggleNav.mode = Navigation.Mode.Explicit;
    }

    private void Update()
    {
        
        if (scrollerNav.mode != Navigation.Mode.Explicit)
        {
            scrollerNav.mode = Navigation.Mode.Explicit;
        }
        scroller.navigation = scrollerNav;

        float value = (GetSelectedToggle() / contentChildren.Count) / 1 ;
        float invertValue = 1 - value;
        scroller.value = invertValue;

        print(value);
    }

    float GetSelectedToggle()
    {
        for (int i = 1; i < contentChildren.Count; i++)
        {
            if (contentChildren[i] == Event.currentSelectedGameObject)
            {
                return i + 1;
            }
        }
        return 0;
    }

}
