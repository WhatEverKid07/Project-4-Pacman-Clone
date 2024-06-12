using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement speed
    public float speed = 5f;
    public Animator animator;

    // Reference to the Rigidbody2D component
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAnimations();
        // Get input from WASD keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a new vector for movement
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Apply movement to the Rigidbody2D
        rb.velocity = movement * speed;
    }

    private void CheckAnimations()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("PacmanUp");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("PacmanLeft");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger("PacmanDown");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("PacmanRight");
        }
    }
}