using Assets.Scripts.Bosses.Mago;
using UnityEngine;
using Assets.Scripts.Bosses;




    public class BossHealth : MonoBehaviour
    {
        public int maxHealth = 100;
        private int currentHealth;
        [SerializeField]private GameObject mago;
        [SerializeField]private Cientifico cientifico;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            //animacion de los bosses
            print("El boss recibio daño");
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Animator animago = mago.transform.Find("Skin").GetComponent<Animator>();
            mago.GetComponent<Collider2D>().enabled = false;
            animago.SetTrigger("Died");
            mago.GetComponent<Mago>().enabled = false;
            cientifico.isStarted = false;
            // Implementar lógica de muerte aquí
            Debug.Log("Boss muerto");
        }
    }
