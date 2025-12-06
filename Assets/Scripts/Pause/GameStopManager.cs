using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(GameStopManager))]
public class GameStopManager : MonoBehaviour
{

    [Header("PlayerInput")]
    [SerializeField] PlayerInput playerInput;
    [Space]

    [Header("Audio")]
    [SerializeField] AudioClip clip;

    private InputAction pauseAction;
    private InputAction inventoryShowCloseAction;

    private Animator[] animators;

    public event System.Action OnGamePaused;
    public event System.Action OnGameResumed;

    public static GameStopManager Instance;

    private bool isGameActive = true;
    private bool isGamePaused = false;
    private bool isInventoryOpen = false;

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
            inventoryShowCloseAction = playerInput.actions.FindAction("Inventory");

            if (inventoryShowCloseAction == null)
            {
                Debug.LogError("Noetskjsdhf");
            }

            pauseAction.performed += OnPauseClick;
            inventoryShowCloseAction.performed += OnInventoryShowCloseClick;
            pauseAction.Enable();
            inventoryShowCloseAction.Enable();
        }

        Debug.Log("PauseManager setup complete");

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.audioSource.ignoreListenerPause = true;
        }
    }


    private void OnPauseClick(InputAction.CallbackContext context)
    {
        if (isInventoryOpen)
        {
            ResumeGame();
            isGamePaused = false;
            isInventoryOpen = false;
        }

        if (!isGameActive || isInventoryOpen) return;

        OnGamePaused?.Invoke();

        if (Time.timeScale > 0)
        {
            StopGame();
            isGamePaused = true;
        }
        else
        {
            ResumeGame();
            isGamePaused = false;
        }
    }

    private void OnInventoryShowCloseClick(InputAction.CallbackContext context)
    {
        if (!isGameActive || isGamePaused) return;

        GameEventsManager.instance.inventoryEvents.ShowInventoryPanel();

        if (Time.timeScale > 0)
        {
            StopGame();
            isInventoryOpen = true;
        }
        else
        {
            ResumeGame();
            isInventoryOpen = false;
        }
    }

    public void StopGame()
    {

        if (!isGameActive) return;

        Time.timeScale = 0;

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
        GameEventsManager.instance.inventoryEvents.CloseInventoryPanel();


        Time.timeScale = 1;
        //Debug.Log("Game Resumed");

        if (playerInput != null)
        {
            playerInput.SwitchCurrentActionMap("Player");
        }

        if (pauseAction != null)
        {
            pauseAction.Enable();
            inventoryShowCloseAction.Enable();
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