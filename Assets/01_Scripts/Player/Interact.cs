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
            float dist = Vector3.Distance(obj.transform.position, transform.position);
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

    bool usingTool = false;

    [SerializeField] GameObject heldObj;

    Vector3 heldPos;



    // Start is called before the first frame update
    void Start()
    {
        if (active)
        {
            toolsLayer = LayerMask.NameToLayer("Tools");
            Interactables = Tools(toolsLayer).ToList();
            heldPos = new Vector3(0, 0.3f, 0.9f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        activeTool = closestTool(Interactables.ToArray());
        toolDist = Vector3.Distance(transform.position, activeTool.gameObject.transform.position);

        if (usingTool)
        {
            useTool();
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
        heldObj.transform.parent = null;
        heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    void collectItem()
    {
        heldObj = activeTool.GiveItem();
        heldObj.transform.position = heldPos;
        heldObj.transform.parent = gameObject.transform;

        heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    void deliverItem()
    {
        activeTool.GetComponent<Tool>().TakeItem(heldObj);
        Destroy(heldObj);
    }




    //FUNCTION ON LEFT BUTTON HOLD
    public void InteractHold(CallbackContext context)
    {
        if (active)
        {
            if (context.started)
            {
                if (activeTool.GetComponent<timerScript>() != null)
                {
                    usingTool = true;
                }
                else
                {
                    //display question UI?
                }
            }
            if (context.canceled)
            {
                usingTool = false;
                if (activeTool.GetComponent<timerScript>().charge)
                {
                    activeTool.GetComponent<timerScript>().charge = false;
                }
            }
        }
    }



    void useTool()
    {
        if (toolDist <= range)
        {
            activeTool.GetComponent<timerScript>().charge = true;
        }
        else
        {
            activeTool.GetComponent<timerScript>().charge = false;
        }
    }


}
