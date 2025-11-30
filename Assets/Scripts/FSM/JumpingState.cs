using UnityEngine;

public class JumpingState : State
{
    //bool grounded;

    public float gravityValue;
    float jumpHeight;

  

    public JumpingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        Debug.Log("OnEnterJump");
        base.Enter();
 

        gravityValue = character.gravityValue;
        jumpHeight = character.jumpHeight;
       
        gravityVelocity.y = 0f;

        character.animator.SetTrigger("Jump");
        Jump();
        //character.falling.SetInitialVelocity(gravityVelocity);
        character.falling.changerGravityVelocityFromJumpToFall = gravityVelocity.y;
     
        character.controller.Move(gravityVelocity * Time.deltaTime);

        // Переходим в falling и передаем текущую velocity


      
        stateMachine.ChangeState(character.falling);
    }

    public override void HandleInput()
    {
        base.HandleInput();

        input = moveAction.ReadValue<Vector2>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void PhisicsUpdate()
    {
        base.PhisicsUpdate();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    void Jump()
    {
        character.animator.SetBool("IsInAir", true);
        gravityVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //character.falling.
        Debug.Log("jump");

        
    }

    public void GetGravity() 
    {
        
    }
  
}
