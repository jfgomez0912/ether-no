using UnityEngine;

namespace Assets.Scripts.Bosses
{
    public class BossHealth : MonoBehaviour
    {
        public int maxHealth = 100;
        private int currentHealth;
        [SerializeField]private Animator mago;
        [SerializeField]private Animator cientifico;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            //animacion de los bosses
            print("El boss recibio da�o");
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // Implementar l�gica de muerte aqu�
            Debug.Log("Boss muerto");
        }
    }
}