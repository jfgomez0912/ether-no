using UnityEngine;

namespace Assets.Scripts.Bosses {

    public class Cientifico : MonoBehaviour
{

        [Header("Cientifico Settings")]
        public float moveSpeed = 2.0f;
    public float fireRate = 2.0f;
    public GameObject projectilePrefab;
    private Transform firePoint;
    private Animator animator;
    private float nextFireTime = 0f;
    private Vector3 startPosition;
    public bool isStarted = false;
        [Header("Harmonic Movement Settings")]
        public float amplitude = 2.0f; // Amplitud del movimiento armónico
        public float frequency = 1.0f; // Frecuencia del movimiento armónico

        private void Awake()
        {
            // Obtener el componente Transform del objeto hijo llamado "firePoint"
            firePoint = transform.Find("firePoint");
            if (firePoint == null)
            {
                Debug.LogError("firePoint no encontrado en el objeto hijo 'firePoint'");
            }

            // Obtener el componente Animator del objeto hijo llamado "skin"
            animator = GetComponentInChildren<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator no encontrado en el objeto hijo 'skin'");
            }

            // Guardar la posición inicial
            startPosition = transform.position;

        }

        private void Update()
    {
        if (isStarted)
            {
                Move();
                Fire();
            }
    }

    private void Move()
    {
            // Movimiento armónico en el eje Y
            float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

        private void Fire()
        {
            if (Time.time > nextFireTime)
            {
                // Iniciar la animación de ataque
                animator.SetBool("IsAttacking", true);

                // Instanciar el proyectil después de un pequeño retraso para sincronizar con la animación
                Invoke(nameof(SpawnProjectile), 0.4f); // Ajusta el retraso según la duración de la animación

                nextFireTime = Time.time + fireRate;
            }
        }

        private void SpawnProjectile()
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            // Volver al estado Idle después de disparar
            animator.SetBool("IsAttacking", false);
        }
    }
}