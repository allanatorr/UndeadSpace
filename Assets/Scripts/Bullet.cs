using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;

    void Start()
    {
        Destroy(gameObject, 5); // Zerstört das Bullet nach einer bestimmten Zeit
    }

    void Update() {

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag != "PlayerGun") {
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "Enemy") {

            Debug.Log("Zerstört");
            other.gameObject.GetComponent<EnemyHealth>().ApplyDamage(50);
        }
    }
}
