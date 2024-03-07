using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public float damagePerSecond = 10f; // Schaden pro Sekunde
    private float damageCooldown = 0.5f; // Cooldown in Sekunden
    private float lastDamageTime = -1f; // Zeitpunkt des letzten Schadens

    public GameObject playerHealth;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (Time.time >= lastDamageTime + damageCooldown)
            {
                playerHealth.GetComponent<PlayerHealth>().ApplyDamage(damagePerSecond);
                lastDamageTime = Time.time;
            }
        }
    }
}
