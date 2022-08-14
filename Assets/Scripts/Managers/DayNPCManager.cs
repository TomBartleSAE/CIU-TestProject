using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using Tom;
using UnityEngine;

public class DayNPCManager : ManagerBase<DayNPCManager>
{
	public List<GameObject> Ghouls = new List<GameObject>();
    public List<GameObject> Soldiers = new List<GameObject>();

    public event Action GhoulDeathEvent;
    public event Action SoldierDeathEvent;
    public event Action GameOverDayEvent;

    public int[] ghoulDamageLevels = new int[4];
    public float[] ghoulHealthLevels = new float[4];
    public float[] ghoulAttackRateLevels = new float[4];
    public float[] ghoulMovementSpeedLevels = new float[4];

    public void AddToSoldierList(GameObject newSoldier)
    {
        Soldiers.Add(newSoldier);
        //TODO not sure how to unsub when is a local variable?
        newSoldier.GetComponent<Health>().DeathEvent += RemoveFromSoldierList;
        
        foreach (var ghoul in Ghouls)
        {
	        ghoul.GetComponent<GhoulModel>().targetAlive = true;
        }
    }
    
    public void RemoveFromSoldierList(GameObject soldier)
    {
        SoldierDeathEvent?.Invoke();
        Soldiers.Remove(soldier);
        
        if (Soldiers.Count == 0)
        {
	        foreach (var ghoul in Ghouls)
	        {
		        ghoul.GetComponent<GhoulModel>().targetAlive = false;
	        }
        }
    }

    public void AddToGhoulList(GameObject ghoul)
    {
        ghoul.GetComponent<GhoulModel>().SetLevel(PlayerManager.Instance.CastleLevel);
        Ghouls.Add(ghoul);
        PlayerManager.Instance.CurrentGhouls = Ghouls.Count;
    }

    public void RemoveFromGhoulList(GameObject ghoul)
    {
        Ghouls.Remove(ghoul);
        PlayerManager.Instance.CurrentGhouls = Ghouls.Count;
    }

    public void GhoulDeath()
    {
        GhoulDeathEvent?.Invoke();
    }

    public void GameOverDayEventFired()
    {
        GameOverDayEvent?.Invoke();
    }
}
