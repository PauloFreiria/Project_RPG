using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [SerializeField] private float velocity;

    private Vector3 moveDirection;

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

        print(moveDirection);
    }

    private void HandleOnJump()
    {
        print(message: "Estou pulando");
    }
}
