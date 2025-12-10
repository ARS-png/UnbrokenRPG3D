using Unity.VisualScripting;
using UnityEngine;

public class AttackState : State
{

    float timePassed;
    float clipLength;
    float clipSpeed;
    bool attack;


    public AttackState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Attack state is startted");

        attack = false;
        character.animator.applyRootMotion = true;
        timePassed = 0f;
        character.animator.SetTrigger("attack");
        character.animator.SetFloat("moveAmount", 0f);   
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (attackAction.triggered)
        { 
            attack = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        timePassed += Time.deltaTime;
        clipLength = character.animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
        clipSpeed = character.animator.GetCurrentAnimatorStateInfo(1).speed;

        if (timePassed >= clipLength / clipSpeed && attack) 
        {
            stateMachine.ChangeState(character.attacking);
            character.animator.SetTrigger("attack");   
        }

        if (timePassed >= clipLength / clipSpeed) 
        {
            stateMachine.ChangeState(character.combatting);
        }    
    }

    public override void Exit() //выходит до завершения анимации"
    {
        base.Exit();
        Debug.Log("Attack state is exited");
        character.animator.applyRootMotion = false;
    }

}
