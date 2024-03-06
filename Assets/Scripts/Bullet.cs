using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Vector3 firingPoint;

    public float speed = 20f;

    void Start()
    {
        firingPoint = transform.position;
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
            Destroy(other.gameObject);
        }
    }
}
