using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed = 10;
    public GameObject gun;
    public float bulletSpeed = 20f;

    public float shootingCooldown = 0.5f; // Cooldown-Zeit in Sekunden zwischen den Schüssen

    private float shootingTimer; // Timer, der verfolgt, wann zuletzt geschossen wurde
    private CharacterController characterController;

    void Awake() {

    }
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        shootingTimer += Time.deltaTime;

        //HandleMovementInput();
        HandleMovementCharacterController();
        HandlePlayerToMouseRelationship();

        if (Input.GetButton("Fire1") && shootingTimer >= shootingCooldown)
        {
            HandleShootInput();
            shootingTimer = 0f; // Setze den Timer zurück, nachdem geschossen wurde
        }
    }

    void HandleShootInput() {

       Gun.Instance.Shoot();
       
    }

    void HandleMovementCharacterController()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(h, 0.0f, v);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleMovementInput() {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //rb.AddForce(Vector3.right * speed * h * Time.deltaTime);
        //rb.AddForce(Vector3.forward * speed * v * Time.deltaTime);
    }

    void HandlePlayerToMouseRelationship() {

        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))    
        {
            // Berechne den horizontalen Abstand zum Trefferpunkt
            Vector3 hitPointHorizontal = new Vector3(_hit.point.x, transform.position.y, _hit.point.z);
            float distance = Vector3.Distance(transform.position, hitPointHorizontal);

            // Stellt sicher, dass der Abstand groß genug ist, bevor Drehung durchführt
            if (distance > 0.5f) 
            {
                transform.LookAt(hitPointHorizontal);
            }
        }
    }
}
