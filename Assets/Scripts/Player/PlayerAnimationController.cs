using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Between(horizontalInput, -0.5f, 0.5f) && Between(verticalInput, -0.5f, 0.5f)) 
        {
            Debug.Log("Idle");
            setAllFalse();
            animator.SetBool("isIdle", true);
            return;
        }


        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y - transform.position.y));
        Vector3 lookDirection = mouseWorldPosition - transform.position;
        lookDirection.y = 0f;
        Vector3 mouseDirection = lookDirection.normalized;

        // calc angle between vectors
        float angle = Vector3.Angle(movementDirection, mouseDirection);

        // translate range [0,180] of angle to range [0, 360]
        if (Vector3.Dot(Vector3.Cross(movementDirection, mouseDirection), Vector3.up) < 0)
        {
            angle = 360 - angle;
        }

        angle = (angle + 45) % 360;
        float normalizedAngle = angle / 360;

        if (normalizedAngle < 0.25) {
            Debug.Log("Run Forward");
            setAllFalse();
            animator.SetBool("isRunningForward", true);
        } else if (normalizedAngle < 0.5) {
            Debug.Log("Strafe Left");
            setAllFalse();
            animator.SetBool("isRunningLeft", true);
        } else if (normalizedAngle < 0.75) {
            Debug.Log("Run Backwards");
            setAllFalse();
            animator.SetBool("isRunningBackwards", true);
        } else {
            Debug.Log("Strafe Right");
            setAllFalse();
            animator.SetBool("isRunningRight", true);
        }
    }

    private bool Between(float value, float lower, float upper)
    {
        return value < upper && value > lower;
    }

    private void setAllFalse()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isRunningForward", false);
        animator.SetBool("isRunningLeft", false);
        animator.SetBool("isRunningBackwards", false);
        animator.SetBool("isRunningRight", false);
    }
}
