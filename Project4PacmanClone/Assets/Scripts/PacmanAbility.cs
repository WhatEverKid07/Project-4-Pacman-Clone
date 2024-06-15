using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacmanAbility : MonoBehaviour
{
    [SerializeField] private float coolDownForAbility;
    [SerializeField] private float lenghOfSpeedBoost;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private Slider abilitySlider;
    [SerializeField] private PlayerMovement playerController;

    private CircleCollider2D pacmanCollider;
    private Rigidbody2D pacmanRigidbody;
    
    private bool isBoosting = false;
    /*
    private float boostDuration = 0.2f; // Duration of the boost
    private float boostEndTime = 0f;
    */
    void Start()
    {
        pacmanCollider = GetComponent<CircleCollider2D>();
        pacmanRigidbody = GetComponent<Rigidbody2D>();

        abilitySlider.maxValue = coolDownForAbility;
        abilitySlider.value = 0f;
        InvokeRepeating("CoolDownRegen", 1f, 0.1f);
    }

    void Update()
    {
        if (abilitySlider.value == abilitySlider.maxValue && Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Boost", 0f, 1f);
            UsingAbility();
        }
        /*
        if (isBoosting && Time.time < boostEndTime)
        {
            Vector2 boostDirection = pacmanRigidbody.velocity.normalized;
            pacmanRigidbody.velocity = boostDirection * boostSpeed;
        }
        else if (isBoosting && Time.time >= boostEndTime)
        {
            isBoosting = false;
            pacmanRigidbody.velocity = pacmanRigidbody.velocity.normalized * playerController.speed; // Resume normal speed
        }
        */
    }

    private void CoolDownRegen()
    {
        if (abilitySlider.value < abilitySlider.maxValue)
        {
            abilitySlider.value += 1f;
        }
        else if (abilitySlider.value == abilitySlider.maxValue)
        {
            Debug.Log("Full Bar");
            CancelInvoke("CoolDownRegen");
        }
    }

    private void Boost()
    {
        Debug.Log("ABILITY");

        //isBoosting = true;
        //boostEndTime = Time.time + boostDuration;

        if (abilitySlider.value > 0f)
        {
            //The ability

            abilitySlider.value -= lenghOfSpeedBoost;
        }
        else if (abilitySlider.value == 0f)
        {
            //go back to normal
            playerController.speed /= speedMultiplier;
            abilitySlider.maxValue = coolDownForAbility;
            CancelInvoke();
            InvokeRepeating("CoolDownRegen", 1f, 0.1f);
        }
    }

    
    private void UsingAbility()
    {
        playerController.speed *= speedMultiplier;
    }
    
}