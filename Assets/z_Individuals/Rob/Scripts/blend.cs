using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blend : MonoBehaviour
{

    float weight = 0;
    bool increase;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weight > 2)
        {
            increase = false;
        }
        else if (weight < 0)
        {
            increase = true;
        }


        if (increase)
        {
            weight += Time.deltaTime / 4;
        }
        else
        {
            weight -= Time.deltaTime / 4;
        }
        anim.SetFloat("Blend", weight);
    }
}
