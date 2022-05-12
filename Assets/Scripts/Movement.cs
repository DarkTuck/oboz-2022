using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    // podczepiamy pod statek
    [SerializeField] private float thrust = 7f;
    Rigidbody rb;
    Vector3 direction;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 9.5f;
        rb.mass = 1f;
    }
    void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
    }
    void FixedUpdate()
    {
        rb.AddForce(direction * thrust, ForceMode.Acceleration);
    }
}
