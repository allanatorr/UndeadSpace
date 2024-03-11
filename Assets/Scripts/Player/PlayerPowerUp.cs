using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    public void ApplyHealthBuff(float amount)
    {
        gameObject.GetComponent<PlayerHealth>().ApplyHealthBuff(amount);
    }

    // public void ApplySpeedBuff(int amount, float time)
    // {
    //     isSpeedBoosted = true;

    //     if(boostTimer <= 0) {
    //         speed += amount;
    //     }
    //     boostTimer = time;
    // }

    // public void HandlePlayerSpeed() {

    //     if (isSpeedBoosted)
    //     {
    //         boostTimer -= Time.deltaTime;

    //         if (boostTimer <= 0)
    //         {
    //             isSpeedBoosted = false;
    //             speed = defaultSpeed;
    //             boostTimer = 0;
    //         }
    //     }
    // }
}
