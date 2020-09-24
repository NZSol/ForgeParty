using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class QuenchBucket : MonoBehaviour
{

    public bool Count = false;

    float timer;
    float maxVal;

    public GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Count)
        {
            timer += Time.deltaTime;
            if (timer > maxVal)
            {
                Count = false;
            }
            
            //CountdownEvent Upwards;
            //When count == 100%, Out put == objectl
        }
    }
}
