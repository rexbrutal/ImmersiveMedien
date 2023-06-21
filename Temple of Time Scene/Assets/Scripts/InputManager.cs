using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;

    // Movement variable (up, down, left right)
    public Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;

    // When the game object attached to it, is enabled
    private void OnEnable()
    {
        // if the player control is not loaded in
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            // when WASD or control stick has been inputted, we record the vector2
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
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
    }
}
