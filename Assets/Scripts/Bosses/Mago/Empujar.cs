using UnityEngine;

namespace Assets.Scripts.Bosses.Mago
{
    public class Empujar
    {
        private float fuerza;
        private int danio;

        public Empujar(float fuerza, int danio)
        {
            this.fuerza = fuerza;
            this.danio = danio;
        }

        public void Aplicar(GameObject enemigo, Vector3 origen)
        {
            Rigidbody2D enemigoRb = enemigo.GetComponent<Rigidbody2D>();
            if (enemigoRb != null)
            {
                Vector2 direccionEmpuje = (enemigo.transform.position - origen).normalized;
                enemigoRb.AddForce(direccionEmpuje * fuerza, ForceMode2D.Impulse);
                
                enemigo.GetComponent<PlayerControler>().TakeDamage(danio);
            }
        }
    }
}