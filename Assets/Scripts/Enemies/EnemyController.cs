using System;
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
        private CircleCollider2D _atkTrigger;

        [Header("Stats")] public int health;
        public int damage;
        public bool isAlive = true;
        public float speed = 5.0f;
        public bool followPlayer;
        private PatrolState _patrolState;
        private AttackState _attackState;
        
        private bool _isFacingRight = true;
        private Animator _animator;
        private Rigidbody2D _enemyRigidbody;
        private MonoBehaviour _activeState;

        private void Awake()
        {
            newState = States.Patrol;
            _currentState = States.Patrol;
            _patrolState = GetComponent<PatrolState>();
            _attackState = GetComponent<AttackState>();
            
            _atkTrigger = GetComponent<CircleCollider2D>();
            _animator = GetComponent<Animator>();
            _enemyRigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            InitialBehaviorParams();
            ChangeState(States.Patrol);
        }

        private void Update()
        {
            if (newState != _currentState)
            {
                ChangeState(newState);
            }
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                _attackState.Focus = other.gameObject.transform;
                _attackState.RigidbodyEnemy = _enemyRigidbody;
                _attackState.Speed = speed;
                newState = States.Attack;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                newState = States.Patrol;
            }
        }

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
                default:
                    _currentState = States.Patrol;
                    AttackStateEnd();
                    PatrolStateStart();
                    break;
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
    }