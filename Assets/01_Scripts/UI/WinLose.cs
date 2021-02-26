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

    [SerializeField] GameObject[] npc;
    [SerializeField] InputSystemUIInputModule system;
    GameObject zone = null;

    // Start is called before the first frame update
    void Start()
    {
        weapon = gameObject.GetComponent<WeaponLists>();
        zone = GameObject.FindWithTag("requestZone");
    }

    public void Lose()
    {
        npc = GameObject.FindGameObjectsWithTag("NPC");
        Screen.SetActive(true);
        loss.gameObject.SetActive(true);
        foreach(GameObject chara in npc)
            {
                chara.GetComponent<NpcRequest>().Bubble.enabled = false;
            }
        zone.SetActive(false);
        system.enabled = true;
    }

    void Win()
    {
        npc = GameObject.FindGameObjectsWithTag("NPC");
        Screen.SetActive(true);
        win.gameObject.SetActive(true);
        foreach (GameObject chara in npc)
        {
            chara.GetComponent<NpcRequest>().Bubble.enabled = false;
        }
        zone.SetActive(false);
        system.enabled = true;
    }
}
