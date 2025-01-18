using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMovementController : MonoBehaviour
{
    public bool atk;

    private string[] _states = { "Idle", "Patrol", "Follow", "Attack", "Wounded", "Died" };
    [SerializeField]
    private string _currentState;
    private bool _coldDownAtk = false;
    private Animator _anim;
    private Transform _sprite;
    private Rigidbody2D _rb;
    private bool isFacingRigth = true;

    [Header("Enemy")] public string enemyName;

    [Header("Stats")] public int health;
    public int damage;
    public bool isAlive = true;
    public float speed = 5.0f;
    public float coldDownAtkTime;
    private Vector2 input;
    

    private void Awake()
    {
        _currentState = _states[0];
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ProcessInputs();
        FlipSprite();
        Move();
        if (atk && !_coldDownAtk)
        {
            Attack();
        }
    }
    void Move()
    {
        _rb.linearVelocity = input * speed;
    }

    void FlipSprite()
    {
        if(isFacingRigth && (input.x > 0 || input.y > 0))
        {
            isFacingRigth = !isFacingRigth;
            transform.localScale *= -1f;
        }
    } 

    private void Attack()
    {
        _coldDownAtk = true;
        _anim.SetBool("atk", true);
        StartCoroutine(ColdDownStart());
    }

    private IEnumerator ColdDownStart()
    {
        yield return new WaitForSeconds(0.8f);
        _anim.SetBool("atk", false);
        yield return new WaitForSeconds(coldDownAtkTime);
        _coldDownAtk = false;
    }
    
    void ProcessInputs()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize(); // Prevents diagonal movement from being faster
    }
    void OnTriggerStay2D(Collider2D other)
    {
        /*print(other.gameObject.name);*/
    }
}