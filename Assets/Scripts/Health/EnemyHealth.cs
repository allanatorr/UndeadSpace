using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    EnemyController enemyController;

    private void Start() 
    {
        enemyController = gameObject.GetComponent<EnemyController>();
    }

    public void ApplyDamage(float damage)
    {
        if (enemyController.IsDead()) return;

        SubtractLife(damage);


        if (health <= 0)
        {
            WaveSpawner.GetInstance().OnEnemyDeath(gameObject.GetComponent<Transform>().position);
            gameObject.GetComponent<EnemyController>().Die();
            // Implementiere die Logik für den Tod des Spielers
        }
    }
}