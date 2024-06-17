using UnityEngine;
using UnityEngine.UI;

public class ScoreAndHealth : MonoBehaviour
{
    [SerializeField] private int pacmanHealthInt;
    [SerializeField] private Text pacmanHealth;
    [SerializeField] private Text pacmanScore;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Image deadGhost;
    [SerializeField] private Image deadGhost2;
    [SerializeField] private Image aliveGhost;

    [SerializeField] private GameObject ghost;
    [SerializeField] private SpriteRenderer ghostSprite;


    private void Start()
    {
        pacmanHealth.text = pacmanHealthInt.ToString();
        ghostSprite = ghost.GetComponent<SpriteRenderer>();
    }
}
