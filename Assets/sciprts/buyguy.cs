using UnityEngine;

public class buyguy : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 50f;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = player.position - transform.position;
        transform.position += (Vector3)dir.normalized * followSpeed * Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the tag "Player"
        if (collision.gameObject.CompareTag("bullet"))
        {
            // Example: Destroy bullet after hit
            Destroy(gameObject);
        }
    }
}
