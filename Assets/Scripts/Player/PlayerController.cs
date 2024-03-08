using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{


    [SerializeField] private float velocity = 10;
    [SerializeField] private float rotationVelocity = 10;

    private CharacterController characterController;

    private Vector3 moveDirection;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        GameManager.Instance.inputManager.OnJump += HandleOnJump;

    }

    private void Update()
    {
        HandleMove();
        
    }


    private void HandleMove()
    {
        
        Vector2 inputData = GameManager.Instance.inputManager.MoveDirection;
        moveDirection.x = inputData.x;
        moveDirection.z= inputData.y;
        Vector3 cameraRelativeMovement = ConvertMoveDirectionToCameraSpace(moveDirection);



        characterController.Move(cameraRelativeMovement * velocity * Time.deltaTime);
        RotatePlayerAccordingToInput(cameraRelativeMovement);
        print(moveDirection);
    }
    private void RotatePlayerAccordingToInput(Vector3 cameraRelativeMovement)
    {
        if (moveDirection == Vector3.zero) return;

        Vector3 pointToLookAt;
        pointToLookAt.x = cameraRelativeMovement.x;
        pointToLookAt.y = 0;
        pointToLookAt.z = cameraRelativeMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(pointToLookAt); 
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationVelocity * Time.deltaTime);

        }

    }


    private Vector3 ConvertMoveDirectionToCameraSpace(Vector3 moveDirection)
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 cameraForwardZProduct = cameraForward * moveDirection.z;
        Vector3 cameraRightXProduct = cameraRight * moveDirection.x;

        Vector3 directionToMovePlayer = cameraForwardZProduct + cameraRightXProduct;

        return directionToMovePlayer;

    }


    private void HandleOnJump()
    {
        print(message: "Estou pulando");
    }
}
