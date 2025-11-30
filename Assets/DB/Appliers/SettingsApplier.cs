using UnityEngine;

public class SettingsApplier : MonoBehaviour
{
    public void ApplySettings(Settings settings)
    {

        QualitySettings.SetQualityLevel(settings.QualityLevel);
        AudioListener.volume = settings.GameVolume;



        Debug.Log("Volume:" + settings.GameVolume);
        Debug.Log("Quality:" + settings.QualityLevel);
    }



    public void ApplyDefaultSettings()
    {

        QualitySettings.SetQualityLevel(0);
        AudioListener.volume = 1.0f;
    }
}
