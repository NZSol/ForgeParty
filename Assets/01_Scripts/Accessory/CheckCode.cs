using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckCode : MonoBehaviour
{
    [SerializeField] InputField textInputField = null;
    
    // Start is called before the first frame update
    void Start()
    {
        if (LevelSelect.instance.setPrefFalse)
        {
            PlayerPrefs.SetInt("TutorsCode", 0);
        }
    }

    public void enableTutors(string text)
    {
        if (text == "Tutors")
        {
            PlayerPrefs.SetInt("TutorsCode", 1);

            textInputField.text = "Code Accepted";
            textInputField.textComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
            print(PlayerPrefs.GetInt("TutorsCode"));
        }
        else
        {
            textInputField.text = string.Empty;
        }
    }
}
