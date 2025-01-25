using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private FlowManager fm;
    public Transform spawnPoint;
    public string nextSceneName;
    public float waitTime = 1f;
    private void Awake()
    {
         fm = GetComponent<FlowManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fm.GoWithLoading(nextSceneName);
            PlayerControler player = collision.GetComponent<PlayerControler>();
            
            player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            player.WrapRespawn(waitTime);
            player.transform.position = spawnPoint.position;

        }
    } 
}

