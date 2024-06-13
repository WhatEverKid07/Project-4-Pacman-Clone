using UnityEngine;

public class GhostController : MonoBehaviour
{
    // Movement speed
    public float speed2 = 5f;
    public Animator animator;

    // Reference to the Rigidbody2D component
    private Rigidbody2D rb2;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckAnimations();
        // Get input from WASD keys
        float moveHorizontal2 = Input.GetAxis("Horizontal2");
        float moveVertical2 = Input.GetAxis("Vertical2");

        Debug.Log(moveVertical2);
        Debug.Log(moveHorizontal2);
        // Create a new vector for movement
        Vector2 movement2 = new Vector2(moveHorizontal2, moveVertical2);

        // Apply movement to the Rigidbody2D
        rb2.velocity = movement2 * speed2;
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