using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostAbility : MonoBehaviour
{
    [SerializeField] private float coolDownForGoingThroughWalls;
    [SerializeField] private float abilityLenghInSeconds;

    [SerializeField] private Slider abilitySlider;

    private CircleCollider2D ghostCollider;

    void Start()
    {
        ghostCollider = GetComponent<CircleCollider2D>();

        abilitySlider.maxValue = coolDownForGoingThroughWalls;
        abilitySlider.value = 0f;
        InvokeRepeating("CoolDownRegen", 1f, 0.1f);
    }

    void Update()
    {
        if (abilitySlider.value == abilitySlider.maxValue && Input.GetKeyDown(KeyCode.RightControl))
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