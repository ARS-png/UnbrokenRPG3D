using UnityEngine;
using UnityEngine.Rendering;

public class CrouchingState : State
{
    float playerSpeed;
    bool belowCeiling;
    bool crouchHeld;

    bool grounded;
    float gravityValue;
    Vector3 currentVelocity;


    public CrouchingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        character.animator.SetBool("isCrouching", true);
        belowCeiling = false;
        crouchHeld = false;
        gravityVelocity.y = 0;

        playerSpeed = character.crouchSpeed;
        character.controller.height = character.crouchColliderHeight;
        character.controller.center = new Vector3(0f, character.crouchColliderHeight / 2f, 0f);
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;
    }

    public override void Exit() 
    {
        base.Exit();
        character.controller.height = character.normalColliderHeight;
        character.controller.center = new Vector3(0f, character.normalColliderHeight / 2f, 0f);
        gravityVelocity.y = 0;
        character.playerVelocity = new Vector3(input.x, 0, input.y);
        character.animator.SetBool("isCrouching", false);

    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (crouchAction.triggered && !belowCeiling)
        {
            crouchHeld = true;
        }

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        character.animator.SetFloat("moveAmount", input.magnitude, character.SpeedDampTime, Time.deltaTime);

        if (crouchHeld) 
        {
            stateMachine.ChangeState(character.standing);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
   
        gravityVelocity.y = gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0;
        }

        currentVelocity = Vector3.Lerp(currentVelocity, velocity, character.velocityDampTime);

        character.controller.Move(currentVelocity * Time.deltaTime  * playerSpeed + gravityVelocity * Time.deltaTime);

        if (velocity.magnitude > 0) 
        {
            character.transform.rotation = Quaternion.Lerp(character.transform.rotation,Quaternion.LookRotation(velocity),character.rotationDampTime);
        }


    }
}
