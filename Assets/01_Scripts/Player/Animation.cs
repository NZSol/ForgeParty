using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    //Animator + Avatars
    [SerializeField] Animator anim = null;
    [SerializeField] Avatar armEmpty = null;
    [SerializeField] Avatar armTongs = null;

    //Tong Vars
    public GameObject tongs = null;
    [SerializeField] GameObject tongJnt = null;
    

    //Gameobject Animators

    GameObject animObj;
    [SerializeField] GameObject player;
    [SerializeField] Transform rootJNT;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //enable/disable animation mask
        if (gameObject.GetComponent<Interact>().heldObj == null)
        {
            anim.SetLayerWeight(1, 0);
        }
        else
        {
            anim.SetLayerWeight(1, 1);
        }

        //Decide animation mask
        if (gameObject.GetComponent<Interact>().heldObj != null)
        {
            if (gameObject.GetComponent<Interact>().heldObj.GetComponent<Item>().tool == Item.Tool.Cast)
            {
                anim.SetBool("Crucible", true);
                if (animObj != tongs)
                {
                    animObj = tongs;
                    animObj.SetActive(true);

                }
            }
            else
            {
                anim.SetBool("Crucible", false);
                if (animObj != null)
                {
                    animObj.SetActive(false);
                    animObj = null;
                }
            }
        }


        if (animObj == tongs)
        {
            if (anim.GetBool("Crucible") && anim.GetBool("Move"))
            {
                animObj.transform.parent = rootJNT;
            }
            else if (!anim.GetBool("Move"))
            {
                animObj.transform.parent = player.transform;
            }

        }

    }
}
