using UnityEngine;

public class TPprueba : MonoBehaviour
{
    private FlowManager fm;

    private void Awake()
    {
         fm = GetComponent<FlowManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fm.GoToDirectly("Cambio2");
        }
    }
}

