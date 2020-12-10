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

    private void Start()
    {
        Resolution = Screen.resolutions;
        ResolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < Resolution.Length; i++)
        {
            string option = Resolution[i].width + "x" + Resolution[i].height;
            options.Add(option);

            if (Resolution[i].width==Screen.currentResolution.width &&
                Resolution[i].height==Screen.currentResolution.height)
            {

                currentResolutionIndex = i;
            }


        }

        ResolutionDropdown.AddOptions(options);
        currentResolutionIndex = Settings.resolution;
        ResolutionDropdown.value = Settings.resolution;
       
        ResolutionDropdown.RefreshShownValue();


        


        Details.value = Settings.detail;
       
        VolumeSlider.value = Settings.Volume;

       
       
    }

    private void Update()
    {
        SetVolumeM();


        SetDetail();

        SetResolution();


    }

   void SetDetail()
    {
        Settings.detail = Details.value;

        QualitySettings.SetQualityLevel(Details.value);
    }

    void SetResolution()
    {
        Settings.resolution = ResolutionDropdown.value;

        Resolution resolution = Resolution[ResolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    void SetVolumeM()
    {
        Settings.Volume = VolumeSlider.value ;

        mixer.SetFloat("VolumeMaster",VolumeSlider.value) ;

    }


}
