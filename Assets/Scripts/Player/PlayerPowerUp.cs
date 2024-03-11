using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{

    private bool isSpeedBuffActive = false;

    private float boostTimer = 0f;

    private PlayerMovementController playerMovementController;

    void Start() {
        playerMovementController = gameObject.GetComponent<PlayerMovementController>(); 
    }

    void Update() {
        HandlePlayerSpeed();
    }

    public void ApplyHealthBuff(float amount)
    {
        gameObject.GetComponent<PlayerHealth>().ApplyHealthBuff(amount);
    }

    public void ApplySpeedBuff(float amount, float time)
    {
        isSpeedBuffActive = true;

        if(boostTimer <= 0) {
            Debug.Log("##################");
            playerMovementController.RunningSpeed = playerMovementController.RunningSpeed + amount;
            // speed += amount;
        }
        boostTimer = time;
    }

    public void HandlePlayerSpeed() {

        if (isSpeedBuffActive)
        {
            boostTimer -= Time.deltaTime;

            if (boostTimer <= 0)
            {
                isSpeedBuffActive = false;
                // speed = defaultSpeed;
                boostTimer = 0;
            }
        }
    }
}
