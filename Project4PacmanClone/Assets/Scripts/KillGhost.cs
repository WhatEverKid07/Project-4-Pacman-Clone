using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGhost : MonoBehaviour
{
    [SerializeField] private Animator ghostAnimator;
    [SerializeField] private Animator pacmanAnimator;
    [SerializeField] private Transform ghostReSpawn;
    public bool canKillPacman;
    
    private ScoreAndHealth scoreAndHealth;
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        scoreAndHealth = Component.FindObjectOfType<ScoreAndHealth>();
        playerMovement = Component.FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PacMan") && canKillPacman)
        {
            print("pacman dead");
            scoreAndHealth.RemoveLife(1);

        }
        else if(collision.gameObject.CompareTag("PacMan") && !canKillPacman)
        {
            playerMovement.KillGhost();
            print("ghost dead");
        }
    }
}
