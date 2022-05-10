using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public int hp = 2;
    public float moveSpeed = 1F;
    [SerializeField] float rotSpeed = 1F;
    [SerializeField] float cutoffZ = -24F; // when part is gone from screen
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        // Idle rotation
        Vector3 rotVector = new Vector3(rotSpeed, rotSpeed, rotSpeed);
        rb.AddTorque(rotVector, ForceMode.Force);

        // Move asteroid
        rb.AddForce(new Vector3(0, 0, -moveSpeed), ForceMode.Force);

        if (transform.position.z < cutoffZ)
        {
            Destroy(gameObject);
        }
    }
}

