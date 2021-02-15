using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalizedCenter : MonoBehaviour
{

    [SerializeField] Transform LHand = null;
    [SerializeField] Transform RHand = null;

    void Update()
    {
        Vector3 pos = LHand.transform.position + (RHand.transform.position - LHand.transform.position) / 2;
        Vector3 actualPos = new Vector3(pos.x, pos.y + 0.5f, pos.z + 0.4f);

        transform.position = actualPos;
    }

}
