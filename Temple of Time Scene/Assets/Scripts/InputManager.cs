using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;

    // Movement variable (up, down, left right)
    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool lshift_input;
    public bool jump_input;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    // When the game object attached to it, is enabled
    private void OnEnable()
    {
        // if the player control is not loaded in
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            // when WASD or control stick has been inputted, we record the vector2
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed   += i => cameraInput   = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Leftshift.performed += i => lshift_input = true;
            playerControls.PlayerActions.Leftshift.canceled += i => lshift_input = false;
            playerControls.PlayerActions.Jump.performed += i => jump_input = true;

        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpingInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        // Abs values of the vertical/horizontal because we don't have negative values
        // we would use negative values if we had sth like strafing, moving backwards while locking on a target
        // with positive values we basically only run in the direction we look
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleSprintingInput()
    {
        if (lshift_input && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }

    private void HandleJumpingInput()
    {
        if (jump_input)
        {
            jump_input = false;
            playerLocomotion.HandleJumping();
        }
    }
}
