using UnityEngine;

public class TPprueba : MonoBehaviour
{
    private FlowManager fm;
    public Transform spawnPoint;

    private void Awake()
    {
         fm = GetComponent<FlowManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerControler player = collision.GetComponent<PlayerControler>();
            
            player.transform.position = spawnPoint.position;
            player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            fm.GoToDirectly("Cambio2");
    
        }
    }
}

