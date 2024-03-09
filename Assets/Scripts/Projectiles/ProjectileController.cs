using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] GameObject bulletHolePrefab;
    private float speed;
    private Vector3 direction;
    private float damage;
    private float range;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5); // Zerstört das Bullet nach einer bestimmten Zeit
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetRange(float range)
    {
        this.range = range;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            // Get the contact point of collision
            ContactPoint contact = other.contacts[0];
            Vector3 collisionPoint = contact.point;

            // Instantiate bullet hole at the collision point
            Instantiate(bulletHolePrefab, collisionPoint, Quaternion.identity);

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}
