using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Trigger the "isKicking" animation
            animator.SetBool("isKicking", true);
            animator.SetBool("isFighting", false);
        }

        // Check if the right mouse button is pressed
        if (Input.GetMouseButtonDown(1))
        {
            // Trigger the "isFighting" animation
            animator.SetBool("isKicking", false);
            animator.SetBool("isFighting", true);
        }

        // Check if both mouse buttons are released
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            // Set both animations to false
            animator.SetBool("isKicking", false);
            animator.SetBool("isFighting", false);
        }
    }
}