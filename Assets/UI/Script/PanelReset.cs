using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelReset : MonoBehaviour
{

    public GameObject panelToReset;

    private void OnEnable()
    {
        panelToReset.GetComponent<Button>();
        Debug.Log ("i got a button");
    }
}
