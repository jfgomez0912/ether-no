using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class AttackState : MonoBehaviour
    {
        
        public GameObject bubblePrefab;
        public Transform spawnPointBubble;
        public float coldDownAtkTime;
        
        private bool _coldDownAtk;
        private GameObject _bubbleReference;
        private Rigidbody2D _bubbleRigidbody;
        
        //Controller values
        private Animator _animator;
        public bool IsFacingRight { get; set; }

        private void Update()
        {
            if (!_coldDownAtk)
            {
                Attack();
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
            yield return new WaitForSeconds(0.8f);
            _animator.SetBool("Atk", false);
            yield return new WaitForSeconds(coldDownAtkTime);
            _coldDownAtk = false;
        }

        void ThrowBubble()
        {
            Vector2 direction = new Vector2(3.5f, 1.5f);
            if (IsFacingRight == false)
            {
                direction.x *= -1;
            }
            _bubbleReference = Instantiate(bubblePrefab, spawnPointBubble);
            _bubbleRigidbody = _bubbleReference.GetComponent<Rigidbody2D>();
            _bubbleRigidbody.AddForce(direction, ForceMode2D.Impulse);
        }

        void ThrowCloud()
        {
            _bubbleRigidbody.linearVelocity = Vector2.zero;
            _bubbleRigidbody.gravityScale = 0;
        }
        
        public Animator Animator
        {
            set => _animator = value;
        }
    }
}