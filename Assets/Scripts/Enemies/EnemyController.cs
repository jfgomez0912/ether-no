using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
    {
        public enum States
        {
            Patrol,
            Attack,
            Wounded,
            Died
        }

        [SerializeField] private States newState;
        private States _currentState;

        [Header("Stats")] public int health = 50;
        public bool isAlive = true;
        public float speed = 5.0f;
        private PatrolState _patrolState;
        private AttackState _attackState;
        
        private bool _isFacingRight = true;
        private Animator _animator;
        private Rigidbody2D _enemyRigidbody;
        private MonoBehaviour _activeState;
        private CircleCollider2D _circleCollider2D;
        private BoxCollider2D _boxCollider2D;

        private void Awake()
        {
            newState = States.Patrol;
            _currentState = States.Patrol;
            _patrolState = GetComponent<PatrolState>();
            _attackState = GetComponent<AttackState>();
            
            _animator = GetComponent<Animator>();
            _enemyRigidbody = GetComponent<Rigidbody2D>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            InitialBehaviorParams();
            ChangeState(States.Patrol);
        }

        private void Update()
        {
            if (health <= 0)
            {
                newState = States.Died;
            }
            if (newState != _currentState)
            {
                ChangeState(newState);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerStay2D(other);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Player") && isAlive)
            {
                _attackState.Focus = other.gameObject.transform;
                _attackState.RigidbodyEnemy = _enemyRigidbody;
                _attackState.Speed = speed;
                newState = States.Attack;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Player") && isAlive)
            {
                newState = States.Patrol;
            }
        }

    //private void OnCollisionEnter2D(Collision2D other)
    //{

    //Temp
    //    if (other.gameObject.tag.Equals("Player") && isAlive)
    //  {
    //Tomar daño del daño del jugardor con el other getComponent
    //health -= 10;
    //newState = States.Wounded;
    //}
    //}

    private void ChangeState(States state)
        {
            switch (state)
            {
                case States.Patrol:
                    _currentState = States.Patrol;
                    AttackStateEnd();
                    PatrolStateStart();
                    break;
                case States.Attack:
                    _currentState = States.Attack;
                    PatrolStateEnd();
                    AttackStateStart();
                    break;
                case States.Wounded:
                    if (!_currentState.Equals(States.Died))
                    {
                        StartCoroutine(HurtCoroutune());
                    }
                    break;
                case States.Died:
                    _currentState = States.Died;
                    AttackStateEnd();
                    PatrolStateEnd();
                    _animator.SetBool("Death", true);
                    _circleCollider2D.enabled = false;
                    _boxCollider2D.enabled = false;
                    isAlive = false;
                    break;
                default:
                    print("Error: estado del enemigo indefinido");
                    break;
            }
        }

    public void TakeDamage(int damage)
    {
        if (isAlive)
        {
            health -= damage;
            newState = States.Wounded;
        }
    }
    private void InitialBehaviorParams()
        {
            _patrolState.IsFacingRight = _isFacingRight;
            _attackState.IsFacingRight = _isFacingRight;
        }

        private void PatrolStateStart()
        {
            _patrolState.IsFacingRight = _isFacingRight;
            _patrolState.Speed = speed;
            _patrolState.Animator = _animator;
            _patrolState.RigidbodyEnemy = _enemyRigidbody;
            _patrolState.enabled = true;
        }

        private void PatrolStateEnd()
        {
            _isFacingRight = _patrolState.IsFacingRight;
            _patrolState.enabled = false;
        }

        private void AttackStateStart()
        {
            _attackState.Animator = _animator;
            _attackState.IsFacingRight = _isFacingRight;
            _attackState.enabled = true;
        }

        private void AttackStateEnd()
        {
            _isFacingRight = _attackState.IsFacingRight;
            _attackState.enabled = false;
        }
        
        public IEnumerator HurtCoroutune()
        {
            _animator.SetBool("Hurt", true);
            yield return new WaitForSeconds(1f);
            _animator.SetBool("Hurt", false);
        }
    }