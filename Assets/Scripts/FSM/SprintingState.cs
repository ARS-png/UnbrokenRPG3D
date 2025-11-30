using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SprintingState : State
{
    float gravityValue;
    Vector3 currentVelocity;

    bool grounded;
    bool sprint;
    float playerSpeed;
    bool sprintJump;
    Vector3 cVelocity;

    public SprintingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        character.animator.SetBool("isRunning", true);

        sprint = false;
        sprintJump = false;
        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        playerSpeed = character.sprintSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;


    }

    public override void HandleInput()
    {
        base.HandleInput();
        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0;

        if (sprintAction.triggered || input.sqrMagnitude == 0f) // tuta
        {
            sprint = false;
        }//добавить какоето условие выхода
        else
        {
            sprint = true;
        }
        if (jumpAction.triggered)
        {
            sprintJump = true;
            //stateMachine.ChangeState(character.jumping);
        }
    }

    public override void LogicUpdate()//or here
    {
        base.LogicUpdate();
        if (sprint)
        {
            character.animator.SetFloat("runAmount", input.magnitude + 0.5f, character.SpeedDampTime, Time.deltaTime);
        }
        else
        {
            character.animator.SetBool("isRunning", false);
            //добавить условие про combat layer
            if (character.animator.GetBool("isCombat"))
            {
                stateMachine.ChangeState(character.combatting);
            }
            else 
            {
                stateMachine.ChangeState(character.standing);
            }
                
        }
        if (sprintJump)
        {
            character.animator.SetBool("isRunning", false);
            stateMachine.ChangeState(character.jumping);
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
        character.animator.SetFloat("runAmount", 0);
    }


}
