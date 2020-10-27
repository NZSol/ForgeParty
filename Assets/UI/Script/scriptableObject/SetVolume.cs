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

    private static SetVolume instance;
    public static SetVolume Instance { get { return Instance; } }
    void Awake()
    {
        instance = this;
    }


   

    public Settings Settings;





    private void Start()
    {
        VolumeSlider.value = Settings.Volume;
        bgMusic.Play();
    }




   public  void ChangeVolume()
    {
      
    }


    private void Update()
    {
        Settings.Volume = VolumeSlider.value;

        bgMusic.volume = VolumeSlider.value;
    }


}
