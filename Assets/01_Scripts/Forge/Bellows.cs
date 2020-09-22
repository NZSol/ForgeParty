using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bellows : MonoBehaviour
{
    [SerializeField] Slider _slide;

    GameObject Forge;
    ForgeContents FC;

    public float Temperature;
    public bool TempIncrease;

    // Start is called before the first frame update
    void Start()
    {
        Forge = GameObject.FindWithTag("Forge");
        FC = Forge.GetComponent<ForgeContents>();

    }

    // Update is called once per frame
    void Update()
    {
        if (TempIncrease == true)
        {
            Temperature += Time.deltaTime / 1.5f;
        }
        else
        {
            if (Temperature > 0)
            {
                Temperature -= Time.deltaTime / 4;
            }
        }

        _slide.value = Temperature;
        

    }
}
