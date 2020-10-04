using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEngine.InputSystem.InputAction;

public class Interact : MonoBehaviour
{
    public bool active = false;
    bool inputArmed = true;

    //LayerMasks
    LayerMask toolsLayer;

 
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

    GameObject closestTool(GameObject[] tools)
    {
        GameObject tool = null;
        float minDist = Mathf.Infinity;
        Vector3 curPos = transform.position;
        foreach (GameObject obj in tools)
        {
            float dist = Vector3.Distance(obj.transform.position, transform.position);
            Debug.Log(obj.name + " " + dist);
            if (dist < minDist)
            {
                tool = obj;
                minDist = dist;
            }
        }
        return tool;
    }

    public GameObject activeTool;


    // Start is called before the first frame update
    void Start()
    {
        if (active)
        {
            toolsLayer = LayerMask.NameToLayer("Tools");
            Interactables = Tools(toolsLayer).ToList();
        }
    }

    // Update is called once per frame
    void Update()
    {
        activeTool = closestTool(Interactables.ToArray());
    }

    public void InteractPress(CallbackContext context)
    {
        if (active)
        {
            if (context.started && inputArmed)
            {
                //button Action
                inputArmed = false;
            }
            if (context.canceled)
            {
                inputArmed = true;
            }
        }
    }


    public void InteractHold(CallbackContext context)
    {
        if (active)
        {
            if (context.started)
            {

            }
            if (context.canceled)
            {

            }
        }
    }

}
