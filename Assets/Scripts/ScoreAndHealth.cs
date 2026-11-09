using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreAndHealth : MonoBehaviour
{
    [Header("---Pacman Health---")]
    [SerializeField] private GameObject lifePrefab;
    [SerializeField] private Transform startPoint;
    [Space (5)]
    public int lives;
    [Space (5)]
    [SerializeField] private float spacing = 1f;
    //[ContextMenu("UpdateLifeDisplay")]

    private PlayerMovement playerMovement;
    private KillGhost killGhost;

    
    //[SerializeField] private int pacmanHealthInt;
    //[SerializeField] private Text pacmanHealth;
    //[SerializeField] private Text pacmanScore;
    //[SerializeField] private Image deadGhost;
    //[SerializeField] private Image deadGhost2;
    //[SerializeField] private Image aliveGhost;
    
    public PlayerMovement playerController;
    [SerializeField] private Animator ghostAnimator;
    public GameObject ghost;
    

    private void Start()
    {
        playerMovement = Component.FindObjectOfType<PlayerMovement>();
        killGhost = Component.FindObjectOfType<KillGhost>();
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
    public void RemoveLife(int newLives)
    {
        lives -= newLives;
        UpdateLifeDisplay();
        playerMovement.KillPacman();
    }
    public void AddLives(int newLives)
    {
        lives += newLives;
        UpdateLifeDisplay();
    }

    public bool L = true;
    private void Update()
    {
        if(lives == 0 && L)
        {
            //playerMovement.KillPacman();
            print("GAME OVER");
            Invoke("GameOver", 1f);
            L = false;
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
        killGhost.canKillPacman = true;
    }
    private void GameOver()
    {
        SceneManager.LoadScene("GhostEnd");
    }
}