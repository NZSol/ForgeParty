using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    

    //Animator + Avatars
    [SerializeField] Animator anim = null;
    [SerializeField] Avatar armEmpty = null;
    [SerializeField] Avatar armTongs = null;

    
    public GameObject tongs = null;
    [SerializeField] GameObject hammer = null;
    [SerializeField] GameObject bellows = null;



    //Gameobject Animators

    GameObject animObj;
    [SerializeField] GameObject player;
    [SerializeField] Transform rootJNT;



    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        tongs.SetActive(false);
        hammer.SetActive(false);
        bellows.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //enable/disable animation mask if item held
        if (gameObject.GetComponent<Interact>().heldObj == null)
        {
            anim.SetLayerWeight(1, 0);
        }
        else
        {
            anim.SetLayerWeight(1, 1);
        }

        //Set Holding layer based on item type
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

        //Set Action layer animation based on tool type
        if (gameObject.GetComponent<Interact>().activeTool != null)
        {
            switch (gameObject.GetComponent<Interact>().activeTool.currentTool())
            {
                case Tool.curTool.Anvil:
                    if (gameObject.GetComponent<Interact>().activeTool.GetComponent<Anvil>().charging)
                    {
                        anim.SetLayerWeight(2, 1);
                        anim.SetBool("Anvil", true);
                        hammer.SetActive(true);
                    }
                    else
                    {
                        anim.SetLayerWeight(2, 0);
                        anim.SetBool("Anvil", false);
                        hammer.SetActive(false);
                    }
                    break;
                case Tool.curTool.Furnace:
                    if (gameObject.GetComponent<Interact>().activeTool.GetComponent<Furnace>().charging)
                    {
                        anim.SetLayerWeight(2, 1);
                        anim.SetBool("Bellows", true);
                        bellows.SetActive(true);
                    }
                    else
                    {
                        anim.SetLayerWeight(2, 0);
                        anim.SetBool("Bellows", false);
                        bellows.SetActive(false);
                    }
                    break;
            }
        }

    }
}
