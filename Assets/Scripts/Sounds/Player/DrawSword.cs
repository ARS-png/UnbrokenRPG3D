using UnityEngine;

public class DrawSword : MonoBehaviour
{
    public AudioClip audioClip;

    private void DrawSwardSoundPlay() 
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }


}
