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
    GameObject[] zone = null;

    [SerializeField] GameObject lifePanel = null;
    [SerializeField] Text gameOverText = null;

    // Start is called before the first frame update
    void Start()
    {
        weapon = gameObject.GetComponent<WeaponLists>();
        zone = GameObject.FindGameObjectsWithTag("requestZone");
    }

    public void Lose()
    {
        switch (gameObject.GetComponent<GameMode>().myMode)
        {
            case GameMode.gameMode.Survival:
                gameOverText.text = "OUT OF LIVES!";
                lifePanel.SetActive(false);
                break;
            case GameMode.gameMode.TimeAttack:
                gameOverText.text = "TIMES UP!";
                break;
        }
        npc = GameObject.FindGameObjectsWithTag("NPC");
        Screen.SetActive(true);
        loss.gameObject.SetActive(true);
        foreach(GameObject chara in npc)
        {
            chara.GetComponent<NpcRequest>().Bubble.enabled = false;
        }
        foreach (GameObject site in zone)
        {
            site.SetActive(false);
        }
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
        foreach (GameObject site in zone)
        {
            site.SetActive(false);
        }
        system.enabled = true;
    }
}
