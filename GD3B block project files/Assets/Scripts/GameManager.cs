using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image fedBar;
    public Image hydratedBar;
    public Image energyBar;

    float fed = 100;
    float hydrated = 100;
    float energy = 100;

    bool isEating;
    bool isResting;
    bool isDrinking;

    bool waterCollected;
    bool woodCollected;

    public bool collectWaterEnabled;
    public GameObject collectWaterPrompt;
    public GameObject collectingWater;

    public bool collectWoodEnabled;
    public GameObject collectWoodPrompt;
    public GameObject collectingWood;

    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /// CONDITION BARS DECREASING OVER TIME
        if (fed > 1 && !isEating) //&& no increase bools are true or something like that
        {
            fed -= Time.deltaTime;
        }
        fedBar.fillAmount = fed / 100;

        if (hydrated > 1 && !isDrinking)
        {
            hydrated -= Time.deltaTime;
        }
        hydratedBar.fillAmount = hydrated / 100;

        if (energy > 1 && !isResting)
        {
            energy -= Time.deltaTime;
        }
        energyBar.fillAmount = energy / 100;


    /// CONDITION BARS INCREASING WITH ACTIONS (need to first activate these bools in-game)
        if (isEating && fed < 100)
        {
            fed += 2 * Time.deltaTime;

            //amountFed += 2 * Time.deltaTime;
            //set bool false once amountFed has reached desired value;
        }


        if (isDrinking && hydrated < 100)
        {
            hydrated += 2 * Time.deltaTime;

            //amountDrank += 2 * Time.deltaTime;
            //set bool false once amountDrank has reached desired value;
        }

        if (isResting && energy < 100)
        {
            energy += 2 * Time.deltaTime;

            //amountRested += 2 * Time.deltaTime;
            //set bool false once amountRested has reached desired value;
        }

        /// LOSE CONDITION
        if (fed <= 1 || hydrated <= 1 || energy <= 1)
        {
            //loseFunction
        }

        ///TRIGGER COLLECTING STUFF
        if (collectWoodEnabled)
        {
            collectWoodPrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                CollectWood();
            }
        }

        else
        {
            collectWoodPrompt.SetActive(false);
        }

        if (collectWaterEnabled)
        {
            collectWaterPrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                CollectWater();
            }
        }

        else
        {
            collectWaterPrompt.SetActive(false);
        }
    }

    public void CollectWater()  //activated when clicking collect water button
    {
        collectWaterEnabled = false;

        collectingWater.SetActive(true);  //activate collecting water UI or animation

        controller.enabled = false; //deactivate character controller

        StartCoroutine(WaterCollection());
    }

    IEnumerator WaterCollection()
    {
        yield return new WaitForSeconds(4);
        //update water collected amount
        waterCollected = true; //need this to activate treat water task
        collectingWater.SetActive(false);  //deactivate collecting UI

        controller.enabled = true; //Reactivate character controller

    }

    public void CollectWood()  //activated when clicking collect wood button
    {
        collectWoodEnabled = false;

        collectingWood.SetActive(true); //activate collecting wood UI or animation
        
        controller.enabled = false; //deactivate character controller

        StartCoroutine(WoodCollection());
    }

    IEnumerator WoodCollection()
    {
        yield return new WaitForSeconds(4);
        //update wood collected amount
        woodCollected = true; //need this to activate fire task
        collectingWood.SetActive(false);//deactivate collecting UI
        
        controller.enabled = true; //Reactivate character controller

    }
}
