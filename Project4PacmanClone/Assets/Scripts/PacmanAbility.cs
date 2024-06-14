using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacmanAbility : MonoBehaviour
{
    [SerializeField] private float coolDownForGoingThroughWalls;
    [SerializeField] private float boostSpeed;

    [SerializeField] private Slider abilitySlider;

    private CircleCollider2D pacmanCollider;
    private Rigidbody2D pacmanRigibody;

    void Start()
    {
        pacmanCollider = GetComponent<CircleCollider2D>();
        pacmanRigibody = GetComponent<Rigidbody2D>();

        abilitySlider.maxValue = coolDownForGoingThroughWalls;
        abilitySlider.value = 0f;
        InvokeRepeating("CoolDownRegen", 1f, 0.1f);
    }

    void Update()
    {
        if (abilitySlider.value == abilitySlider.maxValue && Input.GetKeyDown(KeyCode.Space))
        {
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
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            pacmanRigibody.velocity = movement * boostSpeed;


            abilitySlider.value -= coolDownForGoingThroughWalls;
        }
        else if (abilitySlider.value == 0f)
        {
            abilitySlider.maxValue = coolDownForGoingThroughWalls;
            CancelInvoke();
            InvokeRepeating("CoolDownRegen", 1f, 0.1f);
        }
    }
}