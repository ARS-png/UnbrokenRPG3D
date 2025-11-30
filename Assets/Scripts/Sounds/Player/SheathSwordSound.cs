using UnityEngine;

public class SheathSwordSound : MonoBehaviour
{
    [SerializeField] float sheathWeaponVolume;

    public AudioClip audioClip;

    private void SheathSwordSoundPlay()
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position, sheathWeaponVolume);
    }
}
