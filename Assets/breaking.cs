using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breaking : MonoBehaviour
{
    public GameObject brokenVersion;


   void OnMouseDown()
    {
        Instantiate(brokenVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
