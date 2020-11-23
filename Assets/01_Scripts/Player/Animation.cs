using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    //Animator + Avatars
    [SerializeField] Animator anim = null;
    [SerializeField] Avatar armEmpty = null;
    [SerializeField] Avatar armTongs = null;

    //GameObjects
    [SerializeField] GameObject tongs = null;

    //Gameobject Animators
    Animator tongsAnim;

    GameObject animObj;
    [SerializeField] Transform HandParent;
    int currentFrame = 0;

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
            AnimatorClipInfo[] animClip = anim.GetCurrentAnimatorClipInfo(0);
            currentFrame = (int) (animClip[0].weight * (animClip[0].clip.length * animClip[0].clip.frameRate));
        }

        //Decide animation mask
        if (gameObject.GetComponent<Interact>().heldObj != null)
        {
            if (gameObject.GetComponent<Interact>().heldObj.GetComponent<Item>().tool == Item.Tool.Cast)
            {
                anim.SetBool("Crucible", true);
                if (animObj == null)
                {
                    animObj = Instantiate(tongs, HandParent);
                    animObj.transform.localPosition = Vector3.zero;
                    tongsAnim.playbackTime = currentFrame;
                }
            }
            else
            {
                anim.SetBool("Crucible", false);
                if (animObj != null)
                {
                    Destroy(animObj);
                }
            }
        }

    }
}
