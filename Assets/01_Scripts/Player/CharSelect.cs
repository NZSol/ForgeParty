using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour
{
    PlayerThroughput playerInputs;

    [SerializeField] Material woody = null;
    [SerializeField] Material jacob = null;
    [SerializeField] Material bill = null;
    [SerializeField] Material cherry = null;
    [SerializeField] Material heather = null;
    [SerializeField] Material mark = null;

    [SerializeField] GameObject[] baseMesh = null;
    [SerializeField] GameObject[] billMesh = null;
    [SerializeField] GameObject[] woodyMesh = null;
    [SerializeField] GameObject[] jacobMesh = null;
    [SerializeField] GameObject[] cherryMesh = null;
    [SerializeField] GameObject[] heatherMesh = null;
    [SerializeField] GameObject[] markMesh = null;

    public Image charImg;

    [SerializeField] Sprite billImg = null;
    [SerializeField] Sprite woodyImg = null;
    [SerializeField] Sprite jacobImg = null;
    [SerializeField] Sprite markImg = null;
    [SerializeField] Sprite heatherImg = null;
    [SerializeField] Sprite cherryImg = null;

    public Text txt = null;

    List<GameObject[]> allMesh = new List<GameObject[]>();

    public enum skin { Bill, Woody, Jacob, Cherry, Heather, Mark, End}
    public skin character;

    int curSkin = 0;

    public skin mySkin;

    // Start is called before the first frame update
    void Start()
    {
        playerInputs = gameObject.GetComponent<PlayerThroughput>();
        character = (skin)curSkin;
        if (this.gameObject.tag == "Player")
        {
            DisableAccessories();
            CharSwitch(mySkin);
        }
    }

    public void AssignSelect(PlayerSelectSetup setupScript)
    {
        charImg = setupScript.CharacterImage;
        txt = setupScript.NameText;
    }

    void DisableAccessories()
    {
        foreach (GameObject obj in billMesh)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in woodyMesh)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in jacobMesh)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in cherryMesh)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in heatherMesh)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in markMesh)
        {
            obj.SetActive(false);
        }
    }


    public void ChangeCharRight()
    {
        curSkin++;
        if (curSkin == (int)CharSelect.skin.End)
        {
            curSkin = 0;
        }
        character = (CharSelect.skin)curSkin;
        SendCharacterInfo(character);
    }
    public void ChangeCharLeft()
    {
        curSkin--;
        if (curSkin < 0)
        {
            curSkin = (int)CharSelect.skin.End - 1;
        }
        character = (CharSelect.skin)curSkin;
        SendCharacterInfo(character);
    }

    public void CharSwitch(skin activeSkin)
    {
        mySkin = activeSkin;
        switch (mySkin)
        {
            case CharSelect.skin.Bill:
                foreach (GameObject mesh in baseMesh)
                {
                    mesh.GetComponent<SkinnedMeshRenderer>().material = bill;
                }

                //Accessories
                foreach (GameObject mesh in billMesh)
                {
                    mesh.SetActive(true);
                }

                foreach (GameObject mesh in markMesh)
                {
                    mesh.SetActive(false);
                }
                break;

            case CharSelect.skin.Woody:
                foreach (GameObject mesh in baseMesh)
                {
                    mesh.GetComponent<SkinnedMeshRenderer>().material = woody;
                }

                //Accessories
                foreach (GameObject mesh in woodyMesh)
                {
                    mesh.SetActive(true);
                }

                foreach (GameObject mesh in billMesh)
                {
                    mesh.SetActive(false);
                }
                break;


            case CharSelect.skin.Jacob:
                foreach (GameObject mesh in baseMesh)
                {
                    mesh.GetComponent<SkinnedMeshRenderer>().material = jacob;
                }

                //Accessories
                foreach (GameObject mesh in jacobMesh)
                {
                    mesh.SetActive(true);
                }

                foreach (GameObject mesh in woodyMesh)
                {
                    mesh.SetActive(false);
                }
                break;


            case CharSelect.skin.Cherry:
                foreach (GameObject mesh in baseMesh)
                {
                    mesh.GetComponent<SkinnedMeshRenderer>().material = cherry;
                }

                //Accessories
                foreach (GameObject mesh in cherryMesh)
                {
                    mesh.SetActive(true);
                }

                foreach (GameObject mesh in jacobMesh)
                {
                    mesh.SetActive(false);
                }
                break;

            case CharSelect.skin.Heather:
                foreach (GameObject mesh in baseMesh)
                {
                    mesh.GetComponent<SkinnedMeshRenderer>().material = heather;
                }

                //Accessories
                foreach (GameObject mesh in heatherMesh)
                {
                    mesh.SetActive(true);
                }

                foreach (GameObject mesh in cherryMesh)
                {
                    mesh.SetActive(false);
                }
                break;

            case CharSelect.skin.Mark:
                foreach (GameObject mesh in baseMesh)
                {
                    mesh.GetComponent<SkinnedMeshRenderer>().material = mark;
                }

                //Accessories
                foreach (GameObject mesh in markMesh)
                {
                    mesh.SetActive(true);
                }

                foreach (GameObject mesh in heatherMesh)
                {
                    mesh.SetActive(false);
                }
                break;

        }
    }

    void SendCharacterInfo(skin texture)
    {
        playerInputs.mySkin = character;
    }

    public void IconSwitch(skin activeSkin)
    {
        character = activeSkin;
        switch (character)
        {
            case CharSelect.skin.Bill:
                charImg.sprite = billImg;
                txt.text = "Bill";
                break;

            case CharSelect.skin.Woody:
                charImg.sprite = woodyImg;
                txt.text = "Woody";
                break;

            case CharSelect.skin.Jacob:
                charImg.sprite = jacobImg;
                txt.text = "Jacob";
                break;


            case CharSelect.skin.Cherry:
                charImg.sprite = cherryImg;
                txt.text = "Cherry";
                break;

            case CharSelect.skin.Heather:
                charImg.sprite = heatherImg;
                txt.text = "Heather";
                break;

            case CharSelect.skin.Mark:
                charImg.sprite = markImg;
                txt.text = "Mark";
                break;
        }
    }


}
