using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerupEffect powerupEffect;

    public float lifetime = 15f; // Lebensdauer des Power-Ups in Sekunden

    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Destroy(gameObject, lifetime);
    }

    void OnDestroy() {
        WaveSpawner.GetInstance().OnPowerUp();
    }

    private void OnTriggerEnter(Collider collider) {
            Debug.Log("Drinnen");
            Debug.Log(collider.gameObject.name);

        if(collider.gameObject.tag == "Player") {
            powerupEffect.Apply(collider.gameObject);
            Destroy(gameObject);
        }
    }

    // public void OnCollisionEnter(Collision collider) {
    //  Debug.Log("Drinnen");
    // Debug.Log(collider.gameObject.name);

    //     if(collider.gameObject.tag == "PlayerHurtbox") {
    //         Debug.Log("#####################");
    //         powerupEffect.Apply(player.gameObject);
    //         Destroy(gameObject);
    //     }
    // }
}
