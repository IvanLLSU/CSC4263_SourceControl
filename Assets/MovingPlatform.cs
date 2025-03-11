using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f; // Platform movement speed
    public float moveDistance = 3f; // Distance the platform moves from its start position

    private Vector2 startPosition;
    private int direction = 1; // 1 = right, -1 = left

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Move platform
        transform.position += Vector3.right * direction * speed * Time.deltaTime;

        // Check if it has reached the maximum distance and reverse direction
        if (Vector2.Distance(startPosition, transform.position) >= moveDistance)
        {
            direction *= -1; // Reverse direction
        }
    }
}
