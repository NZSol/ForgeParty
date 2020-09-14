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

    bool inputArmed = true;
    [SerializeField] Text _text;
    [SerializeField] Slider _slide;

    [SerializeField] GameObject CuOre;
    [SerializeField] GameObject SnOre;

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
        layer = LayerMask.NameToLayer("Ores");
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

            inputArmed = false;
            _text.enabled = true;
            Invoke("cancel", 0.1f);

            if (heldObj != null)
            {
                heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                heldObj.transform.parent = null;
                heldObj = null;
            }
            else
            {
                float SnDist = Vector3.Distance(transform.position, SnBucket.transform.position);
                float CuDist = Vector3.Distance(transform.position, CuBucket.transform.position);

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
            timer = 0;
            _slide.value = timer;
        }
    }


    void Update()
    {

        Vector3 CuDir = CuBucket.transform.position - transform.position;
        Vector3 SnDir = SnBucket.transform.position - transform.position;

        float CuDist = Vector3.Distance(transform.position, CuBucket.transform.position);
        float SnDist = Vector3.Distance(transform.position, SnBucket.transform.position);


        if (CuDist < range)
        {
            Debug.DrawRay(transform.position, CuDir, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position, CuDir, Color.red);
        }

        if (SnDist < range)
        {
            Debug.DrawRay(transform.position, SnDir, Color.white);
        }
        else
        {
            Debug.DrawRay(transform.position, SnDir, Color.gray);
        }

        if (count)
        {
            timer += Time.deltaTime;
            _slide.value = timer;
        }
    }

}
