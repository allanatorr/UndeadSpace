using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtBox : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;

    [SerializeField] EnemyHealth enemyHealth;

    public void DealDamage(float damage)
    {
        enemyHealth.ApplyDamage(damage);
    }
}
