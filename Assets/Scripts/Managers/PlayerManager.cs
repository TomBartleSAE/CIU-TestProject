using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ManagerBase<PlayerManager>
{
    public SaveData saveData = new SaveData();

    [Header("Stats")]
    public int soldiersKilled;
    public int villagersDrained;
    public int ghoulsCreated;

    [Header("Blood")] 
    public int currentBlood;
    public int maxBlood = 50;
    
    [Header("Castle")]
    [SerializeField] private int castleLevel = 1;

    public int CastleLevel
    {
        get => castleLevel;
        set
        {
            castleLevel = value;
            CastleLevelChangedEvent?.Invoke();
        }
    }
    public float castleHealth;

    [Header("Ghouls")] 
    [SerializeField] private int currentGhouls;
    [SerializeField] private int ghoulPopcap = 5;
    
    public int CurrentGhouls
    {
        get
        {
            return currentGhouls;
        }

        set
        {
            if (value > ghoulPopcap)
            {
                value = ghoulPopcap;
            }
            
            currentGhouls = value;
            CurrentGhoulsChangedEvent?.Invoke(value);
        }
    }

    public int GhoulPopcap
    {
        get
        {
            return ghoulPopcap;
        }

        set
        {
            ghoulPopcap = value;
            MaxGhoulsChangedEvent?.Invoke(value);
        }
    }
    
    public int[,] towerLayout = new int[11, 11];
    
    /// <summary>
    /// Sends out how much the blood has changed by
    /// </summary>
    public event Action<int> BloodChangedEvent;

    /// <summary>
    /// Sends out how much the max blood has changed by
    /// </summary>
    public event Action<int> MaxBloodChangedEvent;

    public event Action<int> CurrentGhoulsChangedEvent;
    public event Action<int> MaxGhoulsChangedEvent;
    public event Action CastleLevelChangedEvent;

    private void Start()
    {
        DayNPCManager.Instance.SoldierDeathEvent += AddSoldierKilled;
        NightNPCManager.Instance.VillagerDeathEvent += AddVillagerDrained;
        NightNPCManager.Instance.GuardDeathEvent += AddGhoulCreated;
    }

    public void ChangeBlood(int amount)
    {
        if (currentBlood + amount > maxBlood)
        {
            amount = maxBlood - currentBlood;
        }

        currentBlood += amount;

        BloodChangedEvent?.Invoke(amount);
    }

    public void ChangeMaxBlood(int amount)
    {
        maxBlood += amount;

        Mathf.Clamp(currentBlood, 0, maxBlood);

        MaxBloodChangedEvent?.Invoke(amount);
    }

    public void AddSoldierKilled()
    {
        soldiersKilled++;
    }

    public void AddVillagerDrained(GameObject a)
    {
        villagersDrained++;
    }

    public void AddGhoulCreated(GameObject g)
    {
        ghoulsCreated++;
    }

    public void ResetStats()
    {
        soldiersKilled = 0;
        villagersDrained = 0;
        ghoulsCreated = 0;
    }

    public void SetSaveData()
    {
        saveData.day = GameManager.Instance.currentDay;

        saveData.soldiersKilled = soldiersKilled;
        saveData.villagersDrained = villagersDrained;
        saveData.ghoulsCreated = ghoulsCreated;
        
        saveData.currentBlood = currentBlood;
        saveData.maxBlood = maxBlood;

        saveData.castleLevel = castleLevel;
        saveData.castleHealth = castleHealth;

        saveData.ghoulCount = currentGhouls;
        saveData.maxGhouls = ghoulPopcap;

        saveData.towerLayout = towerLayout;
    }

    public void LoadSaveData(SaveData loadedData)
    {
        GameManager.Instance.currentDay = loadedData.day;

        soldiersKilled = loadedData.soldiersKilled;
        villagersDrained = loadedData.villagersDrained;
        ghoulsCreated = loadedData.ghoulsCreated;
        
        currentBlood = loadedData.currentBlood;
        maxBlood = loadedData.maxBlood;

        castleLevel = loadedData.castleLevel;
        castleHealth = loadedData.castleHealth;

        currentGhouls = loadedData.ghoulsCreated;
        ghoulPopcap = loadedData.maxGhouls;

        towerLayout = loadedData.towerLayout;
    }
}