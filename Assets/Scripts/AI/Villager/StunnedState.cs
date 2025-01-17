using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Anthill.AI;
using Unity.VisualScripting;
using UnityEngine;

public class StunnedState : AntAIState
{
    public GameObject owner;
    public VillagerModel villager;

    public event Action StunBeginEvent;
    public event Action StunEndEvent;

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        
        owner = aGameObject;
        villager = aGameObject.GetComponentInParent<VillagerModel>();
    }

    public override void Enter()
    {
        base.Enter();

        villager.GetComponent<FollowPath>().moveSpeed = 0;
        
        villager.stunParticles.Play();

        StartCoroutine(StunnedTimer());
    }
    
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
    }

    public override void Exit()
    {
        base.Enter();
    }

    IEnumerator StunnedTimer()
    {
        yield return new WaitForSeconds(villager.stunTime);

        villager.isStunned = false;
        villager.isScared = true;
        villager.stunParticles.Stop();

        Finish();
    }
}
