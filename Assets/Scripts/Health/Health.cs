using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float health = 100f;
    protected float maxHealth = 100f;
    public GameObject healthbar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubtractLife(float damage) {
        health -= damage;
        healthbar.GetComponent<Healthbar>().UpdateHealth(health / maxHealth);
    }
}
