using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    public GameObject bubble;
    public Transform spawnPointBubble;
    private GameObject bubbleRef;
    private Rigidbody2D rb;

    void throwBubble()
    {
        bubbleRef = Instantiate(bubble, spawnPointBubble);
        rb = bubbleRef.GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(3.5f,1.5f);
        rb.AddForce(direction, ForceMode2D.Impulse);
    }

    void throwCloud()
    {
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;
    }
}
