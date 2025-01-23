using UnityEngine;

namespace Assets.Scripts.Bosses
{
    public class BossHealth : MonoBehaviour
    {
        public int maxHealth = 100;
        private int currentHealth;


        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // Implementar lógica de muerte aquí
            Debug.Log("Boss muerto");
        }
    }
}