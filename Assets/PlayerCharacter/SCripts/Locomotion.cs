using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class Locomotion : MonoBehaviour
{
    CharacterController charactercController;
    Transform playerContainer, cameraContainer;

    public float speed = 6.0f;
    public float jumpSpeed = 10f;
    public float mouseSensitivity = 2f;
    public float gravity = 20.0f;
    public float lookupClamp = -30f;
    public float lookDownClamp = 60f;

    private Vector3 moveDirection = Vector3.zero;
    float rotateX, rotateY;
    

    void Start()
    {
        Cursor.visible = false;
        charactercController = GetComponent<CharacterController>();
        SetCurrentCamera();

        //SetCurrentCamera();

    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        RotateAndLook();
    }

    void SetCurrentCamera()
    {
        playerContainer = gameObject.transform.Find("Container3P");
        cameraContainer = playerContainer.transform.Find("Camera3PContainer");
    }

    void Movement()
    {
        if(charactercController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            //to do jumping/crouching
        }
        moveDirection.y = gravity * Time.deltaTime;
        charactercController.Move(moveDirection * Time.deltaTime);
    }

    void RotateAndLook()
    {
        rotateX = Input.GetAxis("Mouse X") * mouseSensitivity;

        rotateY -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotateY = Mathf.Clamp(rotateY, lookupClamp, lookDownClamp);
        transform.Rotate(0f, rotateX, 0f);

        cameraContainer.transform.localRotation = Quaternion.Euler(rotateY, 0f, 0f);

    }

}
