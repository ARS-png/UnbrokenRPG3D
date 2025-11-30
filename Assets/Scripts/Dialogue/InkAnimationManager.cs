using System;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class InkAnimationManager : MonoBehaviour 
{

    [SerializeField] private CharacterList characterList;

    private void OnEnable()
    {
        GameEventsManager.instance.animationEvents.onAnimationPlay += AnimationPlay;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.animationEvents.onAnimationPlay -= AnimationPlay;
    }

    private void AnimationPlay(string currentSpeaker, string triggerAnimationName)
    {
        Animator animator = characterList.GetCharacterAnimator(currentSpeaker);
        animator.SetTrigger(triggerAnimationName);

    }
}
