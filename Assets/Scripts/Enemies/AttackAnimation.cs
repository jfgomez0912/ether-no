using System;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    public GameObject bubble;
    public GameObject cloud;
    public Transform spawnPointBubble;
    public Transform spawnPointCloud;
    private GameObject bubbleRef;
    private GameObject cloudRef;
    private Rigidbody2D rb;

    void throwBubble()
    {
        print("Lanzar burbuja");
        bubbleRef = Instantiate(bubble, spawnPointBubble);
        rb = bubbleRef.GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(3.5f,1.5f);
        rb.AddForce(direction, ForceMode2D.Impulse);
    }

    void throwCloud()
    {
        print("Lanzar nube");
        Destroy(bubbleRef);
        cloudRef = Instantiate(cloud, spawnPointCloud);
        
    }
}
