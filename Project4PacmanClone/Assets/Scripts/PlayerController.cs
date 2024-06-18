using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("---Pacman---")]
    [SerializeField] private Rigidbody2D rb;
    public float speed = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private CircleCollider2D pacCollider;
    [SerializeField] private GameObject pacman;

    [Header("---Ghost---")]
    [SerializeField] private Rigidbody2D rb2;
    public float speed2 = 5f;
    [SerializeField] private Animator animator2;

    [Space(20)]

    [SerializeField] private Transform spawnLocation;

    [HideInInspector]
    public bool isGhostDead;
    private ScoreAndHealth scoreAndHealth;
    public bool canMove;
    public bool canMove2;

    private void Start()
    {
        scoreAndHealth = Component.FindObjectOfType<ScoreAndHealth>();
        canMove = true;
        canMove2 = true;
    }

    void Update()
    {
        if (canMove)
        {
            PlayerOne();
        }
        if (canMove2)
        {
            PlayerTwo();
        }
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
            //print("GHOST IS DEAD");
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

    public void KillPacman()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        canMove = false;
        pacCollider.enabled = false;
        animator.SetTrigger("PacmanDead");
        StartCoroutine(RespawnPacman());
    }

    private IEnumerator RespawnPacman()
    {
        yield return new WaitForSeconds(1);
        pacman.transform.position = spawnLocation.transform.position;
        pacCollider.enabled = true;
        animator.SetTrigger("PacmanRight");
        if(scoreAndHealth.lives == 0)
        {
            scoreAndHealth.AddLives(3);
        }
        scoreAndHealth.L = true;
        canMove = true;
    }
}