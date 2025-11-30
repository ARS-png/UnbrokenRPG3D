using UnityEngine;

public class LandingSound : MonoBehaviour
{
    [SerializeField] float footStepVolume;
    public AudioClip audioClip;

    private void LandingSoundPlay()
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position, footStepVolume);
    }
}
