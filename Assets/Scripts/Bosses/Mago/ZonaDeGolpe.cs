using UnityEngine;

namespace Assets.Scripts.Bosses.Mago
{
    public class ZonaDeGolpe : MonoBehaviour
    {
        private Mago mago;

        private void Start()
        {
            mago = GetComponentInParent<Mago>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemigo"))
            {
                mago.Atacar(other.gameObject);
            }
        }
    }
}