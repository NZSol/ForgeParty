using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    public Transform point;
    Camera mainCam = null;
    // Update is called once per frame
    private void Start()
    {
        mainCam = Camera.main;
    }
    void Update()
    {
        transform.position = mainCam.WorldToScreenPoint(point.position);
    }
}
