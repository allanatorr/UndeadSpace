using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    private Rigidbody rb;
    private int speed;

    public int defaultSpeed = 700;

    public GameObject gun;
    public GameObject health;
    public float bulletSpeed = 20f;

    public float shootingCooldown = 0.5f; // Cooldown-Zeit in Sekunden zwischen den Schüssen

    private float shootingTimer; // Timer, der verfolgt, wann zuletzt geschossen wurde

    private bool isSpeedBoosted = false;

    private float boostTimer = 0f;

    void Awake() {
        health = GameObject.FindWithTag("PlayerHealth");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        speed = defaultSpeed;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        shootingTimer += Time.deltaTime;

        HandleMovementInput();
        HandlePlayerToMouseRelationship();
        HandlePlayerSpeed();

        if (Input.GetButton("Fire1") && shootingTimer >= shootingCooldown)
        {
            HandleShootInput();
            shootingTimer = 0f; // Setze den Timer zurück, nachdem geschossen wurde
        }
    }

    void HandleShootInput() {

       Gun.Instance.Shoot();
    }

    void HandleMovementInput() {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rb.AddForce(Vector3.right * speed * h * Time.deltaTime);
        rb.AddForce(Vector3.forward * speed * v * Time.deltaTime);
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

    public void ApplyHealthBuff(float amount)
    {
        health.GetComponent<PlayerHealth>().ApplyHealthBuff(amount);
    }

    public void ApplySpeedBuff(int amount, float time)
    {
        isSpeedBoosted = true;

        if(boostTimer <= 0) {
            speed += amount;
        }
        boostTimer = time;
    }

    public void HandlePlayerSpeed() {

        if (isSpeedBoosted)
        {
            boostTimer -= Time.deltaTime;

            if (boostTimer <= 0)
            {
                isSpeedBoosted = false;
                speed = defaultSpeed;
                boostTimer = 0;
            }
        }
    }
}
