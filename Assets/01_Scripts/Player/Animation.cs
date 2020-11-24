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
    [SerializeField] GameObject tongs = null;
    [SerializeField] GameObject tongJnt = null;
    Vector3 startPos = new Vector3(0,0,0);
    Vector3 startRot = new Vector3(0, 0, 0);


    //Gameobject Animators
    Animator tongsAnim;

    GameObject animObj;
    [SerializeField] GameObject player;
    [SerializeField] Transform HandParent;
    int currentFrame = 0;
    float animPercent = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        startPos = tongJnt.transform.position;
        startRot = tongJnt.transform.rotation.eulerAngles;
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
            AnimatorClipInfo[] animClip = anim.GetCurrentAnimatorClipInfo(1);
            currentFrame = (int) (animClip[0].weight * (animClip[0].clip.length * animClip[0].clip.frameRate));
            animPercent = (currentFrame / animClip[0].clip.length) * 1;
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
        //    if (anim.GetBool("Move") == true)
        //    {
                tongJnt.transform.parent = HandParent;
                tongJnt.transform.localPosition = new Vector3(0, 0.25f, 0);
                //tongJnt.transform.localRotation = Quaternion.Euler(4.9f, -140.4f, 75.4f);
                tongJnt.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(3.26f, 218.47f, 87.036f), Quaternion.Euler(4.97f, 220.87f, 71.7f), animPercent);
            if (animPercent > 1)
            {
                animPercent = 0;
            }
            print(currentFrame);
            //}
            //else
            //{
            //    tongJnt.transform.parent = player.transform;
            //    tongJnt.transform.localPosition = startPos;
            //    tongJnt.transform.localRotation = Quaternion.Euler(startRot);
            //}
        }

    }
}
