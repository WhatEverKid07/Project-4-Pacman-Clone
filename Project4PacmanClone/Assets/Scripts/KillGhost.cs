using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGhost : MonoBehaviour
{
    [SerializeField] private Animator ghostAnimator;
    [SerializeField] private Animator pacmanAnimator;
    [SerializeField] private Transform ghostReSpawn;
    [SerializeField] private AudioSource ghostDead;
    [SerializeField] private AudioSource pacmanDead;
    public bool canKillPacman;
    private bool KILL = true;
    
    private ScoreAndHealth scoreAndHealth;
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        scoreAndHealth = Component.FindObjectOfType<ScoreAndHealth>();
        playerMovement = Component.FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PacMan") && canKillPacman && KILL)
        {
            KILL = false;
            pacmanDead.Play();
            print("pacman dead");
            scoreAndHealth.RemoveLife(1);
            Invoke("KillBoolSetToTrue", 1f);
        }
        else if(collision.gameObject.CompareTag("PacMan") && !canKillPacman)
        {
            playerMovement.KillGhost();
            print("ghost dead");
            ghostDead.Play();
        }
    }

    private void KillBoolSetToTrue()
    {
        KILL = true;
    }
}
