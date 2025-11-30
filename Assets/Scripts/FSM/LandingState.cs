
using UnityEngine;

public class LandingState : State
{
    float timePassed;
    float landingTime;

    public LandingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        character.animator.SetBool("IsInAir", false);
   

        timePassed = 0f;
        
        landingTime = 0.0f;
        //Debug.Log("Landing");
    }

    public override void LogicUpdate()
    {

        base.LogicUpdate();

        //if (sprintAction.triggered)
        //{
        //    stateMachine.ChangeState(character.sprinting);
        //}

        if (timePassed > landingTime)
        {


            //stateMachine.ChangeState(character.sprinting);

            if (character.animator.GetBool("isCombat"))
            {
                stateMachine.ChangeState(character.combatting);
            }
            else
            {
                stateMachine.ChangeState(character.standing);
            }
             

        }
        timePassed += Time.deltaTime;
    }
}