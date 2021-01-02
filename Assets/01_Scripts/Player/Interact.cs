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
    Animator anim = null;

    //LayerMasks
    LayerMask toolsLayer = 0;


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
    public Tool activeTool = null;


    //Game Variables
    public float range = 5;
    float toolDist = 0;
    Tool curTool;


    public GameObject heldObj = null;
    [SerializeField] Transform LHand;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
        if (active)
        {
            toolsLayer = LayerMask.NameToLayer("Tools");
            Interactables = Tools(toolsLayer).ToList();
            anim = gameObject.GetComponent<Animator>();
            StartCoroutine(ActivateTool());
        }
    }

    IEnumerator ActivateTool()
    {
        yield return new WaitForSeconds(0.1f);
        curTool = activeTool;
    }

    // Update is called once per frame
    void Update()
    {
        activeTool = closestTool(Interactables.ToArray());
        if (curTool != activeTool && curTool != null)
        {
            curTool.GetComponent<Outline>().OutlineColor = Color.white;
            curTool = activeTool;
        }

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
            activeTool.GetComponent<Outline>().OutlineColor = Color.white;
        }
        else
        {
            activeTool.GetComponent<Outline>().OutlineColor = Color.yellow;
        }

        if (heldObj == null)
        {
            anim.SetLayerWeight(1, 0);
        }
    }

    void SetColors()
    {
        foreach(GameObject tool in Interactables)
        {
            if (tool.GetComponent<Outline>().OutlineColor != Color.yellow)
            {
                tool.GetComponent<Outline>().OutlineColor = Color.white;
            }
        }
    }

    void checkItem()
    {
        if (heldObj != null)
        {
            switch (heldObj.GetComponent<Item>().tool)
            {
                case Item.Tool.Furnace:
                    foreach (GameObject tool in Interactables)
                    {
                        if (tool.GetComponent<Tool>().tool == Tool.curTool.Furnace)
                        {
                            tool.GetComponent<Outline>().OutlineColor = gameObject.GetComponent<Outline>().OutlineColor;
                        }
                    }
                    break;

                case Item.Tool.Cast:
                    foreach (GameObject tool in Interactables)
                    {
                        if (tool.GetComponent<Tool>().tool == Tool.curTool.Cast)
                        {
                            tool.GetComponent<Outline>().OutlineColor = gameObject.GetComponent<Outline>().OutlineColor;
                        }
                    }
                    break;

                case Item.Tool.Anvil:
                    foreach (GameObject tool in Interactables)
                    {
                        if (tool.GetComponent<Tool>().tool == Tool.curTool.Anvil)
                        {
                            tool.GetComponent<Outline>().OutlineColor = gameObject.GetComponent<Outline>().OutlineColor;
                        }
                    }
                    break;

                case Item.Tool.Bucket:
                    foreach (GameObject tool in Interactables)
                    {
                        if (tool.GetComponent<Tool>().tool == Tool.curTool.Bucket)
                        {
                            tool.GetComponent<Outline>().OutlineColor = gameObject.GetComponent<Outline>().OutlineColor;
                        }
                    }
                    break;
            }
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
                        SetColors();
                    }
                    else
                    {
                        collectItem();
                        checkItem();
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
                        gameObject.GetComponent<Animation>().tongs.SetActive(false);
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
            }
            if (context.canceled)
            {
                activeTool.GetComponent<Tool>().charging = false;
            }
        }
    }

    //ACCESSORY

    void positionHeldObj()
    {
        heldObj.transform.parent = LHand;
        heldObj.transform.localPosition = new Vector3(-0.47f, 0.93f, 0.17f);
        heldObj.transform.localRotation = Quaternion.Euler(Vector3.zero);

        heldObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

    }

}
