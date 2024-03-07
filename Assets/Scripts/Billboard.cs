using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // Findet die Hauptkamera im Start, um Performanz zu sparen
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
}
