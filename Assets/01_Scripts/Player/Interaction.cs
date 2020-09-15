using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.UI;
using System.Linq;

public class Interaction : MonoBehaviour
{
    public bool active = false;
    bool inputArmed = true;

    LayerMask layer;

    //Distance Checks
    public float range = 2.5f;

    float CuDist;
    float SnDist;
    float ForgeDist;
    float BellowsDist;


    public int ForgeState;


    [SerializeField] Slider _slide;

    [SerializeField] GameObject CuOre;
    [SerializeField] GameObject SnOre;
    [SerializeField] GameObject Forge;
    [SerializeField] GameObject bellows;
    GameObject[] OreBuckets(int layer)
    {
        var bucketArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var bucketList = new List<GameObject>();
        for (int i = 0; i < bucketArray.Length; i++)
        {
            if (bucketArray[i].layer == layer)
            {
                bucketList.Add(bucketArray[i]);
            }
        }
        if (bucketList.Count == 0)
        {
            return null;
        }
        return bucketList.ToArray();
    }
    GameObject CuBucket, SnBucket;
    List<GameObject> BucketList = new List<GameObject>();

    //Scripts
    ForgeContents fC;
    Bellows bellowsScript;



    GameObject heldObj;


    // Start is called before the first frame update
    void Start()
    {
        if (active)
        {
            Forge = GameObject.FindWithTag("Forge");
            fC = Forge.GetComponent<ForgeContents>();

            bellows = GameObject.FindWithTag("Bellow");
            bellowsScript = bellows.GetComponent<Bellows>();

            layer = LayerMask.NameToLayer("Barrels");
            foreach (GameObject bucket in OreBuckets(layer))
            {
                if (bucket.tag == "Tin")
                {
                    SnBucket = bucket;
                }
                else if (bucket.tag == "Copper")
                {
                    CuBucket = bucket;
                }
            }
            BucketList = OreBuckets(layer).ToList();
        }
    }


    public void InteractPress(CallbackContext context)
    {
        if (active)
        {
            if (context.started && inputArmed)
            {
                buttonPress();
                inputArmed = false;
            }
            if (context.canceled)
            {
                inputArmed = true;
            }
        }
    }

    void buttonPress()
    {

        if (heldObj != null)
        {
            ForgeDist = Vector3.Distance(transform.position, Forge.transform.position);

            if(ForgeDist < range)
            {
                ForgeState = 3;
            }
            else
            {
                ForgeState = 1;
            }
        }
        else
        {
            CuDist = Vector3.Distance(transform.position, CuBucket.transform.position);
            SnDist = Vector3.Distance(transform.position, SnBucket.transform.position);

                    //NEED TO AUTOMATE PROCESS
            if (CuDist < range || SnDist < range)
            {
                ForgeState = 2;
            }

        }


    }

    float timer = 0;
    public bool count = false;
    public void InteractHold(CallbackContext context)
    {
        if (active)
        {
            if (context.started)
            {
                BellowsDist = Vector3.Distance(transform.position, bellows.transform.position);

                if (BellowsDist < range)
                {
                    count = true;
                }
            }
            if (context.canceled)
            {
                count = false;
                bellowsScript.TempIncrease = false;
                ForgeState = 0;
            }
        }
    }


    void Update()
    {
        SwitchTime();
        if (count)
        {
            ForgeState = 4;
        }
    }
    
    
    
    void SwitchTime()
    {
        switch (ForgeState)
        {
            //Drop Held Object
            case 1:
                heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                heldObj.transform.parent = null;
                heldObj = null;

                Debug.Log("Case " + ForgeState);
                ForgeState = 0;
                break;

            //Ore Collect State
            case 2:
                if (!heldObj)
                {
                            //NEED TO AUTOMATE PROCESS

                    if (SnDist < range)
                    {
                        heldObj = Instantiate(SnOre, new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z), transform.rotation, gameObject.transform);
                        heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    }
                    if (CuDist < range)
                    {
                        heldObj = Instantiate(CuOre, new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z), transform.rotation, gameObject.transform);
                        heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    }

                }

                Debug.Log("Case " + ForgeState);
                ForgeState = 0;
                break;

            //Forge State
            case 3:
                if (heldObj.tag == "Tin")
                {
                    print("hit SN");
                    fC.Sn = true;
                    Destroy(heldObj);
                }
                if (heldObj.tag == "Copper")
                {
                    print("hit Cu");
                    fC.Cu = true;
                    Destroy(heldObj);
                }

                Debug.Log("Case " + ForgeState);
                ForgeState = 0;
                break;

            //Bellows State
            case 4:
                BellowsDist = Vector3.Distance(transform.position, bellows.transform.position);

                if (BellowsDist > range)
                {
                    bellowsScript.TempIncrease = false;
                    ForgeState = 0;
                    break;
                }
                if (count)
                {
                    bellowsScript.TempIncrease = true;
                }


                ForgeState = 0;
                break;

                //Default behaviour of switch
            default:

                break;
        }
    }

}
