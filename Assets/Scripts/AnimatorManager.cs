using UnityEngine;

public class AnimatorManager
{
    private Animator animator;

    public AnimatorManager(Animator animator)
    {
        this.animator = animator;
    }

    public void InvokeAnimatorTrigger(string triggerName)
    { 
        animator.SetTrigger(triggerName);
    }


}
