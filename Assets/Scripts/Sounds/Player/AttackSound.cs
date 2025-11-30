using UnityEngine;

public class AttackSound : MonoBehaviour
{
    public AudioClip audioClip;

    private void AttackSoundPlay()
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }
}
