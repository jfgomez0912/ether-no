using UnityEngine;
using Random = UnityEngine.Random;

public class PatrolState : MonoBehaviour
    {
        private Transform pointsParent;
        
        private Vector2 _movement;
        private Transform _focus;
        private int _pointsCount;
        private int _indexPoint;

        //Controller values
        private float _speed;
        private Animator _animator;
        private Rigidbody2D _rigidbodyEnemy;
        public bool IsFacingRight { get; set; }

        private void Awake()
        {
            //Puntos para patrullar
            pointsParent = GameObject.Find("PatrolPoints").transform;
            _pointsCount = pointsParent.childCount;
            _indexPoint = Random.Range(0, _pointsCount);
            _focus = pointsParent.GetChild(_indexPoint);
        }

        // Update is called once per frame
        void Update()
        {
            MoveTo(_focus);
            FlipSprite();
            AnimateMove();
            if (Vector2.Distance(_focus.position, transform.position) < 0.1f)
            {
                MoveToNextPatrolPoint();
            }
        }

        void FlipSprite()
        {
            bool condition1 = IsFacingRight && (_movement.x < 0f);
            bool condition2 = !IsFacingRight && (_movement.x > 0f);
            if (condition1 || condition2)
            {
                IsFacingRight = !IsFacingRight;
                Vector3 scale = transform.localScale;
                scale.x *= -1f;
                transform.localScale = scale;
            }
        }

        void MoveTo(Transform target)
        {
            Vector2 targetPosition = target.position;
            Vector2 currentPosition = transform.position;
            Vector2 direction = targetPosition - currentPosition;
            direction.Normalize();
            _movement = direction;
            _rigidbodyEnemy.MovePosition(currentPosition + direction * (_speed * Time.deltaTime));
        }

        void AnimateMove()
        {
            if (_movement != Vector2.zero)
            {
                _animator.SetFloat("Speed", _movement.sqrMagnitude);
            }
        }

        void MoveToNextPatrolPoint()
        {
            int auxPoint;
            do
            {
                auxPoint = Random.Range(0, _pointsCount);
            } while (_indexPoint == auxPoint);
            _indexPoint = auxPoint;
            _focus = pointsParent.GetChild(_indexPoint);
        }

        

        public float Speed
        {
            set => _speed = value;
        }

        public Animator Animator
        {
            set => _animator = value;
        }

        public Rigidbody2D RigidbodyEnemy
        {
            set => _rigidbodyEnemy = value;
        }
    }