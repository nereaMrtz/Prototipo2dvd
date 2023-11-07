using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.DefaultInputActions;

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

    public bool maldicion;

    MaldicionController maldicionController;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        playersCamera = FindAnyObjectByType<PlayersCamera>();
        maldicionController = FindAnyObjectByType<MaldicionController>();
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

    public void OnMaldicion(InputAction.CallbackContext context)
    {

        
        if (context.action.triggered)
        {
            //    // maldicion = !maldicion;
            if (maldicionController.PlayersReady())
            {
                maldicionController.PassMaldicion();
            }

        }

       


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
        //if(maldicion)
        //{
        //    Debug.Log("maldiciooon");
        //}
        //if (!maldicion)
        //{
        //    Debug.Log("no maldito");
        //}

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        Debug.Log(maldicion);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.CompareTag("Wall") && maldicion){
           // Debug.Log("tiene maldición");
        }
    }

    public bool GetMaldicion() { return maldicion; }
    public void SetMaldicion(bool maldicion) { this.maldicion = maldicion;}
}
