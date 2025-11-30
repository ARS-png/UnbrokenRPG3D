using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;


public class WitchSound : MonoBehaviour
{
    [SerializeField] private float volume = 1f;
    [SerializeField] private float minDelay = 3f;
    [SerializeField] private float maxDelay = 7f;
    public AudioClip[] clips;

    private void Start()
    {
        if (clips.Length == 0)
        {
            Debug.LogError("Witch has not sounds");
            Destroy(this);
            return;
        }

        StartCoroutine(PlaySoundRoutine());
    }

    private IEnumerator PlaySoundRoutine()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            AudioClip randomClip = clips[Random.Range(0, clips.Length)];
            AudioSource.PlayClipAtPoint(randomClip, transform.position, volume);
        }
    }
}