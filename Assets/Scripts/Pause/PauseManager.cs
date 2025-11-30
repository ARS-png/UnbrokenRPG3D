using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(PauseManager))]
public class PauseManager : MonoBehaviour
{

    [Header("PlayerInput")]
    [SerializeField] PlayerInput playerInput;
    [Space]

    [Header("Audio")]
    [SerializeField] AudioClip clip;

    private InputAction pauseAction;
    private Animator[] animators;

    public event System.Action OnGamePaused;
    public event System.Action OnGameResumed;

    public static PauseManager Instance;

    private bool isGameActive = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;        
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {       

        animators = FindObjectsByType<Animator>(FindObjectsSortMode.None);

        if (playerInput != null)
        {
            pauseAction = playerInput.actions.FindAction("Pause");
            pauseAction.performed += OnPauseClick;
            pauseAction.Enable();
        }

        Debug.Log("PauseManager setup complete");

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.audioSource.ignoreListenerPause = true;
        }
    }


    private void OnPauseClick(InputAction.CallbackContext context)
    {
    
        if (!isGameActive) return;

        OnGamePaused?.Invoke();

        if (Time.timeScale > 0)
        {
            StopGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void StopGame()
    {
      
        if (!isGameActive) return;

        Time.timeScale = 0;
        //Debug.Log("Game Paused");

        if (playerInput != null)
        {
            playerInput.SwitchCurrentActionMap("UI");
        }

        foreach (var animator in animators)
        {
            if (animator != null)
            {
                animator.speed = 0f;
            }
        }

        if (clip != null)
        {
            SoundManager.PlaySound(clip);
        }

        AudioListener.pause = true;
    }

    public void ResumeGame()
    {

        if (!isGameActive) return;

        OnGameResumed?.Invoke();
        Time.timeScale = 1;
        //Debug.Log("Game Resumed");

        if (playerInput != null)
        {
            playerInput.SwitchCurrentActionMap("Player");
        }

        if (pauseAction != null)
        {
            pauseAction.Enable();
        }

        foreach (var animator in animators)
        {
            if (animator != null)
            {
                animator.speed = 1f;
            }
        }

        AudioListener.pause = false;
    }


    private void OnDestroy()
    {
        if (pauseAction != null)
        {
            pauseAction.performed -= OnPauseClick;
            pauseAction.Disable();
        }
    }
}