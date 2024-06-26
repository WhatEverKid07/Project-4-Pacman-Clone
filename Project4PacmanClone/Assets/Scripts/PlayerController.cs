using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [Header("---Pacman---")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [SerializeField] private CircleCollider2D pacCollider;
    [SerializeField] private GameObject pacman;
    public InputAction pacmanControls;

    public float speed = 5f;

    [Header("---Ghost---")]
    [SerializeField] private Rigidbody2D rb2;
    [SerializeField] private Animator animator2;

    [SerializeField] private CircleCollider2D ghostCollider;
    [SerializeField] private GameObject ghost;
    public InputAction ghostControls;

    public float speed2 = 5f;

    [Space(20)]

    [SerializeField] private List<GameObject> pacmanSpawnLocations;
    [SerializeField] private List<GameObject> ghostSpawnLocations;
    [SerializeField] private float distance = Mathf.Infinity;

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
        //buttonIdentifier.text = movement.ToString();

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
        //buttonIdentifier2.text = movement2.ToString();

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
        
        GameObject furthestGameObject = FindFurthestPacmanRespawnPoint(pacman, pacmanSpawnLocations);
        if (furthestGameObject != null)
        {
            StartCoroutine(RespawnPacman(furthestGameObject.transform.position));
        }
    }

    GameObject FindFurthestPacmanRespawnPoint(GameObject fromObject, List<GameObject> objects)
    {
        GameObject furthestObject = null;
        float maxDistance = float.MinValue;

        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(fromObject.transform.position, obj.transform.position);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                furthestObject = obj;
            }
        }
        return furthestObject;
    }
    private IEnumerator RespawnPacman(Vector3 position)
    {
        yield return new WaitForSeconds(1);
        pacman.transform.position = position;
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
        GameObject furthestGameObject = FindFurthestGhostRespawnPoint(ghost, ghostSpawnLocations);
        if (furthestGameObject != null)
        {
            StartCoroutine(RespawnGhost(furthestGameObject.transform.position));
        }
    }


    GameObject FindFurthestGhostRespawnPoint(GameObject fromObject, List<GameObject> objects)
    {
        GameObject furthestObject = null;
        float maxDistance = float.MinValue;

        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(fromObject.transform.position, obj.transform.position);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                furthestObject = obj;
            }
        }
        return furthestObject;
    }
    private IEnumerator RespawnGhost(Vector3 position)
    {
        yield return new WaitForSeconds(0.2f);
        ghost.transform.position = position;
        ghostCollider.enabled = true;

        ResetGhostAnimations();
        if (scoreAndHealth.lives == 0)
        {
            scoreAndHealth.AddLives(3);
        }
        canMove2 = true;
    }
}