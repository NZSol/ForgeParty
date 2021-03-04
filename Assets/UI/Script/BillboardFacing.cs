using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardFacing : MonoBehaviour
{
    [SerializeField]
    GameObject cam = null;
    // Update is called once per frame
    private void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
    }
    void Update()
    {
        gameObject.transform.LookAt(cam.transform);
    }
}
