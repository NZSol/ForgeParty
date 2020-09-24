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
    bool inRangeForge;

    //Layaer Masks
    LayerMask oreLayer;
    LayerMask castLayer;
    LayerMask furnaceLayer;
    LayerMask anvilLayer;

    //Distance Checks
    public float range = 2.5f;

    float CuDist;
    float SnDist;
    float ForgeDist;
    float BellowsDist;
    float SwordDist;
    float AxeDist;
    float AnvilDist;


    public int ForgeState;

    //Lists
    List<float> forgeDistList = new List<float>();

    //GameObjects
    [SerializeField] GameObject CuOre;
    [SerializeField] GameObject SnOre;

    GameObject bellows;
    GameObject CuBucket, SnBucket;
    GameObject swordCast, AxeCast;
    GameObject anvil;
    
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


    //Get All Furnaces
    GameObject[] Furnaces(int layer)
    {
        var furnaceArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var furnaceList = new List<GameObject>();
        for (int i = 0; i < furnaceArray.Length; i++)
        {
            if (furnaceArray[i].layer == layer)
            {
                furnaceList.Add(furnaceArray[i]);
            }
        }
        if (furnaceList.Count == 0)
        {
            return null;
        }
        return furnaceList.ToArray();
    }
    public List<GameObject> FurnaceList = new List<GameObject>();
    GameObject activeForge;

    //Get All Anvils
    GameObject[] Anvils(int layer)
    {
        var anvilArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var anvilList = new List<GameObject>();
        for (int i = 0; i < anvilArray.Length; i++)
        {
            if (anvilArray[i].layer == layer)
            {
                anvilList.Add(anvilArray[i]);
            }
        }
        if (anvilList.Count == 0)
        {
            return null;
        }
        return anvilList.ToArray();
    }
    public List<GameObject> AnvilList = new List<GameObject>();
    GameObject activeAnvil;

    //Scripts
    Bellows bellowsScript;
    Casting castAxe;
    Casting castSword;
    Anvil anvilScript;


    GameObject heldObj;

    //NearestForge
    GameObject ClosestForge(GameObject[] forges)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 curPos = transform.position;
        foreach (GameObject t in forges)
        {
            float dist = Vector3.Distance(t.transform.position, curPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    //Nearest Anvil
    GameObject ClosestAnvil(GameObject[] Anvils)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 curPos = transform.position;
        foreach (GameObject t in Anvils)
        {
            float dist = Vector3.Distance(t.transform.position, curPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (active)
        {
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

            anvilLayer = LayerMask.NameToLayer("Anvils");
            AnvilList = Anvils(anvilLayer).ToList();


            furnaceLayer = LayerMask.NameToLayer("Forges");
            FurnaceList = Furnaces(furnaceLayer).ToList();

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
        ForgeDist = Vector3.Distance(transform.position, activeForge.transform.position);
        AnvilDist = Vector3.Distance(transform.position, activeAnvil.transform.position);
                //Get Distance of casts
        SwordDist = Vector3.Distance(transform.position, swordCast.transform.position);
        AxeDist = Vector3.Distance(transform.position, AxeCast.transform.position);
        //If holding Object, do these checks
        if (heldObj != null)
        {
            if (ForgeDist < range && heldObj.layer == LayerMask.NameToLayer("Ores") && activeForge.GetComponent<ForgeContents>().instanceObj == null)
            {
                ForgeState = 3;
            }
            else if ((SwordDist < range || AxeDist < range) && heldObj.layer == LayerMask.NameToLayer("Metals"))
            {
                ForgeState = 6;
            }
            else if (AnvilDist < range && heldObj.layer == LayerMask.NameToLayer("Weapons"))
            {
                ForgeState = 8;
            }
            else
            {
                ForgeState = 1;
            }
        }
                //If no held object, do these checks
        else
        {
                    //Get Distances from Ore Crates
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
    public bool forgeCount = false;
    public bool anvilCount = false;

    public void InteractHold(CallbackContext context)
    {
        if (active)
        {
            if (context.started)
            {
                BellowsDist = Vector3.Distance(transform.position, bellows.transform.position);
                AnvilDist = Vector3.Distance(transform.position, activeAnvil.transform.position);

                if (BellowsDist < range)
                {
                    forgeCount = true;
                }
                if (AnvilDist < range)
                {
                    anvilCount = true;
                }
            }
            if (context.canceled)
            {
                forgeCount = false;
                anvilCount = false;
                bellowsScript.TempIncrease = false;
                ForgeState = 0;
            }
        }
    }


    void Update()
    {
        activeForge = ClosestForge(FurnaceList.ToArray());
        activeAnvil = ClosestAnvil(AnvilList.ToArray());

        if (forgeCount)
        {
            ForgeState = 4;
            SwitchTime();
        }
        if (anvilCount)
        {
            ForgeState = 9;
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
                    activeForge.GetComponent<ForgeContents>().Sn = true;
                    Destroy(heldObj);
                }
                if (heldObj.tag == "Copper")
                {
                    print("hit Cu");
                    activeForge.GetComponent<ForgeContents>().Cu = true;
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
                if (forgeCount)
                {
                    bellowsScript.TempIncrease = true;
                }

                ForgeState = 0;
                break;

            //Metal Collection
            case 5:
                activeForge.GetComponent<ForgeContents>().SnMet = false;
                activeForge.GetComponent<ForgeContents>().CuMet = false;
                activeForge.GetComponent<ForgeContents>().BronzeAll = false;

                heldObj = Instantiate(activeForge.GetComponent<ForgeContents>().instanceObj, new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z), transform.rotation, gameObject.transform);
                activeForge.GetComponent<ForgeContents>().instanceObj = null;
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

            //Deliver Anvil
            case 8:

                //Check weapon type and material
                if (heldObj.tag == "Sword")
                {
                    activeAnvil.GetComponent<Anvil>().sword = true;
                    Destroy(heldObj);
                }
                else if (heldObj.tag == "Axe")
                {
                    activeAnvil.GetComponent<Anvil>().axe = true;
                    Destroy(heldObj);
                }

                if (heldObj.tag == "Bronze")
                {
                    activeAnvil.GetComponent<Anvil>().material = 3;
                }
                else if (heldObj.tag == "Copper")
                {
                    activeAnvil.GetComponent<Anvil>().material = 2;
                }
                else if (heldObj.tag == "Tin")
                {
                    activeAnvil.GetComponent<Anvil>().material = 1;
                }

                break;

            //Hammer
            case 9:
                activeAnvil.GetComponent<Anvil>().Hammering = true;

                break;

            //Pickup Anvil
            case 10:

                break;

            //Quench
            case 11:
                
                break;

                //Deliver
            case 12:
                break;

            //Default behaviour of switch
            default:

                break;



        }
    }

}
