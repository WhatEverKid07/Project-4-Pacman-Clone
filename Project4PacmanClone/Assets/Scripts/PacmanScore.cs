using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacmanScore : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private int goUp = 10;
    protected static int score;

    private ScoreAndHealth scoreAndHealth;
    private KillGhost killGhost;

    void Start()
    {
        scoreAndHealth = Component.FindObjectOfType<ScoreAndHealth>();
        killGhost = Component.FindObjectOfType<KillGhost>();
        score = 0;
        UpdateScoreText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            score += goUp;
            UpdateScoreText();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("DeadPoint"))
        {
            killGhost.canKillPacman = false;
            scoreAndHealth.DeadGhost();
            Destroy(collision.gameObject);
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
