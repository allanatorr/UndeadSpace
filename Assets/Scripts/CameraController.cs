using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    Vector3 offset;
    [Range(0f, 1f)]
    public float smoothness = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position + target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, 1.0f-smoothness);
    }
}
