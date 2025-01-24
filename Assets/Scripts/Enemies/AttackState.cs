using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform spawnPointBubble;
    public float coldDownAtkTime;
    
    private Transform _focus;
    private Vector2 _movement;
    private bool _coldDownAtk;
    private GameObject _bubbleReference;
    private Rigidbody2D _bubbleRigidbody;

    //Controller values
    private Animator _animator;
    private float _speed;
    private Rigidbody2D _rigidbodyEnemy;
    public bool IsFacingRight { get; set; }

    private void Update()
    {
        if (!_coldDownAtk)
        {
            //perseguir
            Vector2 target = CalculateTargetPosition();
            MoveTo(target);
            FlipSprite();
            AnimateMove();
            //atacar
            if (Vector2.Distance(target, transform.position) < 0.1f)
            {
                Attack();
            }    
        }
        
    }

    void FlipSprite()
    {
        bool condition1 = IsFacingRight && (_focus.position.x < transform.position.x);
        bool condition2 = !IsFacingRight && (_focus.position.x > transform.position.x);
        if (condition1 || condition2)
        {
            IsFacingRight = !IsFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }

    Vector2 CalculateTargetPosition()
    {
        float offset = IsFacingRight ? -3.5f : 3.5f;
        return new Vector2(_focus.position.x + offset, _focus.position.y);
    }

    void MoveTo(Vector2 target)
    {
        Vector2 currentPosition = transform.position;
        Vector2 direction = (target - currentPosition).normalized;
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
    private void Attack()
    {
        _coldDownAtk = true;
        _animator.SetFloat("Speed", 0f);
        _animator.SetBool("Atk", true);
        StartCoroutine(ColdDownStart());
    }

    private IEnumerator ColdDownStart()
    {
        yield return new WaitForSeconds(0.6f);
        _animator.SetBool("Atk", false);
        yield return new WaitForSeconds(coldDownAtkTime);
        _coldDownAtk = false;
    }

    //Es usado desde el animator como un Event
    IEnumerator ThrowBubbleCoroutine()
    {
        ThrowBubble();
        yield return new WaitForSeconds(0.7f);
        PopBubble();
    }

    private void ThrowBubble()
    {
        Vector2 direction = new Vector2(3.5f, 1.5f);
        if (!IsFacingRight)
        {
            direction.x *= -1;
        }
        _bubbleReference = Instantiate(bubblePrefab, spawnPointBubble);
        _bubbleReference.transform.SetParent(GameObject.Find("EnemyAttacks").transform);
        _bubbleRigidbody = _bubbleReference.GetComponent<Rigidbody2D>();
        _bubbleRigidbody.AddForce(direction, ForceMode2D.Impulse);
    }

    private void PopBubble()
    {
        _bubbleRigidbody.linearVelocity = Vector2.zero;
        _bubbleRigidbody.gravityScale = 0;
    }
    
    //gets and sets
    public Animator Animator
    {
        set => _animator = value;
    }

    public Transform Focus
    {
        set => _focus = value;
    }
    public float Speed
    {
        set => _speed = value;
    }

    public Rigidbody2D RigidbodyEnemy
    {
        set => _rigidbodyEnemy = value;
    }
}