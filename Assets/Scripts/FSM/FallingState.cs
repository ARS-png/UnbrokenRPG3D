
using UnityEngine;

public class FallingState : State
{
    bool grounded;

    float gravityValue;
    float jumpHeight;
    float playerSpeed;

    public float changerGravityVelocityFromJumpToFall;//

    Vector3 airVelocity;

    public FallingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
        
    }

    public override void Enter()
    {
  
        base.Enter();
   
        grounded = false;
        gravityValue = character.gravityValue;
        jumpHeight = character.jumpHeight;
        playerSpeed = character.playerSpeed;
        gravityVelocity.y = changerGravityVelocityFromJumpToFall;

 
   
    }

    public override void HandleInput()
    {
        base.HandleInput();

        input = moveAction.ReadValue<Vector2>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (grounded)
        {
            stateMachine.ChangeState(character.landing);
        }

    }

    public override void PhisicsUpdate()
    {
        base.PhisicsUpdate();
        if (!grounded)
        {
            velocity = character.playerVelocity;
            airVelocity = new Vector3(input.x, 0, input.y);

            velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
            velocity.y = 0;
            airVelocity = airVelocity.x * character.cameraTransform.right.normalized +airVelocity.z *  character.cameraTransform.forward.normalized;
            airVelocity.y = 0;

            character.controller.Move(gravityVelocity * Time.deltaTime + (airVelocity * character.airControl + velocity * (1 - character.airControl)) * playerSpeed * Time.deltaTime);
        }

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
    }

    public override void Exit()
    {
        base.Exit();
    }

   
}
