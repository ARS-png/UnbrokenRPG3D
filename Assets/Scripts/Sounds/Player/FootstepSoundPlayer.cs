
using UnityEngine;

public class FootstepSoundPlayer : MonoBehaviour
{
    [SerializeField] float footStepVolume;
    public AudioClip[] Clips;
    public Animator Animator;
    private float _lastFootstep;    

    private void OnValidate()
    {
        if (!Animator) Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var footstep = Animator.GetFloat("Footstep");
        if (Mathf.Abs(footstep) < 0.001f) footstep = 0f;

        if (Animator.GetBool("IsInAir"))
        {
            return;
        }

        if (_lastFootstep > 0 && footstep < 0 || _lastFootstep < 0 && footstep > 0)
        {
            var randomClip = Clips[Random.Range(0, Clips.Length - 1)];
            AudioSource.PlayClipAtPoint(randomClip, transform.position, footStepVolume);
        }

        _lastFootstep = footstep;
    }
    
    public void FootstepSound()
    {
        var randomClip = Clips[Random.Range(0, Clips.Length - 1)];
        AudioSource.PlayClipAtPoint(randomClip, transform.position);
    }
}
