using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerContoller : MonoBehaviour
{  
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    
    private Vector2 movementInput = Vector2.zero;
    private bool hasJumped = false;
    private bool canWallJump = false;

    PlayersCamera playersCamera;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        playersCamera = FindAnyObjectByType<PlayersCamera>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        float aux = context.ReadValue<float>();
        if (aux != 0f ) { hasJumped = true; }
        else { hasJumped = false;  }
       // hasJumped = context.action.triggered;

    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if(hasJumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        //WallJump
        if(hasJumped && canWallJump)
        {
           
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(groundedPlayer && hit.collider.CompareTag("Wall")){
            canWallJump = true;
        }
    }
    
    public void AddPlayer()
    {
        playersCamera.AddPlayer(transform);

        Debug.Log("Player connected");
    }
}
