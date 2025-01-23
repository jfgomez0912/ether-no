using UnityEngine;
using Assets.Scripts.Bosses.Mago;
using Assets.Scripts.Bosses;

public class AttackController : MonoBehaviour   
{

    private int _damage = 10;

    private Transform punto;

    private void Awake()
    {
        punto= GetComponent<Transform>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            Transform puntoEnemigo = collision.gameObject.GetComponent<Transform>();

            float distance = Vector2.Distance(punto.position, puntoEnemigo.position);

            if (distance < 1)
            {
                print("Ataque");
                if (collision.name == "Mago")
                {
                    collision.gameObject.GetComponent<Mago>().TakeDamage(_damage);
                }
                else if (collision.name == "Cientifico")
                {
                    collision.gameObject.GetComponent<Cientifico>().TakeDamage(_damage);
                }
                else
                {
                    collision.gameObject.GetComponent<EnemyController>().TakeDamage(_damage);
                }
            }
        }
    }
}
