using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PacmanAbility : MonoBehaviour
{
    [SerializeField] private float coolDownForAbility;
    [SerializeField] private float lenghOfSpeedBoost;
    [SerializeField] private float speedMultiplier;

    [SerializeField] private Slider abilitySlider;
    [SerializeField] private PlayerMovement playerController;
    [SerializeField] private InputActionAsset abilitys;

    [SerializeField] private Text abilityButtonIdentifier;

    private CircleCollider2D pacmanCollider;
    private Rigidbody2D pacmanRigidbody;
    private InputAction pacmanAbility;

    public bool canUseController;

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
            abilityButtonIdentifier.text = context.control.displayName;
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

        if (abilitySlider.value > 0f)
        {
            abilitySlider.value -= lenghOfSpeedBoost;
        }
        else if (abilitySlider.value == 0f)
        {
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