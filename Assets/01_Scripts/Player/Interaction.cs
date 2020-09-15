using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.UI;
using System.Linq;

public class Interaction : MonoBehaviour
{
    LayerMask layer;
    public float range = 2.5f;
    float CuDist;
    float SnDist;
    float ForgeDist;


    public int ForgeState;


    bool inputArmed = true;
    [SerializeField] Text _text;
    [SerializeField] Slider _slide;

    [SerializeField] GameObject CuOre;
    [SerializeField] GameObject SnOre;
    GameObject Forge;

    ForgeContents fC;

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

    GameObject heldObj;


    // Start is called before the first frame update
    void Start()
    {
        Forge = GameObject.FindWithTag("Forge");
        fC = Forge.GetComponent<ForgeContents>();
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
    }


    public void InteractPress(CallbackContext context)
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

    void cancel()
    {
        _text.enabled = false;
    }

    void buttonPress()
    {
        CuDist = Vector3.Distance(transform.position, CuBucket.transform.position);
        SnDist = Vector3.Distance(transform.position, SnBucket.transform.position);
        ForgeDist = Vector3.Distance(transform.position, Forge.transform.position);

        if (heldObj != null)
        {
            ForgeState = 1;
        }
        if (CuDist < range || SnDist < range)
        {
            ForgeState = 2;
        }
        if (ForgeDist < range)
        {
            ForgeState = 3;
        }


        switch (ForgeState)
        {
            //Drop Held Object
            case 1:
                heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                heldObj.transform.parent = null;
                heldObj = null;

                Debug.Log("Case " + ForgeState);
                break;

            //Ore Collect State
            case 2:
                if (!heldObj)
                {
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
                break;
        }
    }

    float timer = 0;
    bool count = false;
    public void InteractHold(CallbackContext context)
    {
        if (context.started)
        {
            count = true;
        }
        if (context.canceled)
        {
            count = false;
        }
    }


    void Update()
    {

        if (count)
        {
            timer += Time.deltaTime * 5;
            _slide.value = timer;
            fC.temperature = timer;
        }
        else if (timer > 0)
        {
            timer -= Time.deltaTime * 2.5f;
            _slide.value = timer;
            fC.temperature = timer;
        }
    }

}
