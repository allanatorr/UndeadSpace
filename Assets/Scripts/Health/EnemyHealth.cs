using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public void ApplyDamage(float damage)
    {
        SubtractLife(damage);


        if (health <= 0)
        {
            WaveSpawner.GetInstance().OnEnemyDeath(gameObject.GetComponent<Transform>().position);
            gameObject.GetComponent<EnemyController>().Die();
            Destroy(gameObject);
            // Implementiere die Logik für den Tod des Spielers
        }
    }
}
