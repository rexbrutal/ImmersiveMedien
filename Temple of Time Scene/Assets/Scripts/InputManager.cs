using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
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

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
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
        // HandleJumpInput
        // HandleActionInput
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
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }
}
