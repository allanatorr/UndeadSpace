using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] EnemyHitBox enemyHitBox;

    private void EnableHitBox()
    {
        enemyHitBox.Reset();
        enemyHitBox.gameObject.SetActive(true);
    }

    private void DisableHitBox()
    {
        enemyHitBox.gameObject.SetActive(false);
    }
}
