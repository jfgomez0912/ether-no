using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    public bool atk;

    private string[] _states = { "Idle", "Patrol", "Follow", "Attack", "Wounded", "Died" };
    [SerializeField]
    private string _currentState;
    private bool _coldDownAtk = false;
    private Animator _anim;
    private Transform _sprite;
    private CircleCollider2D _atkTrigger;
    private PolygonCollider2D _followTrigger;

    [Header("Enemy")] public string enemyName;

    [Header("Stats")] public int health;
    public int damage;
    public bool isAlive = true;
    public float speed = 5.0f;
    public float coldDownAtkTime;

    private void Awake()
    {
        _currentState = _states[0];
        _followTrigger = GetComponent<PolygonCollider2D>();
        _atkTrigger = GetComponent<CircleCollider2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (atk && !_coldDownAtk)
        {
            Attack();
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
    
    void OnTriggerStay2D(Collider2D other)
    {
        /*print(other.gameObject.name);*/
    }
}