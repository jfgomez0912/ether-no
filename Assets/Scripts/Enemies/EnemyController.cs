using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public bool atk;
        private string[] _states = { "Patrol", "Follow", "Attack", "Wounded", "Died" };
        private bool _coldDownAtk = false;
        private Animator _anim;
        private Transform _sprite;
        private CircleCollider2D _atkTrigger;

        [Header("Enemy")] public string enemyName;

        [Header("Stats")] public int health;
        public int damage;
        public bool isAlive = true;
        public float speed = 5.0f;
        public float coldDownAtkTime;

        [Header("AtackObjects")] public GameObject bubble;
        public Transform spawnPointBubble;
        private GameObject bubbleRef;
        private Rigidbody2D _rb;
        private PatrolMovement _pm;
        private Vector3 orignalPosBubble;
        private bool isFacingRigth = true;

        private void Awake()
        {
            orignalPosBubble = spawnPointBubble.transform.localPosition;
            _pm = GetComponent<PatrolMovement>();
            _atkTrigger = GetComponent<CircleCollider2D>();
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            isFacingRigth = _pm.isFacingRigth;
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

        void ThrowBubble()
        {
            Vector2 direction = new Vector2(3.5f, 1.5f);
            if (isFacingRigth == false)
            {
                direction.x *= -1;
            }

            bubbleRef = Instantiate(bubble, spawnPointBubble);
            _rb = bubbleRef.GetComponent<Rigidbody2D>();
            _rb.AddForce(direction, ForceMode2D.Impulse);
        }

        void ThrowCloud()
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.gravityScale = 0;
        }

        void OnTriggerStay2D(Collider2D other)
        {
            /*print(other.gameObject.name);*/
        }
    }
}