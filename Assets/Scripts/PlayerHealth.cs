using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public float health = 100f;
    public float maxHealth = 100f;
    public GameObject healthbar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        healthbar.GetComponent<Healthbar>().UpdateHealth(health / maxHealth);

        Debug.Log("Spieler erhält Schaden! Verbleibende Gesundheit: " + health);

        if (health <= 0)
        {
            Debug.Log("Spieler ist gestorben!");

            // Implementiere die Logik für den Tod des Spielers
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
