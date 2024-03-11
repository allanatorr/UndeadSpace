using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtBox : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;

    public void DealDamage(float damage)
    {
        enemyController.DealDamage(damage);
    }
}
