using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tom
{
    public class Health : MonoBehaviour
    {
        public float currentHealth;
        public float maxHealth;

        public event Action<GameObject> DeathEvent;
        public event Action<GameObject> DamageChangeEvent;

        public void Awake()
        {
            currentHealth = maxHealth;
        }

        public void ChangeHealth(float amount, GameObject perp)
        {
            currentHealth += amount;
            
            DamageChangeEvent?.Invoke(perp);

            if (currentHealth <= 0)
            {
                DeathEvent?.Invoke(gameObject);
            }
        }
    }
}