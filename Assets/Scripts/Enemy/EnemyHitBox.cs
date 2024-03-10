using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    private float damage;
    private bool dealtDamage;
    private void OnTriggerStay(Collider other) 
    {
        if (dealtDamage) return;
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().ApplyDamage(damage);
        }
        dealtDamage = true;
    }

    public void Reset()
    {
        dealtDamage = false;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}
