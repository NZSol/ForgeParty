using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardFacing : MonoBehaviour
{
    [SerializeField] GameObject cam = null;

    private void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
    }
    private void Update()
    {
        gameObject.transform.LookAt(cam.transform);
    }
}
