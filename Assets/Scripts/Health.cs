using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FOGPISystems.Helper
{
    [System.Serializable]
    public class HealthChangedEvent : UnityEvent<HealthChangedObject> { }
    [System.Serializable]
    public class HealedEvent : UnityEvent<int> { }
    public class Health : MonoBehaviour
    {
        public HealthChangedEvent HealthChanged;
        public HealedEvent Healed;
        public UnityEvent Hurt;
        public UnityEvent OutofHealth;

        public int MaxHealth = 100;
        public int CurrentHealth = 0;

        private void Start()
        {
            if (CurrentHealth == 0)
                CurrentHealth = MaxHealth;

            if (HealthChanged == null)
                HealthChanged = new HealthChangedEvent();

            if (Healed == null)
                Healed = new HealedEvent();

            if (Hurt == null)
                Hurt = new UnityEvent();

            if (OutofHealth == null)
                OutofHealth = new UnityEvent();

            HealthChanged.Invoke(new HealthChangedObject { 
                maxHealth = MaxHealth, 
                currentHealth = CurrentHealth, 
                delta = MaxHealth
            });

        }

        public void Damage(int damage)
        {
            if (CurrentHealth <= 0)
                return;

            CurrentHealth -= damage;

            if(CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                OutofHealth.Invoke();
            }
            else
            {
                Hurt.Invoke();
            }

            HealthChanged.Invoke(new HealthChangedObject
            {
                maxHealth = MaxHealth,
                currentHealth = CurrentHealth,
                delta = -damage
            });
        }

        public void Kill()
        {
            Damage(CurrentHealth);
        }

        public void Heal(int? amount = null)
        {
            if (CurrentHealth <= 0)
                return;

            if (amount == null)
            {
                amount = MaxHealth - CurrentHealth;
            }

            CurrentHealth += (int)amount;

            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
            

            Healed.Invoke((int)amount);
            HealthChanged.Invoke(new HealthChangedObject
            {
                maxHealth = MaxHealth,
                currentHealth = CurrentHealth,
                delta = (int)amount
            });

        }
    }

    public struct HealthChangedObject
    {
        public int maxHealth;
        public int currentHealth;
        public int delta;
    }

}