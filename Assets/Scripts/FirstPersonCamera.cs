using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Fall 2023:
 * A first person camera, there exists a third person camera too if needed, but the movement code will need to be changed
 * I copied code from this tutorial
 * https://www.youtube.com/watch?v=f473C43s8nE&ab_channel=Dave%2FGameDevelopment
 */
public class FirstPersonCamera : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public PlayerMovement playerMovement;

    public float rotationSpeed;
    float cameraVerticalRotation = 0;
    float xRotation, yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (playerMovement.canMove)
        {
            // rotate player object
            float horizontalInput = Input.GetAxisRaw("Mouse X") * rotationSpeed;
            float verticalInput = Input.GetAxisRaw("Mouse Y") * rotationSpeed;

            xRotation -= verticalInput;
            yRotation += horizontalInput;

            xRotation = Mathf.Clamp(xRotation, -85, 85);

            cameraVerticalRotation -= verticalInput;
            cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90, 90);
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);

            player.Rotate(Vector3.up * horizontalInput);
        }
    }
}
