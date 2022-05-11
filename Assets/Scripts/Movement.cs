using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    // podczepiamy pod statek
    [SerializeField] private float thrust = 7f;
    [SerializeField] private float ceillingCap = 12f;
    [SerializeField] private float cellingPush = 10f; 
    Rigidbody rb;
    [SerializeField] private Camera mainCamera;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 9.5f;
        rb.mass = 1f;
    }
    void Update()
    {
        //if(transform.position.y <= ceillingCap) { 
            //
        //} else
        //{
            //rb.AddForce(Vector3.down * cellingPush, ForceMode.Impulse);
        //}
        StarshipMovement();
    }
    void StarshipMovement()
    {
        // skrypt przesuwania zak³adaj¹cy przesuwanie mapy wzglêdem statku
        // statek mo¿e przesuwaæ siê w górê/dól; na boki
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0f);
        rb.AddForce(direction * thrust, ForceMode.Acceleration);
    }
}
