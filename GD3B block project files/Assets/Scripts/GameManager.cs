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
    bool fishCaught;

    public bool collectWaterEnabled;
    public GameObject collectWaterPrompt;
    public GameObject collectingWater;

    public bool collectWoodEnabled;
    public GameObject collectWoodPrompt;
    public GameObject collectingWood;
    int numberOfLogs;
    public Text numberOfLogsUI;

    public GameObject hasWater;
    public GameObject hasFish;
    public GameObject hasWood;
    

    public bool startFishingEnabled;
    public GameObject fishingRodPrompt;
    public GameObject fishingRod;
    bool rodActive;

    public CharacterController controller;
    public PlayerMovement PM;

    public GameObject compass;

    public GameObject journal;
    public bool journalOpen;
    public MouseLook ML;
    //public GameObject journalPrompt;

    public CollectionUI woodScript;
    public CollectionUI waterScript;

    public bool startFireEnabled;
    public GameObject setUpLogsPrompt;
    public bool logsActive;
    public GameObject fireplaceLogs;

    Vector3 mousePos;
    public Camera cam;
    public GameObject sticks;
    public GameObject stick;
    bool fireStarted;

    //Sounds
    public GameObject WaterSound;
    public GameObject CampSound;
    public GameObject WoodPlacementSound;

    public GameObject inventoryBag;
    public bool inventoryOpen;

    // Start is called before the first frame update
    void Start()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (inventoryOpen)
            {
                inventoryOpen = false;
            }

            else
            {
                inventoryOpen = true;
            }
        }

        inventoryBag.SetActive(inventoryOpen);

        if (inventoryOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            PM.enabled = false;
            ML.enabled = false;
        }

        else
        {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            PM.enabled = true;
            ML.enabled = true;
        }

        journal.SetActive(journalOpen);

        if (Input.GetKey(KeyCode.C))
        {
            compass.SetActive(true);
        }

        else
        {
            compass.SetActive(false);
        }
        /// CONDITION BARS DECREASING OVER TIME
        if (fed > 1 && !isEating && !journalOpen) //&& no increase bools are true or something like that
        {
            fed -= (Time.deltaTime) / 2; 
        }
        fedBar.fillAmount = fed / 100;

        if (hydrated > 1 && !isDrinking && !journalOpen)
        {
            hydrated -= (Time.deltaTime) / 2;
        }
        hydratedBar.fillAmount = hydrated / 100;

        if (energy > 1 && !isResting && !journalOpen)
        {
            energy -= (Time.deltaTime) / 2;
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
                if (numberOfLogs >= 10)
                {
                    Debug.Log("ALREADY COLLECTED WOOD");
                }

                else
                {
                    CollectWood();
                }
                
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
                if (waterCollected)
                {
                    Debug.Log("ALREADY COLLECTED WATER");
                }

                else
                {
                    WaterSound.SetActive(true);
                    CollectWater();
                }
                
            }
        }

        else
        {
            collectWaterPrompt.SetActive(false);
        }

        if (startFishingEnabled)
        {
            fishingRod.SetActive(rodActive);
            fishingRodPrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (rodActive)
                {
                    rodActive = false;
                    controller.enabled = true;
                }

                else
                {
                    rodActive = true;
                    controller.enabled = false;
                    //link mouse to fishing rod
                }
            }
        }

        else
        {
            fishingRodPrompt.SetActive(false);
        }

        if (startFireEnabled)
        {
            if (!logsActive)
            {
                setUpLogsPrompt.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (numberOfLogs >= 3)
                    {
                        setUpLogsPrompt.SetActive(false);
                        numberOfLogs -= 3;
                        numberOfLogsUI.text = numberOfLogs.ToString() + "/10";
                        fireplaceLogs.SetActive(true);
                        WoodPlacementSound.SetActive(true);
                        logsActive = true;
                    }

                    else
                    {
                        setUpLogsPrompt.SetActive(false);
                        //deactivate setup prompt and activate not enough logs UI
                        Debug.Log("not enough logs!!!");
                    }
                }
            }

            else
            {
                if (!fireStarted)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    PM.enabled = false;
                    ML.enabled = false;
                    CampSound.SetActive(true);
                    sticks.SetActive(true);
                    stick.transform.position = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 5));
                    //lock mouse pos to stick
                    //deactivate charater controller and mouse look, etc
                    //prompt player to move stick to start fire
                    //do a velocity check - activate another bool when reached - fireStarted
                }
            }
        }

        else
        {
            setUpLogsPrompt.SetActive(false);
        }

        

        /// PLAYER HAS RESOURCES UI
        hasWater.SetActive(waterCollected);
        hasWood.SetActive(woodCollected);
        hasFish.SetActive(fishCaught);

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
        WaterSound.SetActive(false);
        controller.enabled = true; //Reactivate character controller
        waterScript.timePassed = 0;

    }

    public void CollectWood()  //activated when clicking collect wood button
    {
        woodScript.timePassed = 0;
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
        numberOfLogs++;
        numberOfLogsUI.text = numberOfLogs.ToString() + "/10";
        collectingWood.SetActive(false);//deactivate collecting UI
        
        controller.enabled = true; //Reactivate character controller
        woodScript.timePassed = 0;

    }
}
