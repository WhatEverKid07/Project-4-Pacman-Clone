using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PacmanAbility : MonoBehaviour
{
    public bool canUseController;

    [SerializeField] private float coolDownForAbility;
    [SerializeField] private float lenghOfSpeedBoost;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private Slider abilitySlider;
    [SerializeField] private PlayerMovement playerController;

    private InputAction pacmanAbility;
    [SerializeField] private InputActionAsset abilitys;

    private CircleCollider2D pacmanCollider;
    private Rigidbody2D pacmanRigidbody;
    
    private bool isBoosting = false;

    void Start()
    {
        canUseController = true;
        pacmanCollider = GetComponent<CircleCollider2D>();
        pacmanRigidbody = GetComponent<Rigidbody2D>();

        abilitySlider.maxValue = coolDownForAbility;
        abilitySlider.value = 0f;
        InvokeRepeating("CoolDownRegen", 1f, 0.1f);
    }

    private void OnEnable()
    {
        // Find the action map and the action
        var playerActionMap = abilitys.FindActionMap("Controls");
        pacmanAbility = playerActionMap.FindAction("PacmanAbility");

        pacmanAbility.Enable();
        pacmanAbility.performed += OnJumpPerformed;
    }
    private void OnDisable()
    {
        pacmanAbility.performed -= OnJumpPerformed;
        pacmanAbility.Disable();
    }
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        print("SPACE");
        if (abilitySlider.value == abilitySlider.maxValue && canUseController)
        {
            InvokeRepeating("Boost", 0f, 1f);
            UsingAbility();
        }
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