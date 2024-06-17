using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAndHealth : MonoBehaviour
{/*
    [SerializeField] private int pacmanHealthInt;
    [SerializeField] private Text pacmanHealth;
    [SerializeField] private Text pacmanScore;
    [SerializeField] private Image deadGhost;
    [SerializeField] private Image deadGhost2;
    [SerializeField] private Image aliveGhost;
    */
    public PlayerMovement playerController;
    [SerializeField] private Animator ghostAnimator;
    public GameObject ghost;


    private void Start()
    {
        //pacmanHealth.text = pacmanHealthInt.ToString();
    }
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
}
