using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("---Pacman---")]
    [SerializeField] private Rigidbody2D rb;
    public float speed = 5f;
    [SerializeField] private Animator animator;

    [Header("---Ghost---")]
    [SerializeField] private Rigidbody2D rb2;
    public float speed2 = 5f;
    [SerializeField] private Animator animator2;

    [HideInInspector]
    public bool isGhostDead;


    void Update()
    {
        PlayerOne();
        PlayerTwo();
    }
    private void PlayerOne()
    {
        CheckAnimations();
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;

        //make the player NOT smoothley stop
        if (movement == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void PlayerTwo()
    {
        if(!isGhostDead)
        {
            GhostSprites();
            print("GHOST IS DEAD");
        }
        float moveHorizontal2 = Input.GetAxis("Horizontal2");
        float moveVertical2 = Input.GetAxis("Vertical2");
        Vector2 movement2 = new Vector2(moveHorizontal2, moveVertical2);
        rb2.velocity = movement2 * speed2;

        //make the player NOT smoothley stop
        if (movement2 == Vector2.zero)
        {
            rb2.velocity = Vector2.zero;
        }
    }

    private void GhostSprites()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isGhostDead)
        {
            animator2.SetTrigger("GhostLeft");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isGhostDead)
        {
            animator2.SetTrigger("GhostRight");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isGhostDead)
        {
            animator2.SetTrigger("GhostUp");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isGhostDead)
        {
            animator2.SetTrigger("GhostDown");
        }
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