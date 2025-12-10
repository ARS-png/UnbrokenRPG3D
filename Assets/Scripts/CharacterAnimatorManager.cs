using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    AnimatorManager characterAnimatorManager;

    private void Awake()
    {
        characterAnimatorManager = new AnimatorManager(animator);
    }

}
