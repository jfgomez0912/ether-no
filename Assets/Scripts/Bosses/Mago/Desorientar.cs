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
            // enemigo.GetComponent<ControlEnemigo>().enabled = false;
            yield return new WaitForSeconds(duracion);
            // Reactivar controles del enemigo
            // enemigo.GetComponent<ControlEnemigo>().enabled = true;
        }
    }
}