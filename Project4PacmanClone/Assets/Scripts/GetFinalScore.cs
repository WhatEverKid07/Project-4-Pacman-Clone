using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetFinalScore : PacmanScore
{
    [SerializeField] private Text finalScoreText;
    private void Start()
    {
        finalScoreText.text = score.ToString();
    }
}
