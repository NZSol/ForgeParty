using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.UI;
using System.Linq;

public class Interaction : MonoBehaviour
{
    //public bool active = false;
    //bool inputArmed = true;
    //bool inRangeForge;

    ////Layer Masks
    //LayerMask oreLayer;
    //LayerMask castLayer;
    //LayerMask furnaceLayer;
    //LayerMask anvilLayer;
    //LayerMask bellowsLayer;
    //LayerMask quenchLayer;

    ////Distance Checks
    //public float range = 2.5f;
    //public float quenchRange = 5;

    //float CuDist;
    //float SnDist;
    //float ForgeDist;
    //float BellowsDist;
    //float SwordDist;
    //float AxeDist;
    //float AnvilDist;
    //float quenchDist;

    //public int ForgeState;

    ////Lists
    //List<float> forgeDistList = new List<float>();

    ////GameObjects
    //[SerializeField] GameObject CuOre;
    //[SerializeField] GameObject SnOre;

    //GameObject bellows;
    //GameObject CuBucket, SnBucket;
    //GameObject swordCast, AxeCast;
    //GameObject anvil;


    //GameObject PostOBJ;

    ////Get All Casts
    //GameObject[] Casts(int layer)
    //{
    //    var castArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
    //    var castList = new List<GameObject>();
    //    for (int i = 0; i < castArray.Length; i++)
    //    {
    //        if (castArray[i].layer == layer)
    //        {
    //            castList.Add(castArray[i]);
    //        }
    //    }
    //    if (castList.Count == 0)
    //    {
    //        return null;
    //    }
    //    return castList.ToArray();
    //}
    
    ////Get All Ore Barrels
    //GameObject[] OreBuckets(int layer)
    //{
    //    var bucketArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
    //    var bucketList = new List<GameObject>();
    //    for (int i = 0; i < bucketArray.Length; i++)
    //    {
    //        if (bucketArray[i].layer == layer)
    //        {
    //            bucketList.Add(bucketArray[i]);
    //        }
    //    }
    //    if (bucketList.Count == 0)
    //    {
    //        return null;
    //    }
    //    return bucketList.ToArray();
    //}


    ////Get All Furnaces //Assign Active Furnace
    //GameObject[] Furnaces(int layer)
    //{
    //    var furnaceArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
    //    var furnaceList = new List<GameObject>();
    //    for (int i = 0; i < furnaceArray.Length; i++)
    //    {
    //        if (furnaceArray[i].layer == layer)
    //        {
    //            furnaceList.Add(furnaceArray[i]);
    //        }
    //    }
    //    if (furnaceList.Count == 0)
    //    {
    //        return null;
    //    }
    //    return furnaceList.ToArray();
    //}
    //public List<GameObject> FurnaceList = new List<GameObject>();
    //GameObject activeForge;
    //GameObject ClosestForge(GameObject[] forges)
    //{
    //    GameObject Forge = null;
    //    float minDist = Mathf.Infinity;
    //    Vector3 curPos = transform.position;
    //    foreach (GameObject t in forges)
    //    {
    //        float dist = Vector3.Distance(t.transform.position, curPos);
    //        if (dist < minDist)
    //        {
    //            Forge = t;
    //            minDist = dist;
    //        }
    //    }
    //    return Forge;
    //}


    ////Get All Anvils //Assign Active Anvil
    //GameObject[] Anvils(int layer)
    //{
    //    var anvilArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
    //    var anvilList = new List<GameObject>();
    //    for (int i = 0; i < anvilArray.Length; i++)
    //    {
    //        if (anvilArray[i].layer == layer)
    //        {
    //            anvilList.Add(anvilArray[i]);
    //        }
    //    }
    //    if (anvilList.Count == 0)
    //    {
    //        return null;
    //    }
    //    return anvilList.ToArray();
    //}
    //public List<GameObject> AnvilList = new List<GameObject>();
    //GameObject activeAnvil;
    //GameObject ClosestAnvil(GameObject[] Anvils)
    //{
    //    GameObject Anvil = null;
    //    float minDist = Mathf.Infinity;
    //    Vector3 curPos = transform.position;
    //    foreach (GameObject t in Anvils)
    //    {
    //        float dist = Vector3.Distance(t.transform.position, curPos);
    //        if (dist < minDist)
    //        {
    //            Anvil = t;
    //            minDist = dist;
    //        }
    //    }
    //    return Anvil;
    //}


    ////Get All Bellows //Assign Active Bellows
    //GameObject[] Bellows(int layer)
    //{
    //    var bellowsArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
    //    var bellowsList = new List<GameObject>();
    //    for (int i = 0; i < bellowsArray.Length; i++)
    //    {
    //        if (bellowsArray[i].layer == layer)
    //        {
    //            bellowsList.Add(bellowsArray[i]);
    //        }
    //    }
    //    if (bellowsList.Count == 0)
    //    {
    //        return null;
    //    }
    //    return bellowsList.ToArray();
    //}
    //public List<GameObject> BellowsList = new List<GameObject>();
    //GameObject activeBellows;
    //GameObject ClosestBellows(GameObject[] Bellows)
    //{
    //    GameObject _bellows = null;
    //    float minDist = Mathf.Infinity;
    //    Vector3 curPos = transform.position;
    //    foreach (GameObject t in Bellows)
    //    {
    //        float dist = Vector3.Distance(t.transform.position, curPos);
    //        if (dist < minDist)
    //        {
    //            _bellows = t;
    //            minDist = dist;
    //        }
    //    }
    //    return _bellows;
    //}

    ////Get All QuenchBarrels //Assign Active QuenchBarrels
    //GameObject[] Quench(int layer)
    //{
    //    var QuenchArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
    //    var QuenchList = new List<GameObject>();
    //    for (int i = 0; i < QuenchArray.Length; i++)
    //    {
    //        if (QuenchArray[i].layer == layer)
    //        {
    //            QuenchList.Add(QuenchArray[i]);
    //        }
    //    }
    //    if (QuenchList.Count == 0)
    //    {
    //        return null;
    //    }
    //    return QuenchList.ToArray();
    //}
    //public List<GameObject> QuenchList = new List<GameObject>();
    //public GameObject activeQuenchBarrel;
    //GameObject ClosestQuench(GameObject[] Quench)
    //{
    //    GameObject _quench = null;
    //    float minDist = Mathf.Infinity;
    //    Vector3 curPos = transform.position;
    //    foreach (GameObject t in Quench)
    //    {
    //        float dist = Vector3.Distance(t.transform.position, curPos);
    //        if (dist < minDist)
    //        {
    //            _quench = t;
    //            minDist = dist;
    //        }
    //    }
    //    return _quench;
    //}

    ////Scripts
    //Bellows bellowsScript;
    //Casting castAxe;
    //Casting castSword;
    //Anvil anvilScript;


    //GameObject heldObj;

    ////Nearest Anvil

    //Vector3 holdPos;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    if (active)
    //    {
    //        bellowsLayer = LayerMask.NameToLayer("Bellows");
    //        BellowsList = Bellows(bellowsLayer).ToList();

    //        oreLayer = LayerMask.NameToLayer("Barrels");
    //        foreach (GameObject bucket in OreBuckets(oreLayer))
    //        {
    //            if (bucket.tag == "Tin")
    //            {
    //                SnBucket = bucket;
    //            }
    //            else if (bucket.tag == "Copper")
    //            {
    //                CuBucket = bucket;
    //            }
    //        }

    //        castLayer = LayerMask.NameToLayer("Casts");
    //        foreach (GameObject cast in Casts(castLayer))
    //        {
    //            if (cast.tag == "Sword")
    //            {
    //                swordCast = cast;
    //                castSword = cast.GetComponent<Casting>();
    //            }
    //            else if (cast.tag == "Axe")
    //            {
    //                AxeCast = cast;
    //                castAxe = cast.GetComponent<Casting>();
    //            }
    //        }

    //        anvilLayer = LayerMask.NameToLayer("Anvils");
    //        AnvilList = Anvils(anvilLayer).ToList();


    //        furnaceLayer = LayerMask.NameToLayer("Forges");
    //        FurnaceList = Furnaces(furnaceLayer).ToList();

    //        quenchLayer = LayerMask.NameToLayer("QuenchBarrel");
    //        QuenchList = Quench(quenchLayer).ToList();
    //    }
    //}


    //public void InteractPress(CallbackContext context)
    //{
    //    if (active)
    //    {
    //        if (context.started && inputArmed)
    //        {
    //            buttonPress();
    //            inputArmed = false;
    //        }
    //        if (context.canceled)
    //        {
    //            inputArmed = true;
    //        }
    //    }
    //}

    //void buttonPress()
    //{
    //    holdPos = new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z);
    //    ForgeDist = Vector3.Distance(transform.position, activeForge.transform.position);
    //    AnvilDist = Vector3.Distance(transform.position, activeAnvil.transform.position);
    //            //Get Distance of casts
    //    SwordDist = Vector3.Distance(transform.position, swordCast.transform.position);
    //    AxeDist = Vector3.Distance(transform.position, AxeCast.transform.position);
    //    //If holding Object, do these checks
    //    if (heldObj != null)
    //    {
    //        if (ForgeDist < range && heldObj.layer == LayerMask.NameToLayer("Ores") && activeForge.GetComponent<ForgeContents>().instanceObj == null)
    //        {
    //            ForgeState = 3;
    //        }
    //        else if ((SwordDist < range || AxeDist < range) && heldObj.layer == LayerMask.NameToLayer("Metals"))
    //        {
    //            ForgeState = 6;
    //        }
    //        else if (AnvilDist < range && heldObj.layer == LayerMask.NameToLayer("Weapons"))
    //        {
    //            ForgeState = 8;
    //        }
    //        else
    //        {
    //            ForgeState = 1;
    //        }
    //    }
    //            //If no held object, do these checks
    //    else
    //    {
    //                //Get Distances from Ore Crates
    //        CuDist = Vector3.Distance(transform.position, CuBucket.transform.position);
    //        SnDist = Vector3.Distance(transform.position, SnBucket.transform.position);

    //                //NEED TO AUTOMATE PROCESS
    //        if (CuDist < range || SnDist < range)
    //        {
    //            ForgeState = 2;
    //        }

    //        if (ForgeDist < range)
    //        {
    //            ForgeState = 5;
    //        }

    //        if (AxeDist < range || SwordDist < range)
    //        {
    //            ForgeState = 7;
    //        }

    //        if (AnvilDist < range)
    //        {
    //            ForgeState = 10;
    //        }

    //    }

    //    SwitchTime();

    //}

    //float timer = 0;
    //public bool forgeCount = false;
    //public bool anvilCount = false;
    //public bool quenchCount = false;
    //bool collectItem = false;

    //public void InteractHold(CallbackContext context)
    //{
    //    if (active)
    //    {
    //        if (context.started)
    //        {
    //            BellowsDist = Vector3.Distance(transform.position, activeBellows.transform.position);
    //            AnvilDist = Vector3.Distance(transform.position, activeAnvil.transform.position);

    //            if (BellowsDist < range)
    //            {
    //                forgeCount = true;
    //            }
    //            if (AnvilDist < range)
    //            {
    //                anvilCount = true;
    //            }
    //            if (quenchDist < quenchRange)
    //            {
    //                activeQuenchBarrel.GetComponent<QuenchBucket>().item = heldObj;
    //                heldObj.SetActive(false);
    //                quenchCount = true;
    //            }
    //        }
    //        if (context.canceled)
    //        {
    //            collectItem = true;
    //            forgeCount = false;
    //            anvilCount = false;
    //            if (quenchCount)
    //            {
    //                heldObj.SetActive(true);
    //                quenchCount = false;
    //            }
    //            activeBellows.GetComponent<Bellows>().TempIncrease = false;
    //            ForgeState = 0;
    //            activeAnvil.GetComponent<Anvil>().Hammering = false;
    //            activeQuenchBarrel.GetComponent<QuenchBucket>().Count = false;
                
    //        }
    //    }
    //}


    //void Update()
    //{
    //    activeForge = ClosestForge(FurnaceList.ToArray());
    //    activeAnvil = ClosestAnvil(AnvilList.ToArray());
    //    activeBellows = ClosestBellows(BellowsList.ToArray());
    //    activeQuenchBarrel = ClosestQuench(QuenchList.ToArray());


    //    quenchDist = Vector3.Distance(transform.position, activeQuenchBarrel.transform.position);

    //    if (heldObj != null)
    //    {
    //        PostOBJ = heldObj;
    //    }


    //    if (BellowsDist > range && forgeCount)
    //    {
    //        forgeCount = false;
    //    }
    //    if (quenchDist > quenchRange && quenchCount)
    //    {
    //        quenchCount = false;
    //        activeQuenchBarrel.GetComponent<QuenchBucket>().Count = false;
    //    }
        
    //    if (forgeCount)
    //    {
    //        ForgeState = 4;
    //        SwitchTime();
    //    }
    //    if (anvilCount)
    //    {
    //        ForgeState = 9;
    //        SwitchTime();
    //    }
    //    if (quenchCount)
    //    {
    //        ForgeState = 11;
    //        SwitchTime();
    //    }
    //}
    
    
    //void SwitchTime()
    //{
    //    switch (ForgeState)
    //    {
    //        //Drop Held Object
    //        case 1:
    //            heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    //            heldObj.transform.parent = null;
    //            heldObj = null;

    //            Debug.Log("Case " + ForgeState);
    //            ForgeState = 0;
    //            break;

    //        //Ore Collect State
    //        case 2:
    //            if (!heldObj)
    //            {
    //                //NEED TO AUTOMATE PROCESS

    //                if (SnDist < range)
    //                {
    //                    heldObj = Instantiate(SnOre, holdPos, transform.rotation, gameObject.transform);
    //                    heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    //                }
    //                if (CuDist < range)
    //                {
    //                    heldObj = Instantiate(CuOre, holdPos, transform.rotation, gameObject.transform);
    //                    heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    //                }
    //            }

    //            Debug.Log("Case " + ForgeState);
    //            ForgeState = 0;
    //            break;

    //        //Forge State
    //        case 3:
    //            if (heldObj.tag == "Tin")
    //            {
    //                print("hit SN");
    //                activeForge.GetComponent<ForgeContents>().Sn = true;
    //                Destroy(heldObj);
    //            }
    //            if (heldObj.tag == "Copper")
    //            {
    //                print("hit Cu");
    //                activeForge.GetComponent<ForgeContents>().Cu = true;
    //                Destroy(heldObj);
    //            }

    //            Debug.Log("Case " + ForgeState);
    //            ForgeState = 0;
    //            break;

    //        //Bellows State
    //        case 4:
    //            BellowsDist = Vector3.Distance(transform.position, activeBellows.transform.position);

    //            if (BellowsDist > range)
    //            {
    //                activeBellows.GetComponent<Bellows>().TempIncrease = false;
    //                ForgeState = 0;
    //                break;
    //            }
    //            if (forgeCount)
    //            {
    //                activeBellows.GetComponent<Bellows>().TempIncrease = true;
    //            }

    //            ForgeState = 0;
    //            break;

    //        //Metal Collection
    //        case 5:
    //            activeForge.GetComponent<ForgeContents>().SnMet = false;
    //            activeForge.GetComponent<ForgeContents>().CuMet = false;
    //            activeForge.GetComponent<ForgeContents>().BronzeAll = false;
    //            if (activeForge.GetComponent<ForgeContents>().instanceObj != null)
    //            {
    //                heldObj = Instantiate(activeForge.GetComponent<ForgeContents>().instanceObj, holdPos, transform.rotation, gameObject.transform);
    //            }
    //            activeForge.GetComponent<ForgeContents>().instanceObj = null;
    //            if (heldObj != null)
    //            {
    //                heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    //            }

    //            ForgeState = 0;
    //            break;

    //        //Casting
    //        case 6:
                
    //            //Sword Cast
    //            if (SwordDist < AxeDist)
    //            {
    //                if (castSword.outputObj == null)
    //                {
    //                    if (heldObj.tag == "Tin")
    //                    {
    //                        castSword.Tin = true;
    //                        castSword.readyTimer = 5;
    //                        castSword._slide.maxValue = castSword.readyTimer;
    //                        Destroy(heldObj);
    //                    }
    //                    if (heldObj.tag == "Copper")
    //                    {
    //                        castSword.Copper = true;
    //                        castSword.readyTimer = 10;
    //                        castSword._slide.maxValue = castSword.readyTimer;
    //                        Destroy(heldObj);
    //                    }
    //                    if (heldObj.tag == "Bronze")
    //                    {
    //                        castSword.Bronze = true;
    //                        castSword.readyTimer = 15;
    //                        castSword._slide.maxValue = castSword.readyTimer;
    //                        Destroy(heldObj);
    //                    }
    //                }
    //            }
    //            //Axe Cast
    //            else if (SwordDist > AxeDist)
    //            {
    //                if (castAxe.outputObj == null)
    //                {
    //                    if (heldObj.tag == "Tin")
    //                    {
    //                        castAxe.Tin = true;
    //                        castAxe.readyTimer = 5;
    //                        castAxe._slide.maxValue = castAxe.readyTimer;
    //                        Destroy(heldObj);
    //                    }
    //                    if (heldObj.tag == "Copper")
    //                    {
    //                        castAxe.Copper = true;
    //                        castAxe.readyTimer = 10;
    //                        castAxe._slide.maxValue = castAxe.readyTimer;
    //                        Destroy(heldObj);
    //                    }
    //                    if (heldObj.tag == "Bronze")
    //                    {
    //                        castAxe.Bronze = true;
    //                        castAxe.readyTimer = 15;
    //                        castAxe._slide.maxValue = castAxe.readyTimer;
    //                        Destroy(heldObj);
    //                    }
    //                }
    //            }

    //            break;

    //        //Weapon Collect
    //        case 7:
    //            if (SwordDist < AxeDist)
    //            {
    //                if (castSword.outputObj != null)
    //                {
    //                    heldObj = Instantiate(castSword.outputObj, holdPos, transform.rotation, gameObject.transform);
    //                }
    //                castSword.outputObj = null;
    //                if (heldObj != null)
    //                {
    //                    heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    //                }
    //            }
    //            else
    //            {
    //                if (castAxe.outputObj != null)
    //                {
    //                    heldObj = Instantiate(castAxe.outputObj, holdPos, transform.rotation, gameObject.transform);
    //                }
    //                castAxe.outputObj = null;
    //                if (heldObj != null)
    //                {
    //                    heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    //                }
    //            }
    //            break;

    //        //Deliver Anvil
    //        case 8:
    //            activeAnvil.GetComponent<Anvil>().inputObj = heldObj;
    //            //Check weapon type and material
    //            if (heldObj != null)
    //            {
    //                print(heldObj.name);

    //                MatName = heldObj.tag;
    //                if (heldObj.name == MatName + "Sword(Clone)")
    //                {
    //                    activeAnvil.GetComponent<Anvil>().sword = true;
    //                    print("Hit Sword");
    //                }
    //                else if (heldObj.name == MatName + "Axe(Clone)")
    //                {
    //                    activeAnvil.GetComponent<Anvil>().axe = true;
    //                    print("Hit Axe");
    //                }

    //                if (heldObj.tag == "Bronze")
    //                {
    //                    activeAnvil.GetComponent<Anvil>().material = 3;
    //                }
    //                else if (heldObj.tag == "Copper")
    //                {
    //                    activeAnvil.GetComponent<Anvil>().material = 2;
    //                }
    //                else if (heldObj.tag == "Tin")
    //                {
    //                    activeAnvil.GetComponent<Anvil>().material = 1;
    //                }
                    

    //                Destroy(heldObj);
    //            }

    //            break;

    //        //Hammer
    //        case 9:
    //            activeAnvil.GetComponent<Anvil>().Hammering = true;

    //            break;

    //        //Pickup Anvil
    //        case 10:
    //            if (activeAnvil.GetComponent<Anvil>().outputObj != null)
    //            {
    //                heldObj = Instantiate(activeAnvil.GetComponent<Anvil>().outputObj, holdPos, transform.rotation, gameObject.transform);
    //                heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    //                activeAnvil.GetComponent<Anvil>().inputObj = null;
    //            }
    //            break;

    //        //Quench
    //        case 11:

    //            print("doing");

    //            activeQuenchBarrel.GetComponent<QuenchBucket>().Count = true;

    //            //if (activeQuenchBarrel.GetComponent<QuenchBucket>().Count == true && activeQuenchBarrel.GetComponent<QuenchBucket>().item != null)
    //            //{
    //            //    activeQuenchBarrel.GetComponent<QuenchBucket>().item = heldObj;
    //            //    Invoke("destroyObj", 0.5f);
    //            //}
    //            //else
    //            //{
    //            //    heldObj.SetActive(true);
    //            //    //Invoke("nullObj", 1f);
    //            //}

    //            break;

    //            //Deliver
    //        case 12:
    //            break;

    //        //Default behaviour of switch
    //        default:

    //            break;



    //    }
    //}

    //void destroyObj()
    //{
    //    heldObj.SetActive(false);
    //}

    //void nullObj()
    //{
    //    activeQuenchBarrel.GetComponent<QuenchBucket>().item = null;
    //}
    //string MatName;

}
