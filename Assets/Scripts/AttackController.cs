using UnityEngine;

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
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(_damage);
            }
        }
    }
}
