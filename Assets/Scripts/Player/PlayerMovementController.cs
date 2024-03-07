using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    
    [Range(0f, 10f)]
    [SerializeField] private float speed;
    [SerializeField] private Transform avatar;
    private CharacterController characterController;
    private PlayerStateController playerStateController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerStateController = GetComponent<PlayerStateController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y - transform.position.y));
        Vector3 lookDirection = mouseWorldPosition - transform.position;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        if (Between(h, -0.5f, 0.5f) && Between(v, -0.5f, 0.5f)) 
        {
            HandleRotation(lookDirection);
            SetIdle();
            return;
        }

        Vector3 moveDirection = new Vector3(h, 0.0f, v);
        characterController.Move(moveDirection * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            HandleRotation(lookDirection);
            SetSprint();
            return;
        }

        float normalizedAngle = calcNormalizedAngle(moveDirection, lookDirection);

        if (normalizedAngle < 0.25) {
            SetRunForward();
        } else if (normalizedAngle < 0.5) {
            SetRunLeft();
        } else if (normalizedAngle < 0.75) {
            SetRunBackwards();
        } else {
            SetRunRight();
        }

        HandleRotation(lookDirection);
    }

    private void HandleRotation(Vector3 lookDirection)
    {
        PlayerState currentState = playerStateController.GetCurrentState();
        float rotationOffset = currentState switch
        {
            PlayerState.IS_RUNNING_FORWARD => 45f,
            PlayerState.IS_RUNNING_BACKWARDS => 45f,
            PlayerState.IS_RUNNING_LEFT => 45f,
            PlayerState.IS_RUNNING_RIGHT => -45f,
            _ => 0f,
        };

        Quaternion offsetRotation = Quaternion.AngleAxis(rotationOffset, Vector3.up);
        lookDirection = offsetRotation * lookDirection;
        lookDirection.y = 0f;
        avatar.rotation = Quaternion.LookRotation(lookDirection);
    }

    private float getRotationOffset(PlayerState currentState)
    {
        return currentState switch
        {
            PlayerState.IS_RUNNING_LEFT => 45,
            PlayerState.IS_RUNNING_RIGHT => -45,
            _ => 0,
        };
    }

    private bool Between(float value, float lower, float upper)
    {
        return value < upper && value > lower;
    }

    private float calcNormalizedAngle(Vector3 movementDirection, Vector3 lookDirection)
    {
        float angle = Vector3.Angle(movementDirection, lookDirection);

        if (Vector3.Dot(Vector3.Cross(movementDirection, lookDirection), Vector3.up) < 0)
        {
            angle = 360 - angle;
        }

        angle = (angle + 45) % 360;
        return angle / 360;
    }

    private void SetIdle()
    {
        Debug.Log("Idle");
        playerStateController.ChangeState(PlayerState.IS_IDLE);
    }

    private void SetRunForward()
    {
        Debug.Log("Run Forward");
        playerStateController.ChangeState(PlayerState.IS_RUNNING_FORWARD);
    }

    private void SetRunLeft()
    {
        Debug.Log("Run Left");
        playerStateController.ChangeState(PlayerState.IS_RUNNING_LEFT);
    }

    private void SetRunBackwards()
    {
        Debug.Log("Run Backwards");
        playerStateController.ChangeState(PlayerState.IS_RUNNING_BACKWARDS);
    }

    private void SetRunRight()
    {
        Debug.Log("Run Right");
        playerStateController.ChangeState(PlayerState.IS_RUNNING_RIGHT);
    }

    private void SetSprint()
    {
        Debug.Log("Sprint");
        playerStateController.ChangeState(PlayerState.IS_SPRINTING);
    }

    private void SetRoll()
    {
        Debug.Log("Roll");
        playerStateController.ChangeState(PlayerState.IS_ROLLING);
    }
}
