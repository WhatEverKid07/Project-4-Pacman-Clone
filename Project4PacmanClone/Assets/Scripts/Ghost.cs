using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float coolDownForGoingThroughWalls;
    [SerializeField] private float abilityLenghInSeconds;
    [SerializeField] public Slider abilitySlider;
    [SerializeField] public Slider abilityLengh;
    [SerializeField] private bool isUsingAbility = false;
    private CircleCollider2D ghostCollider;

    void Start()
    {
        ghostCollider = GetComponent<CircleCollider2D>();
        abilitySlider.maxValue = coolDownForGoingThroughWalls;
        abilityLengh.maxValue = abilityLenghInSeconds;
        abilitySlider.minValue = 0;
        abilityLengh.minValue = 0;
        abilitySlider.value = 0;
        abilityLengh.value = 0;
        abilitySlider.value = abilitySlider.maxValue;
        InvokeRepeating("LoseAbility", 0f, 0.2f);

    }

    void Update()
    {
        CheckIfCanUseAbility();
    }

    private void CheckIfCanUseAbility()
    {
        if (abilitySlider.value == coolDownForGoingThroughWalls && Input.GetKeyDown(KeyCode.RightControl))
        {
            isUsingAbility = true;
            /*
            isUsingAbility = true;
            abilitySlider.value = 0;
            */
        }
        if (isUsingAbility)
        {
            //ghostCollider.enabled = false;
            //StartCoroutine(DuringAbility());
        }
    }
    private IEnumerator DuringAbility()
    {
        yield return new WaitForSeconds(abilityLenghInSeconds);
        ghostCollider.enabled = true;
        isUsingAbility = false;
    }
    void LoseAbility()
    {
        //GainAbility();
        abilityLengh.value += 1;
        if (abilityLengh.value == abilityLengh.maxValue)
        {
            CancelInvoke();
        }
    }
    void GainAbility()
    {
        abilitySlider.value = 0;
        if(abilitySlider.value < coolDownForGoingThroughWalls && !isUsingAbility)
        {

        }
        abilitySlider.value += 1;
    }
}