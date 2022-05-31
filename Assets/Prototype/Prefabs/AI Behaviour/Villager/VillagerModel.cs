using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Tanks;
using Tom;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class VillagerModel : MonoBehaviour, IStunnable
{
    public Health health;

    public RaycastHit hit;

    public float rayDistance = 20;
    public float moveSpeed;

    public float viewRange;
    
    public bool isScared;
    public bool isStunned;
    public bool isEaten;
    
    private void Awake()
    {
        health = GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        NPCManager.Instance.Villagers.Add(gameObject);

        foreach (var villager in NPCManager.Instance.Villagers)
        {
            villager.GetComponent<Health>().DeathEvent += DeathCheck;
        }
    }
    
    //not sure if entirely necessary, but checks to see if IT died or if something else did
    void DeathCheck(GameObject deadThing)
    {
        if (deadThing == gameObject)
        {
            Die(deadThing);
        }
        else if (deadThing != gameObject)
        {
            Reaction(deadThing);
        }
    }
    
    //reacting to own death
    public void Die(GameObject me)
    {
        NPCManager.Instance.Villagers.Remove(gameObject);
        Destroy(gameObject);
    }

    //Reacting to other thing's death
    public void Reaction(GameObject deadThing)
    {
        Vector3 targetDirection = transform.position - deadThing.transform.position;
        
        if (Physics.Raycast( new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), targetDirection, out hit, viewRange))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), targetDirection);
            isScared = true;
        }
    }

    public void GetStunned()
    {
        isStunned = true;
    }


}
