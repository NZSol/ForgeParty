using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    public Transform point;
    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(point.position);
    }
}
