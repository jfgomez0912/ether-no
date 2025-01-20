using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class PatrolMovement : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Vector2 _movement;
        private Animator _animator;
        private Transform _focus;
        private int _pointsCount;
        private int _indexPoint;
        private EnemyController _enemyController;
        public Transform pointsParent;
        public bool isFacingRigth = true;

        private void Awake()
        {
            _enemyController = GetComponent<EnemyController>();
            _pointsCount = pointsParent.childCount;
            _indexPoint = Random.Range(0, _pointsCount);
            _focus = pointsParent.GetChild(_indexPoint);
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_enemyController.CurrentState != EnemyController.States.Attack)
            {
                MoveTo(_focus);
                FlipSprite();
                AnimateMove();
                if (Vector2.Distance(_focus.position, transform.position) < 0.1f)
                {
                    MoveToNextPatrolPoint();
                }
            }
            else
            {
                _animator.SetFloat("Speed", 0f);
            }
        }

        void FlipSprite()
        {
            bool condition1 = isFacingRigth && (_movement.x < 0f);
            bool condition2 = !isFacingRigth && (_movement.x > 0f);
            if (condition1 || condition2)
            {
                isFacingRigth = !isFacingRigth;
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
            _rb.MovePosition(currentPosition + (direction * (_enemyController.speed * Time.deltaTime)));
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
    }
}