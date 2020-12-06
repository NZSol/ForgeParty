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
    [SerializeField] GameObject[] woodyMesh = null;
    [SerializeField] GameObject[] jacobMesh = null;
    [SerializeField] GameObject[] billMesh = null;
    [SerializeField] GameObject[] cherryMesh = null;
    [SerializeField] GameObject[] heatherMesh = null;
    [SerializeField] GameObject[] markMesh = null;

    public enum skin { Bill, Woody, Jacob, Cherry, Heather, Mark, End}
    public skin character;

    int curSkin = 0;

    // Start is called before the first frame update
    void Start()
    {
        character = (skin)curSkin;
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
                break;

            case CharSelect.skin.Woody:
                foreach(GameObject mesh in baseMesh)
                {
                    mesh.GetComponent<SkinnedMeshRenderer>().material = woody;
                }
                break;

            case CharSelect.skin.Jacob:
                foreach (GameObject mesh in baseMesh)
                {
                    mesh.GetComponent<SkinnedMeshRenderer>().material = jacob;
                }
                break;


            case CharSelect.skin.Cherry:
                foreach (GameObject mesh in baseMesh)
                {
                    mesh.GetComponent<SkinnedMeshRenderer>().material = cherry;
                }
                break;

            case CharSelect.skin.Heather:
                foreach (GameObject mesh in baseMesh)
                {
                    mesh.GetComponent<SkinnedMeshRenderer>().material = heather;
                }
                break;

            case CharSelect.skin.Mark:
                foreach (GameObject mesh in baseMesh)
                {
                    mesh.GetComponent<SkinnedMeshRenderer>().material = mark;
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
