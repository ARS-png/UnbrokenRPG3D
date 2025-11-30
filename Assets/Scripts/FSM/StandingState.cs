using JetBrains.Annotations;
using UnityEngine;

public class StandingState : State
{
    float gravityValue;
    bool jump;
    bool crouch;
    Vector3 currentVelocity;
    bool grounded;
    bool sprint;
    float playerSpeed;

    Vector3 cVelocity;

    bool sprintHeld;

    bool drawWeapon;

    public StandingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter(); 

        character.animator.SetBool("drawWeapon", false);

        sprintHeld = sprintAction.IsPressed();

        jump = false;
        crouch = false;
        sprint = false;
        drawWeapon = false;
        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0f;

        playerSpeed = character.playerSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;


    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (jumpAction.triggered)
        {
            jump = true;
        }

        if (crouchAction.triggered)
        {
            crouch = true;
        }

        if (sprintAction.IsPressed())
        {
            sprint = true;
        }

        if (drawWeaponAction.triggered)
        {
            drawWeapon = true;
        }


        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;

        sprintHeld = sprintAction.IsPressed();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        character.animator.SetFloat("moveAmount", input.magnitude, character.SpeedDampTime, Time.deltaTime);

        if (sprint || sprintHeld)
        {
            stateMachine.ChangeState(character.sprinting);
        }

        if (jump)
        {
            stateMachine.ChangeState(character.jumping);
        }

        if (crouch)
        {
            stateMachine.ChangeState(character.crouching);
        }

        if (drawWeapon)
        {
            stateMachine.ChangeState(character.combatting);

            character.animator.SetBool("drawWeapon", true);


        }

    }

    public override void PhisicsUpdate()
    {
        base.PhisicsUpdate();

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;

        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }

        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);
        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);

        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }
    }

    public override void Exit()
    {
        base.Exit();

        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0, input.y);

        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.LookRotation(velocity);
        }


    }
}
