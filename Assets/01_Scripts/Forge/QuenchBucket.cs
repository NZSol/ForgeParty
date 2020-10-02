using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class QuenchBucket : MonoBehaviour
{

    public bool Count = false;

    public float timer;
    float maxVal;

    public GameObject item;
    public GameObject inputObj;

    [SerializeField] Slider _slide;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _slide.value = timer;
        if (Count)
        {
            timer += Time.deltaTime;
            if (timer > maxVal)
            {
                Count = false;
                timer = 0;
            }
            
            //CountdownEvent Upwards;
            //When count == 100%, Out put == objectl
        }
    }
}
