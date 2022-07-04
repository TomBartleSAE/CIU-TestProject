using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using Tom;
using UnityEngine;

public class PatrollingState : AntAIState
{
    public GameObject owner;
    public PathfindingAgent pathfinding;
    public GuardModel guard;
    public GameObject[] waypoints;

    public Vector3 pointA;
    public Vector3 pointB;

    public enum PatrolType
    {
        goingTo,
        returning
    };

    public PatrolType patrolType;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        owner = aGameObject;
    }
    
    public override void Enter()
    {
        base.Enter();
        pathfinding = owner.GetComponent<PathfindingAgent>();
        pathfinding.PathCompletedEvent += ChangeDirection;
        //seems like a redundant reference but makes life easier
        guard = owner.GetComponent<GuardModel>();
        waypoints = guard.waypoints;
        guard.vision.angle = 25;
        guard.vision.distance = 2.5f;

        //gets random waypoint from array of possible waypoints
        GetPatrolPoints();

        //allows to find (same patrol route) even if has exited state previously
        FindPatrolPath(pointA, pointB);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        
        if (pathfinding.path.Count != 0)
        {
            if (Vector3.Distance(pathfinding.path[pathfinding.path.Count - 1].coordinates, transform.position) < 0.5)
            {
                //determines which way on the route it's going
                if (patrolType == PatrolType.goingTo)
                {
                    patrolType = PatrolType.returning;
                }
                else if(patrolType == PatrolType.returning)
                {
                    patrolType = PatrolType.goingTo;
                }
            
                PatrolRoute();
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        Finish();
    }

    //Gets points for patrol route per night
    public void GetPatrolPoints()
    {
        int tempPatrolPoint = Random.Range(0, waypoints.Length - 1);
        pointB = waypoints[tempPatrolPoint].transform.position;
    }

    void ChangeDirection()
    {
        GetPatrolPoints();
        PatrolRoute();
    }

    //changes path destination according to patrol direction
    void PatrolRoute()
    {
        switch (patrolType)
        {
            case PatrolType.goingTo :
                FindPatrolPath(transform.position, pointB);
                break;
            case PatrolType.returning :
                FindPatrolPath(transform.position, pointA);
                break;
        }
    }

    //Finds the path
    void FindPatrolPath(Vector3 startingPoint, Vector3 destinationCoords)
    {
        owner.GetComponent<PathfindingAgent>().FindPath(pointA, pointB);
    }
}
