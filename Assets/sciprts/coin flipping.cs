using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
using static UnityEngine.GraphicsBuffer;

public class coinfilpping : MonoBehaviour
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
    [SerializeField] AudioSource SFX_Fire;
    [SerializeField] AudioSource SFX_Lose;
    [SerializeField] AudioSource SFX_Win;
    [SerializeField] ParticleSystem VFX_Fire;
        // Reference to the gun sprite
   
    public GameObject targetObject;
    private Rigidbody2D rb;
    public int coins;
    public int weekness;
    private Vector3 direction;
    private float speed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coins = 0;
        weekness = 0;




    }

   

    // Update is called once per frame
    void Update()
    {

        HandleMovement();
        HandleShooting();
        if (coins >= 1 && Keyboard.current.eKey.wasPressedThisFrame)
        {


                SFX_Fire.Play();
                coins -= 1;

            if (Input.GetKeyDown(KeyCode.E))
            {
                Shoot(lastMoveDirection);
            }




        }

        transform.position += direction * speed * Time.deltaTime;



        if (Keyboard.current.qKey.wasPressedThisFrame)
        {

         float randomNumber = Random.Range(0, 2);
            if (randomNumber == 1)

            {

                SFX_Win.Play();

                coins += 1;
               

            }
            if(randomNumber == 0)
            {
                SFX_Lose.Play();
                weekness += 1;

            }
            
         


        }

    }
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
    private void HandleShooting()
    {
 
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
