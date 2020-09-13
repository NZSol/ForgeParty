using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.UI;
using System.Linq;

public class Interaction : MonoBehaviour
{
    LayerMask layer;


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
    List<GameObject> Buckets = new List<GameObject>();
    bool inCu = false;
    bool inSn = false;

    GameObject CuBucket, SnBucket;

    GameObject heldObj;


    // Start is called before the first frame update
    void Start()
    {
        layer = LayerMask.NameToLayer("Ores");
        Buckets = OreBuckets(layer).ToList();
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
            Vector3 SnDir = SnBucket.transform.position - transform.position;
            Vector3 CuDir = CuBucket.transform.position - transform.position;
            RaycastHit hit;

            if (heldObj != null)
            {
                heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                heldObj.transform.parent = null;
                heldObj = null;
            }

            else if (Physics.Raycast(transform.position, SnDir, out hit, 1))
            {
                heldObj = Instantiate(SnOre, new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z), transform.rotation, gameObject.transform);
                heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else if (Physics.Raycast(transform.position, CuDir, out hit, 1))
            {
                if (heldObj == null)
                {
                    heldObj = Instantiate(CuOre, new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z), transform.rotation, gameObject.transform);
                    heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }



            //RaycastHit hit;
            //if (Physics.Raycast(transform.position, CuDir))

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
        
        Debug.DrawRay(transform.position, CuDir * 1, Color.red);
        Debug.DrawRay(transform.position, SnDir * 1, Color.gray);

        if (count)
        {
            timer += Time.deltaTime;
            _slide.value = timer;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Copper")
        {
            inCu = true;
        }
        if (other.tag == "Tin")
        {
            inCu = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Copper")
        {
            inCu = false;
        }

        if (other.tag == "Tin")
        {
            inSn = false;
        }
    }


}
