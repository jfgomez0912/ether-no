using System.Collections;
using UnityEngine;
using Assets.Scripts.Enemies;

public class PlayerControler : MonoBehaviour
{
    public static PlayerControler Instance { get; private set; }

    public int health = 100;
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 input;
    private Animator animator;
    private Transform attackZone;
    private bool isHurt = false;


    void Awake()
    {   
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
     
        if (health <= 0)
        {
            animator.SetBool("Death", true);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            attackZone.gameObject.SetActive(false);
            rb.linearVelocity = Vector2.zero;
            print("Ha muerto");
            this.enabled = false;
        }
        //  Ataque con mouse del manager de ataque
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            Attack();
        } else
        {
            //Temporal hasta que se implementa la corrutina
            attackZone.gameObject.SetActive(false);
            animator.SetBool("Attack", false);
        }

        ProcessInputs();
        Move();
        Animate();
    }


    public void WrapRespawn(float waitTime)
    {
        StartCoroutine(Respawn(waitTime));
    }
    IEnumerator Respawn(float waitTime)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().enabled = true;
    }
    void ProcessInputs()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize(); // Prevents diagonal movement from being faster

        if (input.x > 0)
        {
            attackZone.localPosition = new Vector2(0.9f, 1f);
            attackZone.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (input.x < 0)
        {
            attackZone.localPosition = new Vector2(-0.9f, 1f);
            attackZone.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else if (input.y > 0)
        {
            attackZone.localPosition = new Vector2(0, 1.80f);
            attackZone.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (input.y < 0)
        {
            attackZone.localPosition = new Vector2(0, 0.11f);
            attackZone.localRotation = Quaternion.Euler(0, 0, 270);
        }
    }
    void Move()
    {
        rb.linearVelocity = input * speed;
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > 100) // Suponiendo que 100 es la salud máxima
        {
            health = 100;
        }
        print("Salud: " + health);
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

    void Attack(){
        attackZone.gameObject.SetActive(true);
        // Implemementar con una corrutina para que el ataque dure un tiempo determinado
        animator.SetBool("Attack", true);
    }

    public void TakeDamage(int damage)
    {
        if (!isHurt)
        {
            isHurt = true;
            health -= damage;
            animator.SetBool("Hurt", true);
            print(health + ", Daño:" +damage);
            StartCoroutine(ColdDownHurt());
        }
    }

    IEnumerator ColdDownHurt()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("Hurt", false);
        isHurt = false;
    }
}
