using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bellows : MonoBehaviour
{
    [SerializeField] Slider _slide;
    [SerializeField] Image _image;

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
            Temperature += Time.deltaTime * 5;
        }
        else
        {
            if (Temperature > 0)
            {
                Temperature -= Time.deltaTime * 2.5f;
            }
        }
        _slide.value = Temperature;
        FC.temperature = Temperature;
        _image.color = Color.Lerp(Color.red, Color.green, Temperature / 100);
        

    }
}
