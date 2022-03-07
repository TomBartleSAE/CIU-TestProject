using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class ReturnToKeepState : AntAIState
{
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entering Return to Keep State");
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        Debug.Log("Executing Return to Keep State");
    }

    public override void Exit()
    {
        base.Exit();
        
        Debug.Log("Exiting Return to Keep State");
    }
}
