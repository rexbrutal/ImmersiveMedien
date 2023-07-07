using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // Referencing the variables of the animator
        // when we use these int variables, we are referencing our animators horizontal and vertical variables
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        // Animation Snapping
        float snappedHorizontal = horizontalMovement;
        float snappedVertical = verticalMovement;

        
        #region Snapped Horizontal
        if (horizontalMovement > 0 && horizontalMovement < 0.5f)
        {
            snappedHorizontal = 0.5f;
        }
        else if (horizontalMovement > 0.5f)
        {
            snappedHorizontal = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.5f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMovement < -0.5f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion
        #region Snapped Vertical
        if (verticalMovement > 0 && verticalMovement < 0.5f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMovement > 0.5f)
        {
            snappedVertical = 1;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.5f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMovement < -0.5f)
        {
            snappedVertical = -1;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion
        

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }
}
