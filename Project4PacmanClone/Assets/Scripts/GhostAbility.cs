using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GhostAbility : MonoBehaviour
{
    public bool canUseController;

    [SerializeField] private float coolDownForGoingThroughWalls;
    [SerializeField] private float abilityLenghInSeconds;

    [SerializeField] private Slider abilitySlider;

    private InputAction ghostAbility;
    [SerializeField] private InputActionAsset abilitys;

    private CircleCollider2D ghostCollider;

    void Start()
    {
        canUseController = true;
        ghostCollider = GetComponent<CircleCollider2D>();

        abilitySlider.maxValue = coolDownForGoingThroughWalls;
        abilitySlider.value = 0f;
        InvokeRepeating("CoolDownRegen", 1f, 0.1f);
    }

    private void OnEnable()
    {
        // Find the action map and the action
        var playerActionMap = abilitys.FindActionMap("Controls");
        ghostAbility = playerActionMap.FindAction("GhostAbility");

        ghostAbility.Enable();
        ghostAbility.performed += OnJumpPerformed;
    }
    private void OnDisable()
    {
        ghostAbility.performed -= OnJumpPerformed;
        ghostAbility.Disable();
    }
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        print("GHOSTSPACE");
        if (abilitySlider.value == abilitySlider.maxValue && canUseController)
        {
            abilitySlider.maxValue = abilityLenghInSeconds;
            abilitySlider.value = abilitySlider.maxValue;
            InvokeRepeating("UsingAbility", 0f, 1f);
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
            CancelInvoke();
        }
    }

    private void UsingAbility()
    {
        Debug.Log("ABILITY");

        if (abilitySlider.value > 0f)
        {
            ghostCollider.enabled = false;
            abilitySlider.value -= 1f;
        }
        else if (abilitySlider.value == 0f)
        {
            ghostCollider.enabled = true;
            abilitySlider.maxValue = coolDownForGoingThroughWalls;
            CancelInvoke();
            InvokeRepeating("CoolDownRegen", 1f, 0.1f);
        }
    }
}