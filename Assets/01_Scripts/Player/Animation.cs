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

    bool itemSet = false;

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
            if (gameObject.GetComponent<Interact>().heldObj.GetComponent<Item>().tool != Item.Tool.Furnace)
            {
                anim.SetBool("Ore", false);
            }

            switch (gameObject.GetComponent<Interact>().heldObj.GetComponent<Item>().tool)
            {
                case Item.Tool.Furnace:
                    anim.SetBool("Ore", true);
                    break;

                case Item.Tool.Cast:
                    anim.SetBool("Crucible", true);
                    if (animObj != tongs)
                    {
                        animObj = tongs;
                        animObj.SetActive(true);

                    }
                    break;

                case Item.Tool.Anvil:
                    anim.SetBool("WeaponHead", true);
                    break;

                case Item.Tool.Bucket:
                    anim.SetBool("Weapon", true);
                    break;
            }
        }
        else
        {
            anim.SetBool("Ore", true);
            anim.SetBool("Crucible", false);
            if (tongs.activeSelf)
            {
                tongs.SetActive(false);
            }
            anim.SetBool("WeaponHead", false);
            anim.SetBool("Weapon", false);
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
