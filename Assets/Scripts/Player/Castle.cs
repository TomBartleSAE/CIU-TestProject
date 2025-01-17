using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tom;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Castle : MonoBehaviour
{
    public Health health;
    
    [Tooltip("Use elements 1, 2 and 3 for upgrades to level 2, 3 and 4, leave element 0 at 0")]
    public int[] upgradeCosts = new int[4];
    public int[] maxHealths = new int[4];
    public int[] maxBloods = new int[4];
    public int[] ghoulPopcaps = new int[4];
    public GameObject[] meshes = new GameObject[4];

    private bool destroyed;

    public event Action CastleUpgradedEvent;

    //public TextMeshProUGUI castleLevelText;

    private void OnEnable()
    {
        health.DeathEvent += DestroyCastle;
        health.DamageChangeEvent += UpdateCastleHealth;
    }

    private void OnDisable()
    {
        health.DeathEvent -= DestroyCastle;
        health.DamageChangeEvent -= UpdateCastleHealth;
    }

    private void Start()
    {
        // Need to wait for PlayerManager singleton to be assigned in Awake
        SetupCastle();
        UpdateCastleHealth(gameObject);
    }

    public void DestroyCastle(GameObject castle)
    {
        if (!destroyed)
        {
            DayPhaseState dayPhase = GameManager.Instance.dayPhaseState as DayPhaseState;
            dayPhase.CastleDestroyed();
            destroyed = true;
        }
    }

    public void UpdateCastleHealth(GameObject a)
    {
        // HACK: Not ideal having 2 variables that point to the same value, potential for mismatch
        PlayerManager.Instance.castleHealth = health.currentHealth;
    }

    public void Upgrade()
    {
        int level = PlayerManager.Instance.CastleLevel;

        if (level < 4)
        {
            if (PlayerManager.Instance.currentBlood >= upgradeCosts[level])
            {
                PlayerManager.Instance.ChangeBlood(-upgradeCosts[level]);
                SetCastleLevel(level);
                PlayerManager.Instance.CastleLevel++;

                foreach (GameObject ghoul in DayNPCManager.Instance.Ghouls)
                {
                    ghoul.GetComponent<GhoulModel>().SetLevel(level);
                }
                
                SetupCastle();
                
                CastleUpgradedEvent?.Invoke();
            }
        }
    }

    public void SetCastleLevel(int level)
    {
        PlayerManager.Instance.GhoulPopcap = ghoulPopcaps[level];
        // Should probably make Max Blood a property and just set the value rather than use this function
        PlayerManager.Instance.ChangeMaxBlood(maxBloods[level] - PlayerManager.Instance.maxBlood);
        health.ChangeHealth(health.MaxHealth - health.currentHealth, gameObject);
    }

    public void SetupCastle()
    {
        int level = PlayerManager.Instance.CastleLevel - 1;
        health.MaxHealth = maxHealths[level];

        foreach (GameObject mesh in meshes)
        {
            mesh.SetActive(false);
        }
        
        meshes[level].SetActive(true);

        //castleLevelText.text = "Lv. " + PlayerManager.Instance.CastleLevel; // HACK
    }
}
