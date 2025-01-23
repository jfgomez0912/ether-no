using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class AttackCloudController : MonoBehaviour
    {
        public int damage = 5;
        private void DestroyCloud()
        {
            Destroy(this.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerControler>().TakeDamage(damage);
            }
        }

    }
}

