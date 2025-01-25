using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Bosses.Mago
{
    public class Mago : MonoBehaviour
    {
        public float desorientarDuracion = 2.0f;
        public float empujarFuerza = 5.0f;
        public int empujarDanio = 10;
        public float rangoAtaque = 2.0f;
        public float distanciaMinima = 0.5f;
        public Transform objetivo;
        private Desorientar desorientar;
        private Empujar empujar;
        private Rigidbody2D rb;
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private bool atacando = false;
        private Transform attackZone;
        [SerializeField]private BossHealth bossHealth;

        private void Start()
        {
            objetivo = GameObject.Find("Player").transform;
            desorientar = new Desorientar(desorientarDuracion);
            empujar = new Empujar(empujarFuerza, empujarDanio);
            rb = GetComponent<Rigidbody2D>();
            animator = transform.Find("Skin").GetComponent<Animator>();
            spriteRenderer = transform.Find("Skin").GetComponent<SpriteRenderer>();
            attackZone = transform.Find("AttackZone");
        }

        private void Update()
        {
            if (objetivo != null && !atacando)
            {
                PerseguirYAtacar();
            }
        }

        private void PerseguirYAtacar()
        {
            float distancia = Vector2.Distance(transform.position, objetivo.position);
            if (distancia > rangoAtaque)
            {
                Vector2 direccion = (objetivo.position - transform.position).normalized;
                rb.linearVelocity = direccion * 2.0f; // Velocidad de movimiento
                animator.SetBool("isRunning", true);

                // Girar el sprite dependiendo de la dirección
                if (direccion.x < 0)
                {
                    spriteRenderer.flipX = true;
                    attackZone.localPosition = new Vector2(-1.5f, -0.5f);
                    attackZone.localRotation = Quaternion.Euler(0, 0, 180);
                }
                else if (direccion.x > 0)
                {
                    spriteRenderer.flipX = false;
                    attackZone.localPosition = new Vector2(1.5f, -0.5f);
                    attackZone.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }
            else if (distancia > distanciaMinima)
            {
                rb.linearVelocity = Vector2.zero;
                animator.SetBool("isRunning", false);
                Atacar(objetivo.gameObject);
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
                animator.SetBool("isRunning", false);
            }
        }

        public void Atacar(GameObject enemigo)
        {
            atacando = true;
            animator.SetBool("isAttacking", true);
            desorientar.Aplicar(enemigo);
            empujar.Aplicar(enemigo, transform.position);
            StartCoroutine(EsperarYPerseguir());
        }

        private IEnumerator EsperarYPerseguir()
        {
            // Esperar hasta que la animación de ataque termine
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            atacando = false;
            animator.SetBool("isAttacking", false);
        }

        public void TakeDamage(int damage)
        {
            bossHealth.TakeDamage(damage);
        }
    }
}