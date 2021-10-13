using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameManager GM;

    public float speed = 5;
    public float gravity = -9.18f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public GameObject plant1_Icon;
    public GameObject plant2_Icon;
    public GameObject plant3_Icon;



    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKey("left shift") && isGrounded)
        {
            speed = 10;
        }
        else
        {
            speed = 5;
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "plant1")
        {
            plant1_Icon.SetActive(true);
        }

        if (collision.gameObject.tag == "plant2")
        {
            plant2_Icon.SetActive(true);
        }

        if(collision.gameObject.tag == "plant3")
        {
            plant3_Icon.SetActive(true);
        }
    }
}