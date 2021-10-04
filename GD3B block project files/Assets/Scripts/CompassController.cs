using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassController : MonoBehaviour
{
    public Transform playerRot;
    Vector3 compassHandMotion;

    // Update is called once per frame
    void Update()
    {
        compassHandMotion.z = playerRot.eulerAngles.y;
        transform.localEulerAngles = compassHandMotion;
    }
}
