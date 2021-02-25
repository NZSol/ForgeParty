using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

public class WinLose : MonoBehaviour
{

    WeaponLists weapon;

    [SerializeField] GameObject loss;
    [SerializeField] GameObject win;
    [SerializeField] GameObject Screen;

    [SerializeField] GameObject spawner;
    [SerializeField] InputSystemUIInputModule system;

    // Start is called before the first frame update
    void Start()
    {
        weapon = gameObject.GetComponent<WeaponLists>();
    }

    public void Lose()
    {
        Screen.SetActive(true);
        loss.gameObject.SetActive(true);
        spawner.gameObject.SetActive(false);
        system.enabled = true;
    }

    void Win()
    {
        Screen.SetActive(true);
        win.gameObject.SetActive(true);
        spawner.gameObject.SetActive(false);
        system.enabled = true;
    }
}
