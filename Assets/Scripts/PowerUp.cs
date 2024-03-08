using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerupEffect powerupEffect;

    public float lifetime = 15f; // Lebensdauer des Power-Ups in Sekunden

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnDestroy() {
        WaveSpawner.GetInstance().OnPowerUp();
    }

    private void OnTriggerEnter(Collider collider) {

        if(collider.gameObject.tag == "Player") {
            
            powerupEffect.Apply(collider.gameObject);
            Destroy(gameObject);
        }
    }
}
