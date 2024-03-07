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

        characterController.Move(moveDirection * velocity * Time.deltaTime);
        RotatePlayerAccordingToInput();
        print(moveDirection);
    }
    private void RotatePlayerAccordingToInput()
    {
        if (moveDirection == Vector3.zero) return;

        Vector3 pointToLookAt;
        pointToLookAt.x = moveDirection.x;
        pointToLookAt.y = 0;
        pointToLookAt.z = moveDirection.z;

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(pointToLookAt);

        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationVelocity * Time.deltaTime);
    }

    private void HandleOnJump()
    {
        print(message: "Estou pulando");
    }
}
