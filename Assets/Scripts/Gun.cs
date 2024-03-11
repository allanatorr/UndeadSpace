using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject bulletPrefab;

    public static Gun Instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake() {
        Instance = GetComponent<Gun>();
    }

    public void Shoot() {

       Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    }
}
