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

    public GameObject waterInventory;
    public GameObject fishInventory;
    public GameObject woodInventory;
    

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

    Vector2 mousePos;
    public Camera cam;
    public GameObject sticks;
    public GameObject stick;
    public bool fireStarted;
    public GameObject fireBadge;
    public GameObject fireBadgeAchievement;

    //Sounds
    public GameObject WaterSound;
    public GameObject CampSound;
    public GameObject FireEffect;
    public GameObject WoodPlacementSound;

    public GameObject inventoryBag;
    public bool inventoryOpen;

    public GameObject collectAloePrompt;
    public GameObject collectGingerPrompt;
    public bool collectAloeEnabled;
    public bool collectGingerEnabled;

    public GameObject[] aloeUI;
    public GameObject aloeInventory;
    bool aloeCollected;

    public GameObject[] gingerUI;
    public GameObject gingerInventory;
    bool gingerCollected;

    public int pointsExplored;
    public GameObject exploreBadge;
    public Text exploredArea;

    public bool gamePaused;
    public GameObject escapeTab;

    public GameObject[] completeObjectiveTick;
    bool[] firstTime = new bool[9];

    public GameObject pickUpAxePrompt;
    public bool axeSeen;
    public bool axeCollected;
    public GameObject axe1;
    public GameObject axe2;
    bool axeActive;
    public GameObject axeInBag;
    //public GameObject activateAxePrompt;
    public GameObject axeInventory;

    public bool isFire;

    public bool collectPlumEnabled;
    public bool collectAppleEnabled;
    public bool collectMangoEnabled;

    int numberOfApples;
    int numberOfPlums;
    int numberOfMangos;

    public GameObject collectApplePrompt;
    public GameObject collectPlumPrompt;
    public GameObject collectMangoPrompt;

    public GameObject appleInventory;
    bool appleCollected;
    public GameObject plumInventory;
    bool plumCollected;
    public GameObject mangoInventory;
    bool mangoCollected;

    public bool activateChemSet;
    public GameObject purifyPrompt;
    public bool chemSetOpen;
    public GameObject chemistrySet;

    // Start is called before the first frame update
    void Start()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Time.timeScale = 1;
        gamePaused = false;
        
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = true;
            escapeTab.SetActive(true);
            Time.timeScale = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            inventoryOpen = true;
     
            ActivateInventory();
               
        }

      //journal.SetActive(journalOpen);

        if (Input.GetKey(KeyCode.C))
        {
            compass.SetActive(true);
        }

        else
        {
            compass.SetActive(false);
        }
        /// CONDITION BARS DECREASING OVER TIME
        if (fed > 1 && !isEating && !inventoryOpen)
        {
            fed -= (Time.deltaTime) / 3; 
        }
        fedBar.fillAmount = fed / 100;

        if (hydrated > 1 && !isDrinking && !inventoryOpen)
        {
            hydrated -= (Time.deltaTime) / 4;
        }
        hydratedBar.fillAmount = hydrated / 100;

        if (energy > 1 && !isResting && !inventoryOpen)
        {
            energy -= (Time.deltaTime) / 5;
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
        ///

        if (collectAloeEnabled)
        {
            collectAloePrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!aloeCollected)
                {
                    AloeCollection();
                }

                else
                {
                    Debug.Log("aloe already collected");
                }
                
            }
        }

        else
        {
            collectAloePrompt.SetActive(false);
        }

        if (collectAppleEnabled)
        {
            collectApplePrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (numberOfApples < 3)
                {
                    CollectApple();
                }

                else
                {
                    Debug.Log("apple storage full");
                }
            }
        }

        else
        {
            collectApplePrompt.SetActive(false);
        }

        if (collectPlumEnabled)
        {
            collectPlumPrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (numberOfPlums < 3)
                {
                    CollectPlum();
                }

                else
                {
                    Debug.Log("plum storage full");
                }
            }
        }

        else
        {
            collectPlumPrompt.SetActive(false);
        }

        if (collectMangoEnabled)
        {
            collectMangoPrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (numberOfMangos < 3)
                {
                    CollectMango();
                }

                else
                {
                    Debug.Log("mango storage full");
                }
            }
        }

        else
        {
            collectMangoPrompt.SetActive(false);
        }

        if (collectGingerEnabled)
        {
            collectGingerPrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!gingerCollected)
                {
                    GingerCollection();
                }

                else
                {
                    Debug.Log("ginger already collected");
                }

            }
        }

        else
        {
            collectGingerPrompt.SetActive(false);
        }

        if (collectWoodEnabled && !axeSeen)
        {
            /*if (axeCollected) //&& inFrontOfTree
            {
                if (!axeActive)
                {
                    activateAxePrompt.SetActive(true);
                }

                else
                {
                    activateAxePrompt.SetActive(false);
                }

               /* if (Input.GetKeyDown(KeyCode.A) && axeCollected)
                {
                    if (!axeActive)
                    {
                        axe2.SetActive(true);
                        axeActive = true;
                    }

                    else
                    {
                        axe2.SetActive(false);
                        axeActive = false;
                    }
                }
            }*/
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
                    ObjectivesCompleted(1);
                }
                
            }
        }

        else
        {
            collectWoodPrompt.SetActive(false);
            //activateAxePrompt.SetActive(false);
            axe2.SetActive(false);
            axeActive = false;
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
                    ObjectivesCompleted(3);
                }
                
            }
        }

        else
        {
            collectWaterPrompt.SetActive(false);
        }

        if (axeSeen)
        {
            pickUpAxePrompt.SetActive(true);
            AxeCollection();
        }

        else
        {
            pickUpAxePrompt.SetActive(false);
        }

        if (activateChemSet && waterCollected)
        {
            purifyPrompt.SetActive(true);
        }

        else
        {
            purifyPrompt.SetActive(false);
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
                    fishingRodPrompt.SetActive(false);
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
                    if (numberOfLogs >= 1)
                    {
                        setUpLogsPrompt.SetActive(false);
                        numberOfLogs -= 1;
                        numberOfLogsUI.text = numberOfLogs.ToString() + "/10";
                        fireplaceLogs.SetActive(true);
                        WoodPlacementSound.SetActive(true);
                        logsActive = true;

                        if (numberOfLogs <= 0)
                        {
                            woodCollected = false;
                        }

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
                  //CampSound.SetActive(true); //wait till fire started
                    sticks.SetActive(true);
                  //stick.transform.position = mousePos;
                     // cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 5));
                    //lock mouse pos to stick
                    //deactivate charater controller and mouse look, etc
                    //prompt player to move stick to start fire
                    //do a velocity check - activate another bool when reached - fireStarted
                }

                else
                {
                    //CampSound.SetActive(true);
                    FireEffect.SetActive(true);
                    sticks.SetActive(false);
                    fireBadge.SetActive(true);
                    isFire = true;
                    //fireBadgeAchievement.SetActive(true);
                    //activate fire and particle effect stuff
                    StartCoroutine(ReactivateMouse());
                    //StartCoroutine(FireOut());
                    
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

        waterInventory.SetActive(waterCollected);
        woodInventory.SetActive(woodCollected);
        fishInventory.SetActive(fishCaught);

    }
    IEnumerator ReactivateMouse()
    {
        yield return new WaitForSeconds(3);
        fireBadgeAchievement.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PM.enabled = true;
        ML.enabled = true;
    }

    IEnumerator FireOut()
    {
        yield return new WaitForSeconds(10);
        CampSound.SetActive(false);
        FireEffect.SetActive(false);
        isFire = false;

    }
    public void OpenJournal()
    {
        inventoryBag.SetActive(false);
        journalOpen = true;
        journal.SetActive(true);
        
    }

    public void CloseJournal()
    {
        inventoryBag.SetActive(true);
        journalOpen = false;
        journal.SetActive(false);
    }
    public void Exploration()
    {
        pointsExplored++;
        if (pointsExplored < 10)
        {
            exploredArea.text = pointsExplored.ToString() + "0%";
        }

        else
        {
            exploredArea.enabled = false;
            exploreBadge.SetActive(true);
            //badge unlocked UI
        }
        
    }
    void ActivateInventory()
    {
        if (inventoryOpen)
        {
            inventoryBag.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            PM.enabled = false;
            ML.enabled = false;
        }

        else
        {
            inventoryBag.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            PM.enabled = true;
            ML.enabled = true;
        }

    }

    public void CloseInventory()
    {
        Debug.Log("This fn works");
        inventoryOpen = false;
        ActivateInventory();
    }

    public void AloeCollection()
    {
        Debug.Log("Aloe collected");
        collectAloeEnabled = false;
        foreach (GameObject ui in aloeUI)
        {
            ui.SetActive(true);
        }
        
        aloeInventory.SetActive(true);
        aloeCollected = true;

        //UI text saying new plant found! or aloe collected
    }

    public void GingerCollection()
    {
        Debug.Log("Ginger collected");
        collectGingerEnabled = false;
        foreach (GameObject ui in gingerUI)
        {
            ui.SetActive(true);
        }

        gingerInventory.SetActive(true);
        gingerCollected = true;
        
        //UI text display
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

    public void CollectApple()
    {
        collectAppleEnabled = false;
        //picking apple sound
        appleCollected = true;
        appleInventory.SetActive(true);
        numberOfApples++;
        //ui text display to indicate collected
        Debug.Log("apple collected");
    }

    public void CollectPlum()
    {
        collectPlumEnabled = false;
        //picking fruit sound
        plumCollected = true;
        plumInventory.SetActive(true);
        numberOfPlums++;
        //ui text display to indicate collected
        Debug.Log("plum collected");
    }

    public void CollectMango()
    {
        collectMangoEnabled = false;
        //picking fruit sound
        mangoCollected = true;
        mangoInventory.SetActive(true);
        numberOfMangos++;
        //ui text display to indicate collected
        Debug.Log("mango collected");
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

    public void ActivateChemSet()
    {
        if (activateChemSet && waterCollected)
        {
            if (chemSetOpen)
            {
                chemistrySet.SetActive(false);
                controller.enabled = true;
                chemSetOpen = false;
            }

            else
            {
                chemistrySet.SetActive(true);
                controller.enabled = false;
                chemSetOpen = true;
            }
            
        }
    }

    public void AxeCollection()
    {
        if (axeSeen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(axe1);
                axe2.SetActive(true);
                axeActive = true;
                axeCollected = true;
                axeInventory.SetActive(true);
                axeInBag.SetActive(true);
                StartCoroutine(AxeNotSeen());
                
                //activate axe in inventory
                //UI to say press A to pack axe or activate axe etc
            }
        }
    }

    IEnumerator AxeNotSeen()
    {
        yield return new WaitForSeconds(0.5f);
        axeSeen = false;
        yield return new WaitForSeconds(2f);
        axeInBag.SetActive(false);
    }

    public void ActivateAxe()
    {
        if (axeCollected)
        {
            if (axeActive)
            {
                axe2.SetActive(false);
                axeActive = false;
            }

            else
            {
                axe2.SetActive(true);
                axeActive = true;
            }

            CloseInventory();
        }
    }

    public void ObjectivesCompleted(int number)
    {
        if (!firstTime[number])
        {
            //UI or audio for objective complete
            firstTime[number] = true;
            completeObjectiveTick[number].SetActive(true);
        }

        
    }
}
