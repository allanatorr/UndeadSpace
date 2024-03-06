using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] private float speed;
    [SerializeField] private Transform avatar;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y - transform.position.y));
        Vector3 lookDirection = mouseWorldPosition - transform.position;
        lookDirection.y = 0f;
        avatar.rotation = Quaternion.LookRotation(lookDirection);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(h, 0.0f, v);
        characterController.Move(moveDirection * Time.deltaTime * speed);
    }
}
