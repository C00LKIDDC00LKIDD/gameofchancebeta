using UnityEngine;

/// <summary>
/// Shoots bullets in the direction the player is moving.
/// Attach to the player GameObject.
/// </summary>
public class ShootSpriteDirectional : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab; // Prefab with SpriteRenderer + Rigidbody2D
    public float bulletSpeed = 10f;
    public float bulletLifetime = 3f;

    [Header("Input Settings")]
    public KeyCode shootKey = KeyCode.Space;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Vector2 lastMoveDirection = Vector2.right; // Default facing right

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    /// <summary>
    /// Handles player movement and updates lastMoveDirection.
    /// </summary>
    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveInput = new Vector2(moveX, moveY).normalized;

        // Move player
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);

        // Update last move direction if moving
        if (moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput;
        }
    }

    /// <summary>
    /// Fires bullet in last movement direction.
    /// </summary>
    private void HandleShooting()
    {
        if (Input.GetKeyDown(shootKey))
        {
            Shoot(lastMoveDirection);
        }
    }

    private void Shoot(Vector2 direction)
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab is not assigned!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("Bullet prefab has no Rigidbody2D — movement won't be physics-based.");
        }

        Destroy(bullet, bulletLifetime);
    }
}
