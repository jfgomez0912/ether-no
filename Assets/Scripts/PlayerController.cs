using UnityEngine;
using Assets.Scripts.Enemies;

public class PlayerControler : MonoBehaviour
{
    public int health = 100;
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 input;
    private Animator animator;
    private Transform attackZone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackZone = transform.Find("AttackZone");
        attackZone.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //  Ataque con mouse del manager de ataque
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            Attack();
        }else
        {

            //Temporal hasta que se implementa la corrutina
            attackZone.gameObject.SetActive(false);
        }

        ProcessInputs();
        Move();
        Animate();
    }

    void ProcessInputs()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize(); // Prevents diagonal movement from being faster
    }

    void Move()
    {
        rb.linearVelocity = input * speed;
    }

    void Animate()
    {
        if (input != Vector2.zero)
        {
            animator.SetFloat("Horizontal", input.x);
            animator.SetFloat("Vertical", input.y);
        }
        animator.SetFloat("Speed", input.sqrMagnitude);
    }

    void Attack()
    {
        attackZone.gameObject.SetActive(true);
        // Implemementar con una corrutina para que el ataque dure un tiempo determinado
    }

    public void TakeDamage(int damage)
    {
        
        print(health);
        health -= damage;

        if (health <= 0)
        {
            print("Ha muerto");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Entre");
        if (collision.gameObject.CompareTag("EnemyAttack"))
        {
            TakeDamage(collision.gameObject.GetComponent<AttackCloudController>().damage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }
}
