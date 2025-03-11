using UnityEngine;
using UnityEngine.SceneManagement; // Needed for reloading the scene

public class Tracker : MonoBehaviour
{
    GameObject player;
    public float detectionRange = 5f; // Detection distance

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        if (player != null && IsPlayerVisible()) 
        {
            GameOver(); // Trigger game over if player is detected
        }
    }

    private bool IsPlayerVisible()
{
    Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
    float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

    // Check if the player is within the detection range
    if (distanceToPlayer > detectionRange)
        return false;

    // Raycast to check if there's an obstacle between turret and player
    RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange);

    // Correct Debug.DrawRay to stop at the hit point
    if (hit.collider != null)
    {
        Debug.DrawRay(transform.position, directionToPlayer * hit.distance, Color.red); // Stops at first obstacle
        if (hit.collider.CompareTag("Player"))
        {
            return true; // Player is visible
        }
    }
    else
    {
        Debug.DrawRay(transform.position, directionToPlayer * detectionRange, Color.red); // No obstacle detected
    }

    return false;
}


    void GameOver()
{
    Debug.Log("Game Over! Player detected by red frog jerk.");
    Invoke("RestartGame", 0.5f); // Wait 2 seconds before restarting
}

void RestartGame()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}

}
