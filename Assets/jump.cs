using UnityEngine;

public class Jumper : MonoBehaviour
{
    public float jumpForce = 5f;  // Jump strength
    public float moveSpeed = 3f;  // Horizontal movement speed

    private Rigidbody2D rb;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get Rigidbody2D component
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");  // Get left (-1) / right (1) input
        Debug.Log(IsGrounded());

        // Jump when pressing Space and grounded
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput) * Mathf.Abs(transform.localScale.x), 
                                   transform.localScale.y, 
                                   transform.localScale.z);

        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Apply upward force
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        float spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
        position += Vector2.down * (spriteHeight / 2);

        Vector2 direction = Vector2.down;
        float distance = 0.1f;

        Debug.DrawRay(position, direction * distance, Color.blue);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance);
        return hit.collider != null;
    }
}
