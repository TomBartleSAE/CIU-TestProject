using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class SoldierMoveToTargetState : AntAIState
{
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Moving to Keep State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        Debug.Log("Executing Moving to Keep State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Moving to Keep State");
    }
}