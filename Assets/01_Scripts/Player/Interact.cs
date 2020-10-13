using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEngine.InputSystem.InputAction;

public class Interact : MonoBehaviour
{
    //Setup
    public bool active = false;
    bool inputArmed = true;

    //LayerMasks
    LayerMask toolsLayer;

    //Get all tools
    GameObject[] Tools(int layer)
    {
        var toolsArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var toolsList = new List<GameObject>();
        for (int i = 0; i < toolsArray.Length; i++)
        {
            if (toolsArray[i].layer == layer)
            {
                toolsList.Add(toolsArray[i]);
            }
        }
        if (toolsList.Count == 0)
        {
            return null;
        }
        return toolsList.ToArray();
    }
    public List<GameObject> Interactables = new List<GameObject>();

    //Closest tool
    Tool closestTool(GameObject[] tools)
    {
        GameObject tool = null;
        float minDist = Mathf.Infinity;
        Vector3 curPos = transform.position;
        foreach (GameObject obj in tools)
        {
            float dist = Vector3.Distance(obj.transform.position, curPos);
            if (dist < minDist)
            {
                tool = obj;
                minDist = dist;
            }
        }
        return tool.GetComponent<Tool>();
    }
    public Tool activeTool;


    //Game Variables
    public float range = 5;
    float toolDist;


    public GameObject heldObj;


    // Start is called before the first frame update
    void Start()
    {
        active = true;
        if (active)
        {
            toolsLayer = LayerMask.NameToLayer("Tools");
            Interactables = Tools(toolsLayer).ToList();
            //heldPos = new Vector3(0, 0f, 1.8f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        activeTool = closestTool(Interactables.ToArray());

        print(Vector3.Distance(activeTool.transform.position, transform.position));

        if (activeTool != null)
        {
            toolDist = Vector3.Distance(transform.position, activeTool.gameObject.transform.position);
        }
        else
        {
            Debug.Log("No Active Tool");
        }

        if (toolDist >= range)
        {
            activeTool.GetComponent<Tool>().charging = false;
        }
    }




    //FUNCTION ON RIGHT BUTTON PRESS
    public void InteractPress(CallbackContext context)
    {
        if (active)
        {
            if (context.started && inputArmed)
            {
                //drop item
                if (toolDist > range)
                {
                    dropItem();
                }
                else
                {
                    if (heldObj != null)
                    {
                        deliverItem();
                    }
                    else
                    {
                        collectItem();
                    }
                }



                inputArmed = false;
            }
            if (context.canceled)
            {
                inputArmed = true;
            }
        }
    }


    void dropItem()
    {
        if (heldObj != null)
        {
            heldObj.transform.parent = null;
            heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            heldObj = null;
        }
        else
        {
            Debug.Log("Trying to drop empty heldObj");
        }
    }

    void collectItem()
    {
        heldObj = activeTool.GiveItem();
        if (heldObj != null)
        {
            positionHeldObj();
        }
    }

    void deliverItem()
    {
        switch (heldObj.GetComponent<Item>().tool)
        {
            case Item.Tool.Furnace:

                if (activeTool.GetComponent<Tool>().tool == Tool.curTool.Furnace)
                {
                    activeTool.GetComponent<Tool>().TakeItem(heldObj);
                    Destroy(heldObj);
                }
                    break;

            case Item.Tool.Cast:

                if (activeTool.GetComponent<Tool>().tool == Tool.curTool.Cast)
                {
                    activeTool.GetComponent<Tool>().TakeItem(heldObj);
                    Destroy(heldObj);
                }
                    break;

            case Item.Tool.Anvil:

                if (activeTool.GetComponent<Tool>().tool == Tool.curTool.Anvil)
                {
                    activeTool.GetComponent<Tool>().TakeItem(heldObj);
                    Destroy(heldObj);
                }
                    break;

            case Item.Tool.Bucket:

                if (activeTool.GetComponent<Tool>().tool == Tool.curTool.Bucket)
                {
                    activeTool.GetComponent<Tool>().TakeItem(heldObj);
                    Destroy(heldObj);
                }
                    break;
        }
    }

    //FUNCTION ON LEFT BUTTON HOLD
    public void InteractHold(CallbackContext context)
    {
        if (active)
        {
            if (context.started)
            {
                if (toolDist <= range)
                {
                    activeTool.GetComponent<Tool>().charging = true;
                }
                print("starting");
            }
            if (context.canceled)
            {
                activeTool.GetComponent<Tool>().charging = false;
                print("cancelling");
            }
        }
    }

    //ACCESSORY

    void positionHeldObj()
    {
        heldObj.transform.parent = gameObject.transform;
        heldObj.transform.localPosition = new Vector3(0, 0, 1f);
        heldObj.transform.localRotation = Quaternion.Euler(Vector3.zero);

        heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

    }
}
