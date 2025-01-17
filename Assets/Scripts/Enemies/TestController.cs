using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Animator _animator;
    private Transform _focus;
    public Transform pointsParent;
    private int _pointsCount;
    private int _indexPoint;
    private EnemyController _enemyController;

    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
        _pointsCount = pointsParent.childCount;
        _indexPoint = Random.Range(0, _pointsCount);
        _focus = pointsParent.GetChild(_indexPoint);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_enemyController.atk)
        {
            Move(_focus);
            AnimateMove();
            if (Vector2.Distance(_focus.position, transform.position) < 0.1f)
            {
                MoveToNextPatrolPoint();
            }
        }
        else
        {
            _animator.SetFloat("Speed", 0f);
            _animator.SetFloat("Horizontal", 0f);// Termporal
        }
        
    }
    
    void Move(Transform target)
    {
        Vector2 targetPosition = target.position;
        Vector2 currentPosition = transform.position;
        Vector2 direction = targetPosition - currentPosition;
        direction.Normalize();
        _movement = direction;
        _rb.MovePosition(currentPosition + (direction * (speed * Time.deltaTime)));
    }

    void AnimateMove()
    {
        if (_movement != Vector2.zero)
        {
            _animator.SetFloat("Horizontal", _movement.x);
            _animator.SetFloat("Vertical", _movement.y);
        }

        _animator.SetFloat("Speed", _movement.sqrMagnitude);
    }
    void MoveToNextPatrolPoint()
    {
        /*_rb.linearVelocity = Vector2.zero;
        _movement = Vector2.zero;
        yield return new WaitForSeconds(2f);*/
        int auxPoint;
        do
        {
            auxPoint = Random.Range(0, _pointsCount);
        } while (_indexPoint == auxPoint);
        _indexPoint = auxPoint;
        _focus = pointsParent.GetChild(_indexPoint);
    }
}