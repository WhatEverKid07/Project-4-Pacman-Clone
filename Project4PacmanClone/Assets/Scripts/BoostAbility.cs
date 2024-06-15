using UnityEngine;

public class BoostPowerUp : MonoBehaviour
{
    public float boostAmount = 10f; // Amount of boost
    public float boostDuration = 0.5f; // Duration of the boost
    public PlayerMovement characterController;
    private Rigidbody2D rb;
    private bool isBoosting = false;
    private float boostEndTime = 0f;

    void Start()
    {
        //characterController = GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isBoosting) // Change "Space" to any desired key
        {
            Boost();
        }

        if (isBoosting && Time.time >= boostEndTime)
        {
            EndBoost();
        }
    }

    void Boost()
    {
        isBoosting = true;
        boostEndTime = Time.time + boostDuration;
        Vector2 boostDirection = rb.velocity.normalized;
        rb.velocity = boostDirection * boostAmount;
    }

    void EndBoost()
    {
        isBoosting = false;
        rb.velocity = rb.velocity.normalized * characterController.speed;
    }
}
