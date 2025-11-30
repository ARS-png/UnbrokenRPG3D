using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;
using System;
using UnityEditor.Build.Pipeline;// чтобы избежать неоднозначности


public class MusicSettings : MonoBehaviour
{


    public Slider sliderVolumeMusic;
    
  
  
    private float volume;

    private void Start()
    {
        Initialize();

        if (DatabaseManager.Instance != null)
        {
            DatabaseManager.Instance.OnUserLoggedIn += LoadCurrentVolumeSettings;
        }

        LoadCurrentVolumeSettings();


    }


    private void Initialize()
    {
        sliderVolumeMusic.onValueChanged.AddListener(OnVolumeChange);
    }

    public void GetSliderValue()
    {
        volume = sliderVolumeMusic.value;   
    }

    public void ApplyMisic()
    {
        AudioListener.volume = volume;
        sliderVolumeMusic.value = volume;
    }

    public void LoadCurrentVolumeSettings()
    {
       

        if (DatabaseManager.Instance != null && DatabaseManager.Instance.IsUserLoggedIn() && sliderVolumeMusic != null)
        {
            Settings settings = DatabaseManager.Instance.GetCurrentUserSettings();
            if (settings != null)
            {
                sliderVolumeMusic.SetValueWithoutNotify(settings.GameVolume);
            }
        }
    }

    private void OnVolumeChange(float _volume)
    {
        if (DatabaseManager.Instance != null && DatabaseManager.Instance.IsUserLoggedIn())
        {
            Settings userSettings = DatabaseManager.Instance.GetCurrentUserSettings();
            if (userSettings != null)
            {
                userSettings.GameVolume = _volume;
                bool success = DatabaseManager.Instance.UpdateCurrentUserSettings(userSettings);

                if (success)
                {

                    AudioListener.volume = _volume;

                    Debug.Log($"✅ Volume saved: {AudioListener.volume}");


                }
            }
        }
    }


    private void OnDestroy()
    {
        if (DatabaseManager.Instance != null)
        {
            DatabaseManager.Instance.OnUserLoggedIn -= LoadCurrentVolumeSettings;
        }
    }







}
