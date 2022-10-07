using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class Locomotion : MonoBehaviour
{
    CharacterController characterController;
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
        characterController = GetComponent<CharacterController>();
        SetCurrentCamera();

        //SetCurrentCamera();

    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        RotateAndLook();
        PerspectiveCheck();
    }

    void SetCurrentCamera()
    {
        SwitchPerspective switchPerspective = GetComponent<SwitchPerspective>();
        if (switchPerspective.GetPerspective() == SwitchPerspective.Perspective.First)
        {
            playerContainer = gameObject.transform.Find("Container1P");
            cameraContainer = playerContainer.transform.Find("Camera1pContainer");
        }

        playerContainer = gameObject.transform.Find("Container3P");
        cameraContainer = playerContainer.transform.Find("Camera3PContainer");
    }

    void Movement()
    {
        if(characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
            if (Input.GetKey(KeyCode.C))
            {
                characterController.height = 0.65f;
                characterController.center = new Vector3(0f, 0.5f, 0f);
            }
            else
            {
                characterController.height = 2f;
                characterController.center = new Vector3(0f, 1f, 0f);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 10f;
            }
            else
            {
                speed = 6f;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void RotateAndLook()
    {
        rotateX = Input.GetAxis("Mouse X") * mouseSensitivity;

        rotateY -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotateY = Mathf.Clamp(rotateY, lookupClamp, lookDownClamp);
        transform.Rotate(0f, rotateX, 0f);

        cameraContainer.transform.localRotation = Quaternion.Euler(rotateY, 0f, 0f);

    }

    void PerspectiveCheck()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchPerspective switchPerspective = GetComponent<SwitchPerspective>();

            if(switchPerspective != null)
            {
                if (switchPerspective.GetPerspective() == SwitchPerspective.Perspective.First)
                {
                    switchPerspective.SetPerspective(SwitchPerspective.Perspective.First);
                }
                else
                {
                    switchPerspective.SetPerspective(SwitchPerspective.Perspective.Third);
                }

                SetCurrentCamera();
                    

            }
        }
    }

}
