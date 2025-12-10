using System;
using UnityEngine;

public class AnimationEvents
{
    public event Action<string,string> onAnimationPlay;

    public void PlayAnimation(string currentSpeaker, string triggerAnimationName) =>
        onAnimationPlay.Invoke(currentSpeaker, triggerAnimationName);


}
