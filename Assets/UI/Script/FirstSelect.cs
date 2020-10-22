using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FirstSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject FirstObject;
    [SerializeField] Image[] overallImgArray;
    [SerializeField] List<Image> imgList = new List<Image>();

    [SerializeField] Sprite imgActive;
    [SerializeField] Sprite ImageInactive;
    [SerializeField] Sprite borderActive;
    [SerializeField] Sprite borderInactive;


    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(FirstObject);
        overallImgArray = gameObject.GetComponentsInChildren<Image>();
        foreach (Image img in overallImgArray)
        {
            if (img.tag == ("Button"))
            {
                imgList.Add(img);
            }
        }
    }
}
