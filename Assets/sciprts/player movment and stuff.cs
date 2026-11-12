using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement_NewInput : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    public UIDocument uidocument;
    private Label lose;
    private Label youwin;
    private Button replay;
    private Rigidbody2D rb;
    private float moveInput;
    public bool isGrounded = true;
    bool facingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        youwin = uidocument.rootVisualElement.Q<UnityEngine.UIElements.Label>("youwin");
        lose = uidocument.rootVisualElement.Q<UnityEngine.UIElements.Label>("lose");
        youwin.style.display = DisplayStyle.None;
        replay = uidocument.rootVisualElement.Q<UnityEngine.UIElements.Button>("replay");
        lose.style.display = DisplayStyle.None;
        replay.style.display = DisplayStyle.None;
        replay.clicked += ReloadScene;
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (isGrounded == true)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                isGrounded = false;
            }
        }


        float move = UnityEngine.Input.GetAxisRaw("Horizontal");
        if (move < 0 && facingRight) Flip();
        else if (move > 0 && !facingRight) Flip();
        
    }

    



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("death"))
        {
            Destroy(gameObject);
            lose.style.display = DisplayStyle.Flex;
            replay.style.display = DisplayStyle.Flex;
        }
        if (collision.gameObject.CompareTag("flag"))
        {
            Destroy(gameObject);
            youwin.style.display = DisplayStyle.Flex;
            replay.style.display = DisplayStyle.Flex;
        }
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    // Read Vector2 from Move action
    public void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        moveInput = inputVector.x; // Only horizontal axis for 2D movement
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    void ReloadScene()
    {
        //reloading scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Flip()
    {
        facingRight = !facingRight;
        GetComponent<SpriteRenderer>().flipX = !facingRight;
    }
}



