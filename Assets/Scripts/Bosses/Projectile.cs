using UnityEngine;

namespace Assets.Scripts.Bosses
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        public float speed = 5.0f;
        public int damage = 10;
        public float lifetime = 3.0f; // Tiempo de vida del proyectil

        private Rigidbody2D rb;

        private void Awake()
        {
            // Obtener el componente Rigidbody2D
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0; // Desactivar la gravedad para el proyectil
        }

        private void Start()
        {
            // Destruir el proyectil después de un tiempo de vida
            Destroy(gameObject, lifetime);
        }

        private void FixedUpdate()
        {
            // Mover el proyectil hacia adelante
         rb.linearVelocity = -transform.right * speed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // Implementar lógica de daño al jugador aquí
                PlayerControler playerHealth = collision.gameObject.GetComponent<PlayerControler>();
                if (playerHealth != null)
                {
                    // playerHealth.TakeDamage(damage);
                }

                // Destruir el proyectil al colisionar con el jugador
                Destroy(gameObject);
            }
            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                // Destruir el proyectil al colisionar con un obstáculo
                Destroy(gameObject);
            }
        }
    }
}
