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
    int curHeight = 0;
    int curWidth = 0;
    Resolution curRes;
    private void Start()
    {
        StartCoroutine(DisableSettingsOnDelay());

        #region ResolutionAssignment

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
                if (Resolution[i].width == 1920 && Resolution[i].height == 1080)
                {
                    PlayerPrefs.SetInt("resolution", i);
                    PlayerPrefs.SetInt("AmISet", 1);
                    curRes = Resolution[i];
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
                    curRes = Resolution[i];
                    Screen.SetResolution(width, height, true);
                }
            }
        }

        ResolutionDropdown.AddOptions(options);

        ResolutionDropdown.value = currentResolutionIndex;
       
        ResolutionDropdown.RefreshShownValue();
        #endregion

        #region QualityAssignment

        if (PlayerPrefs.GetInt("qualitySettingBool") == 0)
        {
            PlayerPrefs.SetInt("qualitySettingBool", 1);
            PlayerPrefs.SetInt("myQuality", 3);
            Settings.detail = PlayerPrefs.GetInt("myQuality");
            Details.value = PlayerPrefs.GetInt("myQuality");
        }
        else
        {
            Settings.detail = PlayerPrefs.GetInt("myQuality");
            Details.value = PlayerPrefs.GetInt("myQuality");
        }
        #endregion


        #region VolumeAssignment
        VolumeSlider.value = PlayerPrefs.GetFloat("volume");

        #endregion
    }

    private void Update()
    {
        if (LevelSelect.instance != null)
        {
            if (LevelSelect.instance.settingsDisable && myParent.activeSelf)
            {
                myParent.SetActive(false);
                LevelSelect.instance.settingsDisable = false;
            }
        }
    }

    IEnumerator DisableSettingsOnDelay()
    {
        yield return new WaitForSeconds(0.0001f);
        myParent.SetActive(false);
    }


    public void SetDetail()
    {
        PlayerPrefs.SetInt("myQuality", Details.value);
        Settings.detail = PlayerPrefs.GetInt("myQuality");
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("myQuality"));
    }

    public void SetResolution()
    {
        Settings.resolution = ResolutionDropdown.value;
        PlayerPrefs.SetInt("resolution", ResolutionDropdown.value);

        Resolution newResolution = Resolution[PlayerPrefs.GetInt("resolution")];
        if (curRes.width != newResolution.width && curRes.height != newResolution.height)
        {
            Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
        }
    }

    public void SetVolumeM()
    {
        print("Setting Resolution");
        PlayerPrefs.SetFloat("volume", VolumeSlider.value);
        Settings.Volume = PlayerPrefs.GetFloat("volume");

        mixer.SetFloat("VolumeMaster", (int)Settings.Volume);
    }


}
