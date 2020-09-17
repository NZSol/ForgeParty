using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreLifetime : MonoBehaviour
{

    float deathTime = 5;

    // Update is called once per frame
    void Update()
    {
        if (transform.parent == null)
        {
            deathTime -= Time.deltaTime;
        }

        if (deathTime < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
