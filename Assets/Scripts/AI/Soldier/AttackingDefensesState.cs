using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using Anthill.AI;
using UnityEngine;

public class AttackingDefensesState : AntAIState
{
    public SoldierModel soldierModel;

    public GameObject owner;
    public Transform target;

    public bool canAttack = true;
    
    private float attackTimer;
    public float damage;

    public override void Create(GameObject aGameobject)
    {
        base.Create(aGameobject);

        owner = aGameobject;
    }
    
    public override void Enter()
    {
        base.Enter();

        soldierModel = owner.GetComponent<SoldierModel>();

	    target = soldierModel.target;
	    
        canAttack = true;

        if (target != null)
        {
	        target.GetComponent<Tom.Health>().DeathEvent += LeaveState;
        }
    }

    private void OnDisable()
    {
	    if (target != null)
	    {
		    target.GetComponent<Tom.Health>().DeathEvent -= LeaveState;
	    }
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        // Tom: NOTE removed all pathfinding code here, should all be handled by MoveToTarget state
        
        //CheckRange();

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0 && soldierModel.inRange)
        {
            if (canAttack)
            {
                canAttack = false;
                Attack();
                attackTimer = soldierModel.attackCooldown;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void CheckRange()
    {
        float distance = Vector3.Distance(owner.transform.position, target.transform.position);

        // Tom: Changed this to force soldier to switch to MoveToTarget state when out of range
        if (distance > soldierModel.range)
        {
            soldierModel.inRange = false;
        }
    }

    public void Attack()
    {
        target.GetComponent<Tom.Health>().ChangeHealth(-damage, owner);
        canAttack = true;
        soldierModel.anim.SetTrigger("Attack");
    }

    public void LeaveState(GameObject go)
    {
        // Tom: Added some extra bool changes here
        target.GetComponent<Tom.Health>().DeathEvent -= LeaveState;
        soldierModel.target = null;
        soldierModel.attackedByTower = false;
        soldierModel.hasTarget = false;
        soldierModel.inRange = false;
        soldierModel.castlePathBlocked = false;
        Finish();
    }
}
