using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGhostCollision : MonoBehaviour
{
    [SerializeField]
    private ScoreAndHealth scoreAndHealth;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pacman")
        {
            Destroy(collision.gameObject);
            scoreAndHealth.DeadGhost();
            print("DeadPoint");
        }
    }
}
