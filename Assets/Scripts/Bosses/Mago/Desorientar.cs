using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Bosses.Mago
{
    public class Desorientar
    {
        private float duracion;

        public Desorientar(float duracion)
        {
            this.duracion = duracion;
        }

        public void Aplicar(GameObject enemigo)
        {
            // Lógica para desorientar al enemigo
            // Por ejemplo, podrías desactivar sus controles por un tiempo
            enemigo.GetComponent<MonoBehaviour>().StartCoroutine(DesorientarCoroutine(enemigo));
        }

        private IEnumerator DesorientarCoroutine(GameObject enemigo)
        {
            // Desactivar controles del enemigo
            enemigo.GetComponent<PlayerControler>().enabled = false;
            enemigo.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            yield return new WaitForSeconds(duracion);
            // Reactivar controles del enemigo
            enemigo.GetComponent<PlayerControler>().enabled = true;
        }
    }
}