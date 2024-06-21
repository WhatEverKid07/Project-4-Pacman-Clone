using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("---Pacman---")]
    [SerializeField] private Rigidbody2D rb;
    public float speed = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private CircleCollider2D pacCollider;
    [SerializeField] private GameObject pacman;
    public InputAction pacmanControls;

    [Header("---Ghost---")]
    [SerializeField] private Rigidbody2D rb2;
    public float speed2 = 5f;
    [SerializeField] private Animator animator2;
    [SerializeField] private CircleCollider2D ghostCollider;
    [SerializeField] private GameObject ghost;
    public InputAction ghostControls;

    [Space(20)]

    [SerializeField] private Transform spawnLocation;
    [SerializeField] private Transform ghostSpawnLocation;

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

    private void OnEnable()
    {
        pacmanControls.Enable();
        ghostControls.Enable();
    }

    private void OnDisable()
    {
        pacmanControls.Disable();
        ghostControls.Disable();
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
        Vector2 movement = pacmanControls.ReadValue<Vector2>();
        rb.velocity = movement * speed;

        if (movement == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
            ResetPacmanAnimations();
        }
        else
        {
            UpdatePacmanAnimation(movement);
        }
    }

    private void PlayerTwo()
    {
        Vector2 movement2 = ghostControls.ReadValue<Vector2>();
        rb2.velocity = movement2 * speed2;

        if (!isGhostDead)
        {
            if (movement2 == Vector2.zero)
            {
                rb2.velocity = Vector2.zero;
                ResetGhostAnimations();
            }
            else
            {
                UpdateGhostAnimation(movement2);
            }
        }
    }

    private void UpdatePacmanAnimation(Vector2 movement)
    {
        animator.SetBool("PacmanRight 0", movement.x > 0);
        animator.SetBool("PacmanLeft 0", movement.x < 0);
        animator.SetBool("PacmanUp 0", movement.y > 0);
        animator.SetBool("PacmanDown 0", movement.y < 0);
    }

    private void ResetPacmanAnimations()
    {
        animator.SetBool("PacmanRight 0", false);
        animator.SetBool("PacmanLeft 0", false);
        animator.SetBool("PacmanUp 0", false);
        animator.SetBool("PacmanDown 0", false);
    }

    private void UpdateGhostAnimation(Vector2 movement)
    {
        animator2.SetBool("GhostRight", movement.x > 0);
        animator2.SetBool("GhostLeft", movement.x < 0);
        animator2.SetBool("GhostUp", movement.y > 0);
        animator2.SetBool("GhostDown", movement.y < 0);
    }

    private void ResetGhostAnimations()
    {
        animator2.SetBool("GhostRight", false);
        animator2.SetBool("GhostLeft", false);
        animator2.SetBool("GhostUp", false);
        animator2.SetBool("GhostDown", false);
    }

    public void KillPacman()
    {
        pacCollider.enabled = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        canMove = false;
        animator.SetTrigger("PacmanDead");
        StartCoroutine(RespawnPacman());
    }

    private IEnumerator RespawnPacman()
    {
        yield return new WaitForSeconds(1);
        pacman.transform.position = spawnLocation.transform.position;
        pacCollider.enabled = true;
        ResetPacmanAnimations();
        if (scoreAndHealth.lives == 0)
        {
            scoreAndHealth.AddLives(3);
        }
        scoreAndHealth.L = true;
        canMove = true;
    }

    public void KillGhost()
    {
        rb2.velocity = Vector2.zero;
        rb2.angularVelocity = 0;
        canMove2 = false;
        ghostCollider.enabled = false;
        StartCoroutine(RespawnGhost());
    }

    private IEnumerator RespawnGhost()
    {
        yield return new WaitForSeconds(0.2f);
        ghost.transform.position = ghostSpawnLocation.transform.position;
        ghostCollider.enabled = true;
        ResetGhostAnimations();
        if (scoreAndHealth.lives == 0)
        {
            scoreAndHealth.AddLives(3);
        }
        canMove2 = true;
    }
}
