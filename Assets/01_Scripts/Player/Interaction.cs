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

    //Layaer Masks
    LayerMask oreLayer;
    LayerMask castLayer;

    //Distance Checks
    public float range = 2.5f;

    float CuDist;
    float SnDist;
    float ForgeDist;
    float BellowsDist;
    float SwordDist;
    float AxeDist;


    public int ForgeState;


    //GameObjects
    [SerializeField] GameObject CuOre;
    [SerializeField] GameObject SnOre;
    GameObject Forge;
    GameObject bellows;
    GameObject CuBucket, SnBucket;
    GameObject swordCast, AxeCast;
    
    //Get All Casts
    GameObject[] Casts(int layer)
    {
        var castArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var castList = new List<GameObject>();
        for (int i = 0; i < castArray.Length; i++)
        {
            if (castArray[i].layer == layer)
            {
                castList.Add(castArray[i]);
            }
        }
        if (castList.Count == 0)
        {
            return null;
        }
        return castList.ToArray();
    }
    
    //Get All Ore Barrels
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

    //Scripts
    ForgeContents fC;
    Bellows bellowsScript;
    Casting castAxe;
    Casting castSword;



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

            oreLayer = LayerMask.NameToLayer("Barrels");
            foreach (GameObject bucket in OreBuckets(oreLayer))
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

            castLayer = LayerMask.NameToLayer("Casts");
            foreach (GameObject cast in Casts(castLayer))
            {
                if (cast.tag == "Sword")
                {
                    swordCast = cast;
                    castSword = cast.GetComponent<Casting>();
                }
                else if (cast.tag == "Axe")
                {
                    AxeCast = cast;
                    castAxe = cast.GetComponent<Casting>();
                }
            }
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
        ForgeDist = Vector3.Distance(transform.position, Forge.transform.position);
        SwordDist = Vector3.Distance(transform.position, swordCast.transform.position);
        AxeDist = Vector3.Distance(transform.position, AxeCast.transform.position);

        if (heldObj != null)
        {

            if (ForgeDist < range && heldObj.layer == LayerMask.NameToLayer("Ores") && fC.instanceObj == null)
            {
                ForgeState = 3;
            }
            else if ((SwordDist < range || AxeDist < range) && heldObj.layer == LayerMask.NameToLayer("Metals"))
            {
                ForgeState = 6;
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

            if (ForgeDist < range)
            {
                ForgeState = 5;
            }
            if (AxeDist < range || SwordDist < range)
            {
                ForgeState = 7;
            }

        }

        SwitchTime();

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
        if (count)
        {
            ForgeState = 4;
            SwitchTime();
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

            //Metal Collection
            case 5:
                fC.SnMet = false;
                fC.CuMet = false;
                fC.BronzeAll = false;

                heldObj = Instantiate(fC.instanceObj, new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z), transform.rotation, gameObject.transform);
                fC.instanceObj = null;
                heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                ForgeState = 0;
                break;

            //Casting
            case 6:
                
                //Sword Cast
                if (SwordDist < AxeDist)
                {
                    if (castSword.outputObj == null)
                    {
                        if (heldObj.tag == "Tin")
                        {
                            castSword.Tin = true;
                            castSword.readyTimer = 5;
                            castSword._slide.maxValue = castSword.readyTimer;
                            Destroy(heldObj);
                        }
                        if (heldObj.tag == "Copper")
                        {
                            castSword.Copper = true;
                            castSword.readyTimer = 10;
                            castSword._slide.maxValue = castSword.readyTimer;
                            Destroy(heldObj);
                        }
                        if (heldObj.tag == "Bronze")
                        {
                            castSword.Bronze = true;
                            castSword.readyTimer = 15;
                            castSword._slide.maxValue = castSword.readyTimer;
                            Destroy(heldObj);
                        }
                    }
                }
                //Axe Cast
                else if (SwordDist > AxeDist)
                {
                    if (castAxe.outputObj == null)
                    {
                        if (heldObj.tag == "Tin")
                        {
                            castAxe.Tin = true;
                            castAxe.readyTimer = 5;
                            castAxe._slide.maxValue = castAxe.readyTimer;
                            Destroy(heldObj);
                        }
                        if (heldObj.tag == "Copper")
                        {
                            castAxe.Copper = true;
                            castAxe.readyTimer = 10;
                            castAxe._slide.maxValue = castAxe.readyTimer;
                            Destroy(heldObj);
                        }
                        if (heldObj.tag == "Bronze")
                        {
                            castAxe.Bronze = true;
                            castAxe.readyTimer = 15;
                            castAxe._slide.maxValue = castAxe.readyTimer;
                            Destroy(heldObj);
                        }
                    }
                }

                break;

            //Weapon Collect
            case 7:
                if (SwordDist < AxeDist)
                {
                    heldObj = Instantiate(castSword.outputObj, new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z), transform.rotation, gameObject.transform);
                    castSword.outputObj = null;
                    heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else
                {
                    heldObj = Instantiate(castAxe.outputObj, new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z), transform.rotation, gameObject.transform);
                    castAxe.outputObj = null;
                    heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                break;
            //Hammering
            case 8:

                break;

            //Quenching
            case 9:

                break;

            //Delivery
            case 10:

                break;
            //Default behaviour of switch
            default:

                break;



        }
    }

}
