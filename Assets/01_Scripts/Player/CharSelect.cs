using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelect : MonoBehaviour
{
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

    List<GameObject[]> allMesh = new List<GameObject[]>();

    public enum skin { Bill, Woody, Jacob, Cherry, Heather, Mark, End}
    public skin character;

    int curSkin = 0;

    // Start is called before the first frame update
    void Start()
    {
        character = (skin)curSkin;
        DisableAccessories();
    }

    void DisableAccessories()
    {
        foreach(GameObject obj in billMesh)
        {
            obj.SetActive(false);
        }
        foreach(GameObject obj in woodyMesh)
        {
            obj.SetActive(false);
        }
        foreach(GameObject obj in jacobMesh)
        {
            obj.SetActive(false);
        }
        foreach(GameObject obj in cherryMesh)
        {
            obj.SetActive(false);
        }
        foreach(GameObject obj in heatherMesh)
        {
            obj.SetActive(false);
        }
        foreach(GameObject obj in markMesh)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (character)
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
                foreach(GameObject mesh in baseMesh)
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

    public void ChangeChar()
    {
        curSkin++;
        if (curSkin == (int)CharSelect.skin.End)
        {
            curSkin = 0;
        }
        character = (CharSelect.skin)curSkin;
    }


}
