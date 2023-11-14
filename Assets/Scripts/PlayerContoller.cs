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

    public bool maldicion;

    [SerializeField] PlayerContoller otherPlayer;

    ParticleSystem particles;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        particles = gameObject.GetComponent<ParticleSystem>();
       // particles.Stop();
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
            if (otherPlayer.GetMaldicion() == false)
            {
                otherPlayer.SetMaldicion(true);
                maldicion = false;
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

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        Debug.Log(maldicion);

        if(maldicion)
        {
            particles.Play();
        }

        if(!maldicion)
        {
            particles.Stop();
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.CompareTag("Wall") && maldicion){
            Debug.Log("tiene maldición");
            Physics.IgnoreCollision(hit.collider, gameObject.GetComponent<CharacterController>());
        }

        else if(hit.collider.CompareTag("Wall") && !maldicion)
        {
            Debug.Log("NOOOOO tiene maldición");
            Physics.IgnoreCollision(hit.collider, gameObject.GetComponent<CharacterController>(), true);
        }
    }

    public bool GetMaldicion() { return maldicion; }
    public void SetMaldicion(bool maldicion) { this.maldicion = maldicion;}
}
