using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioSource bgMusic;

    public Slider VolumeSlider;

    public AudioMixer mixer;

    public Dropdown Details;

    public Dropdown ResolutionDropdown;

    public Settings Settings;
    Resolution[] Resolution;

    [SerializeField] GameObject myParent = null;
    [SerializeField] Text ResolutionText = null;

    bool settingsOpen = true;

    public bool inEditor;

    private void Start()
    {
        if (inEditor)
        {
            PlayerPrefs.SetInt("AmISet", 0);
        }
        StartCoroutine(DisableSettingsOnDelay());
        Resolution = Screen.resolutions;
        ResolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        int width, height;
        for (int i = 0; i < Resolution.Length; i++)
        {
            string option = Resolution[i].width + "x" + Resolution[i].height;
            options.Add(option);

            if (PlayerPrefs.GetInt("AmISet") == 0)
            {
                print("running");
                if (Resolution[i].width == 1920 && Resolution[i].height == 1080)
                {
                    PlayerPrefs.SetInt("resolution", i);
                    PlayerPrefs.SetInt("AmISet", 1);
                }
            }
        }

        if (PlayerPrefs.GetInt("AmISet") == 1)
        {
            currentResolutionIndex = PlayerPrefs.GetInt("resolution");
            for (int i = 0; i < Resolution.Length; i++) 
            { 
                if (i == currentResolutionIndex)
                {
                    width = Resolution[i].width;
                    height = Resolution[i].height;

                    Screen.SetResolution(width, height, true);
                }
            }
        }

        ResolutionDropdown.AddOptions(options);

        ResolutionDropdown.value = currentResolutionIndex;
       
        ResolutionDropdown.RefreshShownValue();


        Details.value = Settings.detail;
       
        VolumeSlider.value = Settings.Volume;
    }


    IEnumerator DisableSettingsOnDelay()
    {
        yield return new WaitForSeconds(0.0001f);
        changeBool();
        myParent.SetActive(false);
    }


    public void changeBool()
    {
        settingsOpen = !settingsOpen;
        print(PlayerPrefs.GetInt("resolution"));
    }

    private void Update()
    {
        if (settingsOpen == true)
        {
            SetVolumeM();

            SetDetail();

            SetResolution();
        }
    }

   void SetDetail()
    {
        Settings.detail = Details.value;

        QualitySettings.SetQualityLevel(Details.value);
    }

    void SetResolution()
    {
        Settings.resolution = ResolutionDropdown.value;
        PlayerPrefs.SetInt("resolution", ResolutionDropdown.value);
        print("Setting Resolution to:   " + PlayerPrefs.GetInt("resolution"));

        Resolution newResolution = Resolution[PlayerPrefs.GetInt("resolution")];
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
        
        if (Screen.currentResolution.width != newResolution.width && Screen.currentResolution.height != newResolution.height)
        {
            print("Resolution not same");
            print(newResolution + "   DesiredResolution");
            print(Screen.currentResolution + "   CurResolution");
        }
        else
        {
            print("Resolution Same");
        }
    }

    void SetVolumeM()
    {
        Settings.Volume = VolumeSlider.value ;

        mixer.SetFloat("VolumeMaster",VolumeSlider.value) ;

    }


}
