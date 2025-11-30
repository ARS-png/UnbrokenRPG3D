using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
//using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;

public class Cha : MonoBehaviour
{

    [SerializeField] float moveSpeed = 2.6f;

    [SerializeField] float rotationSpeed = 500f;

    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;

  


    bool isGrounded;
    bool isRunning;

    float ySpeed;

    Quaternion targetRotation;

    CameraController cameraController;

    Animator animator;

    CharacterController characterController;

    [SerializeField] float jumpForce = 2f;
    [SerializeField] float crouchingSpeed = 2.0f;

    Vector3 moveDir;
    float moveAmount;

    [HideInInspector]//
    public PlayerInput playerInput;//

    public StateMachine movementSM;

    private void Start() // вызывается с самого начала 
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();//

        movementSM = new StateMachine();



    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
        var moveInput = (new Vector3(h, 0, v).normalized);
        moveDir = cameraController.PlanarRotation * moveInput;



        GroundCheck();

        GravityHandling();

        Tern();

        animator.SetFloat("moveAmount", moveAmount, 0.15f, Time.deltaTime);

        animator.SetFloat("runAmount", moveAmount, 0.15f, Time.deltaTime);

        IsInAirAnimator();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }


        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            UnCrouch();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (isGrounded)
            {
                moveSpeed = 4.0f;
                animator.SetBool("isRunning", true);
            }
      
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //moveSpeed = Mathf.Lerp(moveSpeed, 2.6f, 0.15f * Time.deltaTime);
            moveSpeed = 2.6f;
            animator.SetBool("isRunning", false);
        }

        //movementSM.currentState.HandleInput();

        Move(moveDir);
    }

    private void UnCrouch()
    {
        animator.SetBool("isCrouching", false);
        moveSpeed = 2.6f;


    }

    private void Crouch()
    {
        if (isGrounded)
        {
            animator.SetBool("isCrouching", true);
            moveSpeed = crouchingSpeed;
            //characterController.height = 1.4f;
            //characterController.center = new Vector3(characterController.center.x, 0.6f, characterController.center.z);
        }
    }

    public void Move(Vector3 moveDir)
    {
        var velocity = moveDir * moveSpeed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);
    }
    private void GravityHandling()
    {
        if (isGrounded)
        {
            ySpeed = -0.5f;
        }
        else
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }
    }

    private void Tern()
    {
        if (moveAmount > 0) // поэтому когда игрок не двигается камера на него не влияет
        {
            targetRotation = Quaternion.LookRotation(moveDir); // создает желаемое вращение
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,
        rotationSpeed * Time.deltaTime); // чтобы поворачивалось медленно
    }

    private void IsInAirAnimator()
    {
        if (isGrounded)
        {
            animator.SetBool("IsInAir", false);
        }
        else
        {
            animator.SetBool("IsInAir", true);
        }
    }

    public void Jump()
    {
        if (animator.GetBool("isCrouching"))
        {
            return;
        }
        if (isGrounded)
        {
            animator.SetTrigger("Jump");
            ySpeed = jumpForce;
        }

    }

    private void GroundCheck()
    {
        //isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
        isGrounded = characterController.isGrounded;
    }

    private void IsPersonRunning() 
    {
        isRunning = animator.GetBool("isRunning");
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 1, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }
}
