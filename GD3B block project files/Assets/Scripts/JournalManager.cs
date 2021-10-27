using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    public CharacterController controller;
    public PlayerMovement PM;

    public GameObject journal;

    public MouseLook ML;
    //public GameObject journalPrompt;

    public GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        journal.SetActive(GM.journalOpen);
        PM.enabled = !GM.journalOpen;
        ML.enabled = !GM.journalOpen;
    }

    public void OnMouseDown()
    {
        GM.journalOpen = true;
    }


}
