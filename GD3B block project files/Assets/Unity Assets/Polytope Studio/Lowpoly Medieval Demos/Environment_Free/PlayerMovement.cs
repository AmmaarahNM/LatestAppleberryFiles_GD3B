using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameManager GM;

    public float speed = 4;
    public float gravity = -9.18f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

  


    void Update()
    {
        this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKey("left shift") && isGrounded)
        {
            speed = 8;
        }
        else
        {
            speed = 4;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            GM.collectWaterEnabled = true;
        }

        if (other.gameObject.tag == "Wood")
        {
            GM.collectWoodEnabled = true;
        }

        if (other.gameObject.tag == "FishingSpot")
        {
            GM.startFishingEnabled = true;
            Debug.Log("Fishing spot");
        }

        if (other.gameObject.tag == "Fireplace")
        {
            if (GM.hasWood) //but not logs in place
            {
                GM.startFireEnabled = true;
                GM.ObjectivesCompleted(0);
            }
        }

        if (other.gameObject.tag == "aloePlant")
        {
            GM.collectAloeEnabled = true;
        }

        if (other.gameObject.tag == "gingerPlant")
        {
            GM.collectGingerEnabled = true;
        }

        if (other.gameObject.tag == "ExplorePoint")
        {
            Debug.Log("More area seen");
            GM.Exploration();
            Destroy(other);
        }

        if (other.gameObject.tag == "Axe")
        {
            GM.axeSeen = true;
        }

        if (other.gameObject.tag == "PlumTree")
        {
            GM.collectPlumEnabled = true;
        }

        if (other.gameObject.tag == "AppleTree")
        {
            GM.collectAppleEnabled = true;
        }

        if (other.gameObject.tag == "MangoTree")
        {
            GM.collectMangoEnabled = true;
        }

        if (other.gameObject.tag == "ChemArea")
        {
            GM.activateChemSet = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            GM.collectWaterEnabled = false;
        }

        if (other.gameObject.tag == "Wood")
        {
            GM.collectWoodEnabled = false;
        }

        if (other.gameObject.tag == "FishingSpot")
        {
            GM.startFishingEnabled = false;
        }

        if (other.gameObject.tag == "Fireplace")
        {
       
            GM.startFireEnabled = false;
       
        }

        if (other.gameObject.tag == "aloePlant")
        {
            GM.collectAloeEnabled = false;
        }

        if (other.gameObject.tag == "gingerPlant")
        {
            GM.collectGingerEnabled = false;
        }

        if (other.gameObject.tag == "Axe")
        {
            GM.axeSeen = false;
        }

        if (other.gameObject.tag == "PlumTree")
        {
            GM.collectPlumEnabled = false;
        }

        if (other.gameObject.tag == "AppleTree")
        {
            GM.collectAppleEnabled = false;
        }

        if (other.gameObject.tag == "MangoTree")
        {
            GM.collectMangoEnabled = false;
        }

        if (other.gameObject.tag == "ChemArea")
        {
            GM.activateChemSet = false;
        }
    }

    /*private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "aloePlant")
        {
            GM.AloeCollection();
        }

        if (collision.gameObject.tag == "gingerPlant")
        {
            GM.GingerCollection();
        }


    }*/
}