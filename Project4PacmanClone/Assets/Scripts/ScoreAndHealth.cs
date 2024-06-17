using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndHealth : MonoBehaviour
{
    [Header("---Pacman Health---")]
    [SerializeField] private GameObject lifePrefab;
    [SerializeField] private Transform startPoint;
    [Space (5)]
    [SerializeField] private int lives;
    [Space (5)]
    [SerializeField] private float spacing = 1f;
    [ContextMenu("UpdateLifeDisplay")]

    /*
    [SerializeField] private int pacmanHealthInt;
    [SerializeField] private Text pacmanHealth;
    [SerializeField] private Text pacmanScore;
    [SerializeField] private Image deadGhost;
    [SerializeField] private Image deadGhost2;
    [SerializeField] private Image aliveGhost;
    
    public PlayerMovement playerController;
    [SerializeField] private Animator ghostAnimator;
    public GameObject ghost;
    */

    private void Start()
    {
        //pacmanHealth.text = pacmanHealthInt.ToString();
        UpdateLifeDisplay();
    }

    void UpdateLifeDisplay()
    {
        // Clear existing lives
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        // Add life sprites based on the number of lives
        for (int i = 0; i < lives; i++)
        {
            Vector3 position = startPoint.position + new Vector3(spacing * i, 0, 0);
            Instantiate(lifePrefab, position, Quaternion.identity, transform);
        }
    }
    public void SetLives(int newLives)
    {
        lives = newLives;
        UpdateLifeDisplay();
    }
    /*

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.C))
        {
            playerController.isGhostDead = true;
            ghost.tag = "DeadGhost";
            ghostAnimator.SetTrigger("IsDead");
            StartCoroutine(IsGhostedDead());
        }
    }

    public void DeadGhost()
    {
        playerController.isGhostDead = true;
        ghost.tag = "DeadGhost";
        ghostAnimator.SetTrigger("IsDead");
        StartCoroutine(IsGhostedDead());
    }

    private IEnumerator IsGhostedDead()
    {
        yield return new WaitForSeconds(3);
        playerController.isGhostDead = false;
        ghost.tag = "Ghost";
        ghostAnimator.SetTrigger("GhostUp");
    }
    */
}
