using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class AttackCloudController : MonoBehaviour
    {
        public int damage = 5;
        private bool makeDamage = true;

        private void DestroyCloud()
        {
            Destroy(this.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && makeDamage)
            {
                print("Daño");
                collision.gameObject.GetComponent<PlayerControler>().TakeDamage(damage);
                StartCoroutine(ColdDownAttack());
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerEnter2D(other);
        }

        IEnumerator ColdDownAttack()
        {
            makeDamage = false;
            yield return new WaitForSeconds(0.5f);
            makeDamage = true;
        }
    }
}