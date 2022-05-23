using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    public class HealthManager : MonoBehaviour
    {
        #region Variables

        public BaseStatsSO healthStats;
        public float currentHealth;
        public bool isDead;

        #endregion

        #region Unity Built-In Methods

        private void Awake()
        {
            _Init();
        }
        #endregion

        #region Custom Methods

        private void _Init()
        {
            isDead = false;
            currentHealth = healthStats.maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, healthStats.maxHealth);
            SovereignUtils.Log($"Damage: {damage}, CurrentHealth {currentHealth}");
            if (currentHealth == 0)
                isDead = true;
        }

        public void Heal(float health)
        {
            currentHealth = Mathf.Clamp(currentHealth + health, 0, healthStats.maxHealth);

        }

        public void Kill(System.Action OnHealthIsZero)
        {
            SovereignUtils.Log("HealthManager Kill");
            OnHealthIsZero?.Invoke();
        }

        #endregion
    }
}