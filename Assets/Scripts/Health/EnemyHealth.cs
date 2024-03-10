using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public void ApplyDamage(float damage)
    {
        SubtractLife(damage);

        Debug.Log("Gegner erhält Schaden! Verbleibende Gesundheit: " + health);

        if (health <= 0)
        {
            Debug.Log("Gegner ist gestorben!");
            WaveSpawner.GetInstance().OnEnemyDeath(gameObject.GetComponent<Transform>().position);
            Destroy(gameObject);
            // Implementiere die Logik für den Tod des Spielers
        }
    }
}
