using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacmanScore : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private int goUp = 10;
    private int score;

    void Start()
    {
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
    }
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
